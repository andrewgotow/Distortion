using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* 
The deformation manager is a singleton class in charge of handling object deformation.
It keeps an active list of all deformation targets, and active effectors. There should 
never be more than one deformation manager in a scene, and it will automatically set up
all dependent objects.
*/
public class DeformationManager : MonoBehaviour {

	// which layer should we use as the "deformable layer". All objects in this layer will be effected.
	public LayerMask deformationLayer;
	// A list of all active deformation targets. (auto-populated)
	private List<DeformationTarget> _targets = new List<DeformationTarget>();
	// a list of all active deformation effectors. (auto-populated)
	private List<DeformationEffector> _effectors = new List<DeformationEffector>();
	// A static instance reference for the singleton pattern. 
	private static DeformationManager _instance;

	// Debug Parameters
	public bool DEBUG_liveUpdate = true;

	// This defines a getter for the instance variable. When this static variable is called, it will
	// return the currently active manager instance, allowing for singleton style access.
	public static DeformationManager instance {
		get {
			if ( _instance == null )
				_instance = Object.FindObjectOfType<DeformationManager>();
			return _instance;
		}
	}

	// When this script is first instantiated, fetch all objects in the scene marked for deformation.
	public void Awake () {
		this._targets = this.GetDeformationTargets();
	}

	// Update every physics timestep
	public void FixedUpdate () {
		// if live-updates are scheduled, deform the meshes in realtime
		if ( this.DEBUG_liveUpdate ) {
			foreach ( DeformationTarget target in this._targets ) {
				target.DeformMesh( this._effectors );
			}
		}
	}

	// This function will add an effector to the active list, adn will automatically update all deformable objects.
	// using this event-style system, we can prevent unnecessary computation. This is automatically called by subclasses of
	// deformationEffector on instantiation.
	public void AddEffector ( DeformationEffector effector ) {
		this._effectors.Add( effector );
		foreach ( DeformationTarget target in this._targets ) {
			target.DeformMesh( this._effectors );
		}
	}

	// RemoveEffector will pull an effector from the active deformer list, and will auto-update all deformable objects.
	// this is automatically called by subclasses of deformationEffector on descrution
	public void RemoveEffector ( DeformationEffector effector ) {
		this._effectors.Remove( effector );
		foreach ( DeformationTarget target in this._targets ) {
			target.DeformMesh( this._effectors );
		}
	}

	// This function will fetch all objects in the deformation layer specified, and will attach necessary scripts. Then, it
	// will add references to those scripts to the list of active deformation targets.
	private List<DeformationTarget> GetDeformationTargets () {
		List<DeformationTarget> returnList = new List<DeformationTarget>();
		Object[] objs = Object.FindObjectsOfType(typeof(GameObject));

		foreach ( GameObject obj in objs ) {
			if ( (1 << obj.layer) == this.deformationLayer.value )
				returnList.Add( (DeformationTarget)obj.AddComponent<DeformationTarget>() );
		}

		return returnList;
	}

}
