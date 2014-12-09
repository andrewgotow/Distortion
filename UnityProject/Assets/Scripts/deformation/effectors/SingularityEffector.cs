using UnityEngine;
using System.Collections;

public class SingularityEffector : DeformationEffector {

	private float gravitationalForce = 98f;
	private float gravitationalRadius = 10.0f;
	private AudioSource audio_source;
	public AudioClip idle_audio;
	public AudioClip destroyAudio;

	public void Awake() {
		audio_source = GetComponent<AudioSource>();
		if (idle_audio != null) {
			audio_source.clip = idle_audio;
			audio_source.loop = true;
			audio_source.Play ();
		}
	}

	public override bool ObjectInRange ( Transform obj ) {
		return (this.transform.position - obj.position).sqrMagnitude < 2500.0f;
	}

	public override void DestroyEffector () {
		AudioSource.PlayClipAtPoint( destroyAudio, transform.position );
		Destroy( gameObject );
	}

	/* This function has been combined with "transform vertex" for efficiency's sake. 
	We're already performing the vector subtraction */
	/*public override bool VertexInRange ( Vector3 ms_position, Vector3 vertex ) {
		return (ms_position - vertex).sqrMagnitude < 1000.0f;
	}*/

	public override Vector3 TransformVertex ( Vector3 ms_position, Vector3 vertex, float weight = 1.0f ) {
		float offsetMag = (ms_position - vertex).sqrMagnitude;

		if ( offsetMag < 1000.0f ) {
			float strength = GameStateManager.instance.maxSingularityStrength;
			float maxOffset = GameStateManager.instance.maxSingularityDisplacement;

			float mag = strength / offsetMag;
			return Vector3.MoveTowards( vertex, ms_position, Mathf.Min( mag * weight, maxOffset ) );
		}
		
		return vertex;
	}

	public void FixedUpdate () {
		// fetch objects within a given radius.
		Collider[] others = Physics.OverlapSphere( this.transform.position, this.gravitationalRadius );
		foreach ( Collider other in others ) {
			if ( other.tag != "Player" ) {
				if ( other.rigidbody ) {
					Vector3 offsetVec = (this.transform.position - other.transform.position);
					// there's an issue right now where things will get too close to the center of the singularity, and will move way too fast.
					// just check that it's not too close here.
					if ( offsetVec.sqrMagnitude > 1.0f )
						other.rigidbody.AddForce( GameStateManager.instance.singularityGravityMultiplier * offsetVec.normalized * this.gravitationalForce / Mathf.Min(offsetVec.sqrMagnitude, 1.0f), ForceMode.Acceleration );
				}
			}
		}
	}
}