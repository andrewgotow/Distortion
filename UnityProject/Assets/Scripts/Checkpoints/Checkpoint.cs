using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	private GameObject checkpointMgr;
	private BoxCollider coll;
	private CheckpointMgrScript checkPtScript;
	public int CheckpointNumber;
	
	void Start () {
		if(collider != null)
			collider.isTrigger = true;
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
}