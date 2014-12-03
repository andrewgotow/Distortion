using UnityEngine;
using System.Collections;

public class CheckpointMgrScript : MonoBehaviour {

	public GameObject player;
	public GameObject[] checkpointList;
	private GameObject _currentCheckpoint;
	private int _currCheckpointNum;
	private GameObject gameStateMgr;

	// Use this for initialization
	void Start () {
		_currCheckpointNum = 1;
		if (checkpointList.Length > 0) {
			_currentCheckpoint = checkpointList[0];
		}
		gameStateMgr = GameObject.Find("GameStateManager");
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetLastCheckpoint(GameObject cp) {
		if (cp.GetComponent<Checkpoint> () != null) {
			if(cp.GetComponent<Checkpoint>().CheckpointNumber > _currCheckpointNum) {
				_currentCheckpoint = cp;
				_currCheckpointNum = cp.GetComponent<Checkpoint>().CheckpointNumber;
				//AddCheckpoint(cp);
			}
		}
	}

	public void Spawn() {
		if (player != null) {
			if(_currentCheckpoint != null) {
				player.transform.position = _currentCheckpoint.transform.position;
				gameStateMgr.GetComponent<GameStateManager>().SetPauseMenu (false);
			}
		}
	}

	public void AddCheckpoint(GameObject cp) {
		int l = checkpointList.Length;
		checkpointList [l + 1] = cp;
	}
}