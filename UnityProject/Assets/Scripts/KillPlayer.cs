using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour
{
	public GameObject checkpointMgr;

	void Start() {
		checkpointMgr = GameObject.FindGameObjectWithTag ("CheckpointManager");
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			if (checkpointMgr != null)
				checkpointMgr.GetComponent<CheckpointMgrScript> ().Spawn ();
			else
				Application.LoadLevel(Application.loadedLevel);
		} else if(other.gameObject.tag == "PowerCell") {
			other.gameObject.GetComponent<Powercell>().ReturnToStartPosition();
		}
	}
}
