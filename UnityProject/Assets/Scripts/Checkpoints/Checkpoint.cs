using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	private GameObject checkpointMgr;
	private BoxCollider coll;
	private CheckpointMgrScript checkPtScript;
	public int CheckpointNumber;
	
	void Start () {
		if(GetComponent<Collider>() != null)
			GetComponent<Collider>().isTrigger = true;
		checkpointMgr = GameObject.FindGameObjectWithTag("CheckpointManager");
		//checkPtScript = checkpointMgr.GetComponent<CheckpointMgrScript>();
	}

	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			checkpointMgr.GetComponent<CheckpointMgrScript>().SetLastCheckpoint(this.gameObject);
			//checkpointMgr.GetComponent<CheckpointMgrScript>().Spawn ();
		}
	}

	void OnDrawGizmos () {
		Gizmos.color = new Color( 0f, 1f, 0f, 0.15f );
		Gizmos.DrawCube( GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size );
	}
}