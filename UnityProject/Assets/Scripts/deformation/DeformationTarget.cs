using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* 
Here, a DeformableMesh object is defined. this allows us to make a nice little copy of an existing
mesh, and modify it without fear of damaging the assets themselves. This also lets us maintain a link
between the gameObject, meshCollider, and meshFilter components for future use.
*/
[System.Serializable]
public class DeformableMesh {
	// the transform of the MeshFilter to which this mesh is associated.
	public Transform transform;
	// The mesh filter that is being used as the deformable mesh object.
	public MeshFilter meshFilter;
	// the mesh renderer is referenced here so we can change the "warp amount" in the shader.
	public MeshRenderer meshRenderer;
	// the collider which is linked to this meshFilter
	public MeshCollider meshCollider;
	// a copy of the original mesh at the start of the game. This will allow us to "reset" the deformation,
	// without too much worry.
	private Mesh baseMesh;

	// we want to ignore any vertices which are intersecting a solid wall, so that objects don't "pull away" from walls.
	// let's build a list of all effected vertex indices, and only update those.
	private float[] vertexWeights;

	// a multiplier for the vertex offsets. This parameter is directly applied to the "Distortion scale"
	// parameter, used in the distortion shader.
	private float distortionScale = 0.0f;

	// The constructor just takes a mesh filter, and collider component (optional). These will be used to
	// build the DeformableMesh object.
	public DeformableMesh ( MeshFilter meshFilter, MeshRenderer meshRenderer, MeshCollider meshCollider = null ) {
		this.transform = meshFilter.transform;
		this.meshFilter = meshFilter;
		this.meshRenderer = meshRenderer;
		this.meshCollider = meshCollider;

		this.baseMesh = CopyMesh( meshFilter.mesh );
		this.InitVertexColorAttributes();
		this.BuildVertexWeightList();
		this.ResetMesh();
	}

	// the CopyMesh function just creates a new mesh object free of external references, with the same data
	// as a given mesh. This is used on startup to ensure that all original mesh data is preserved.
	private Mesh CopyMesh ( Mesh mesh ) {
		Mesh returnMesh = new Mesh();
		returnMesh.vertices = mesh.vertices;
		returnMesh.normals = mesh.normals;
		returnMesh.uv = mesh.uv;
		returnMesh.triangles = mesh.triangles;
		returnMesh.tangents = mesh.tangents;
		returnMesh.colors32 = mesh.colors32;

		return returnMesh;
	}

	// The resetMesh function just sets the modified components of a mesh back to those we cached at the start
	public void ResetMesh () {
		this.meshFilter.mesh = CopyMesh(this.baseMesh);

		if ( this.meshCollider != null )
			this.meshCollider.sharedMesh = CopyMesh(this.baseMesh);
	}

	public void UpdateWarpScale () {
		this.meshRenderer.material.SetFloat( "_DistortionScale", this.distortionScale );
		this.distortionScale = Mathf.Lerp( this.distortionScale, 1.0f, 2.0f * Time.deltaTime );
	}

	private void InitVertexColorAttributes () {
		Color32[] colors32 = new Color32[this.baseMesh.vertices.Length];
		for ( int index = 0; index < colors32.Length; index ++ ) {
			colors32[index] = new Color32( (byte)127, (byte)127, (byte)127, (byte)0 );
		}
		this.baseMesh.colors32 = colors32;
	}

	private void BuildVertexWeightList () {
		this.vertexWeights = new float[ this.baseMesh.vertices.Length ];
		// initialize the weight list to 1.
		for ( int i = 0; i < this.vertexWeights.Length; i ++ ) { this.vertexWeights[i] = 1; }

		// we want to iterate through each vertex in the object. If that vertex is "touching" something else
		// set its weight to zero.
		// NOTE: I tried a few "vertex blending" approaches to weight generation, and these had some problems, mainly
		// that they were SLOW AS HEEEEEELL. I ended up going with this solution which checks a bunch of bounding spheres
		// and sets the weight that way instead.
		for ( int index = 0; index < this.baseMesh.vertices.Length; index ++ ) {
			Vector3 position = this.transform.TransformPoint( this.baseMesh.vertices[index] );
			if ( Physics.CheckSphere( position, 0.01f, Physics.AllLayers^(1<<LayerMask.NameToLayer("Deformable")) ) )
				vertexWeights[index] = 0;
		}
	}

	// The DeformMesh function is called from the deformation manager, and takes a list of active effectors.
	// For each of these effectors, the model-space position is calculated, and vertices are transformed in 
	// model-space, preventing the calculation required to convert individual vertices to world-space. This
	// function will update the meshFilter, and the mesh collider provided that one exists.
	public void DeformMesh ( DeformationEffector effector ) {
		this.distortionScale = 0;
		this.UpdateWarpScale();
		// get the model-space position of the effector.
		Vector3 ms_position = this.transform.InverseTransformPoint( effector.transform.position );

		// pull in the vertices for this mesh,
		if ( effector.ObjectInRange( this.transform ) ) {
			Vector3[] vertices = this.meshFilter.mesh.vertices;
			Color32[] colors32 = this.baseMesh.colors32;

			//for ( int index = 0; index < vertices.Length; index ++ ) {
			for ( int index = 0; index < this.baseMesh.vertices.Length; index ++ ) {
				// and transform each vertex.
				Vector3 displacement = Vector3.zero;
				if ( effector.VertexInRange( ms_position, vertices[index] ) ) {
					Vector3 newPos = effector.TransformVertex( ms_position, vertices[ index ], this.vertexWeights[index] );
					// we don't want the mesh to pull through surfaces, so let's do a raycast from the old position, to the new one.
					// if the ray hits something, then we clamp the deformation.
					Vector3 castVec = newPos - vertices[index];
					RaycastHit castHit;
					if ( Physics.Raycast( this.transform.TransformPoint(vertices[index]), this.transform.TransformDirection(castVec), out castHit, castVec.magnitude, Physics.AllLayers^(1<<LayerMask.NameToLayer("Deformable")) ) ) {
						newPos = this.transform.InverseTransformPoint( castHit.point ) - castVec.normalized * 0.01f;
					}
					displacement = newPos - vertices[index];
					vertices[index] = newPos;
				}

				// invert the displacement. 
				colors32[ index ] = new Color32( (byte)Mathf.Floor(128 + (int)(128*displacement.x/10.0f)), (byte)(128 + Mathf.Floor(128*displacement.y/10.0f)), (byte)(128 + Mathf.Floor(128*displacement.z/10.0f)), (byte)0 );
			}
			
			// lastly, update the mesh data.
			this.meshFilter.mesh.colors32 = colors32;
			this.meshFilter.mesh.vertices = vertices;
			this.meshFilter.mesh.RecalculateBounds();

			// if the mesh collider exists, modify its vertex data as well. (Note: It's not possible to set the vertex data
			// of a mesh colider and have it work. It does a bunch of behind-the-scenes processing on the mesh data, and it
			// only automatically recalculates on assignment.)
			if ( this.meshCollider != null ) {
				Mesh mesh = CopyMesh(this.baseMesh);
				mesh.vertices = vertices;

				this.meshCollider.sharedMesh = mesh;
			}
		}
	}

}


/*
This script will be applied to deformation targets automatically by the deformation manager, and will generate 
DeformableMesh objects for each meshFilter in the object hierarchy.
*/
public class DeformationTarget : MonoBehaviour {
	// a list of all deformable meshes in this object and its children
	public DeformableMesh mesh; 
	// when a mesh is deformed, we need to correct collision by moving objects out of the volume. To do this,
	// keep track of all the objects currently interacting with the volume.
	private Dictionary<int, Collision> trackedCollisions = new Dictionary<int, Collision>();

	// When this script is first instantiated, fetch all meshFilters in our hierarchy, and build deformable
	// mesh objects for them.
	public void Awake () {
		//MeshFilter[] filters = gameObject.GetComponentsInChildren<MeshFilter>();
		//this.meshes = new DeformableMesh[ filters.Length ];

		//for ( int index = 0; index < filters.Length; index ++ )
		//	this.meshes[ index ] = new DeformableMesh( filters[ index ], filters[ index ].GetComponent<MeshCollider>() );
		this.mesh = new DeformableMesh( this.gameObject.GetComponent<MeshFilter>(), this.gameObject.GetComponent<MeshRenderer>(), this.gameObject.GetComponent<MeshCollider>() );
	}

	public void OnCollisionEnter ( Collision collision ) {
		Debug.Log( "DEBUG: collision information updated" );
		this.trackedCollisions[ collision.gameObject.GetInstanceID() ] = collision;
	}

	public void OnCollisionExit ( Collision collision ) {
		Debug.Log( "DEBUG: collision information removed" );
		this.trackedCollisions.Remove( collision.gameObject.GetInstanceID() );
	}

	public void OnCollisionStay ( Collision collision ) {
		Debug.Log( "DEBUG: collision information updated" );
		this.trackedCollisions[ collision.gameObject.GetInstanceID() ] = collision;
	}

	public void Update () {
		this.mesh.UpdateWarpScale();
	}

	// The deformation manager will call this function, and it will iterate through all meshes and sub-meshes, and will call
	// their respective deformation functions.
	public void DeformMesh ( List<DeformationEffector> effectors ) {
		//foreach ( DeformableMesh mesh in this.meshes ) {
			// reset the mesh to it's base
			this.mesh.ResetMesh();

			// and for every effector,
			foreach ( DeformationEffector effector in effectors ) {
				// apply that effector's changes.
				this.mesh.DeformMesh( effector );
			}
		//}
		/*
		// now, use our list of intersecting objects to push other objects out of the geometry.
		foreach ( KeyValuePair<int,Collision> collision in this.trackedCollisions ) {
			// we want to move the object out along its incoming velocity vector until we hit the new surface. To do this,
			// just raycast from the object's position a ways out, and find the point of impact.
			Vector3 contact_offset = collision.Value.transform.position - collision.Value.contacts[0].point;
			Vector3 ray_dir = -collision.Value.contacts[0].normal;
			Vector3 ray_pos = collision.Value.contacts[0].point - ray_dir * 5.0f;

			RaycastHit hit;
			if ( Physics.Raycast( ray_pos, ray_dir, out hit, 5.0f ) ) {
				// then, set the position of the colliding object to the new contact point, applying the offset from the initial contact.
				collision.Value.transform.position = hit.point + contact_offset;
			}
		}*/
	}

}
