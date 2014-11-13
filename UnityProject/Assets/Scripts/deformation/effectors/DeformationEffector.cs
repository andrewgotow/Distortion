﻿using UnityEngine;
using System.Collections;

public abstract class DeformationEffector : MonoBehaviour {

	public void Start () {
		DeformationManager.instance.AddEffector( this );
	}

	public virtual void OnDestroy () {
		if ( DeformationManager.instance != null )
			DeformationManager.instance.RemoveEffector( this );
	}

	public virtual bool ObjectInRange ( Transform obj ) {
		return true;
	}

	public virtual bool VertexInRange ( Vector3 ms_position, Vector3 vertex ) {
		return true;
	} 

	public virtual Vector3 TransformVertex ( Vector3 ms_position, Vector3 vertex, float weight = 1.0f ) {
		return vertex;
	}

}

