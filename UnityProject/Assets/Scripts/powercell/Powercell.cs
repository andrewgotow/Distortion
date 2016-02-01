using UnityEngine;
using System.Collections;

public class Powercell : MonoBehaviour {

	public Vector3 startPosition;
	public bool respawnOnKillbox;

	// Use this for initialization
	void Start () {
		startPosition = this.transform.position;
		//respawnOnKillbox = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ReturnToStartPosition() {
		if(respawnOnKillbox) {
			this.gameObject.transform.position = startPosition;
			this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, 0);
			this.gameObject.GetComponent<Rigidbody>().rotation = new Quaternion (0, 0, 0, 0);
		}
	}
}
