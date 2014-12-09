using UnityEngine;
using System.Collections;

public class SwitchTrigger : MonoBehaviour
{
	// Defines global variables
	//public Animator doorAnimator;
	//public GameObject door1;
	//public GameObject door2;

	public GameObject[] poweredObjects;
	private GameObject powerCell;

	//private AudioSource a_src;

	void Start() {
		//a_src = GetComponent<AudioSource>();
	}


	void Update () {
		if ( powerCell != null ) {
			powerCell.transform.position = Vector3.Lerp( powerCell.transform.position, transform.position + Vector3.up * (1.5f + Mathf.Sin(Time.time) * 0.5f), Time.deltaTime );
			powerCell.transform.Rotate( Vector3.up * 10 * Time.deltaTime );
		}
	}


	void OnTriggerEnter (Collider col) {
		if ( col.tag == "PowerCell" ) {
			audio.Play();
 			
			foreach ( GameObject obj in poweredObjects ) {
				obj.BroadcastMessage( "SetActive", true, SendMessageOptions.DontRequireReceiver );
			}

			powerCell = col.gameObject;
 			powerCell.rigidbody.isKinematic = true;

			col.enabled = false;
 		}
		
		//doorAnimator.SetInteger ("doorAnim", 1);

		//door1.GetComponent<DoorTrigger> ().activeDoor = true;
		//door2.GetComponent<DoorTrigger> ().activeDoor = true;
	}
	/*
	void OnTriggerExit (Collider col) {
		//doorAnimator.SetInteger ("doorAnim", 2);

		foreach ( GameObject obj in poweredObjects ) {
			obj.BroadcastMessage( "SetActive", false, SendMessageOptions.DontRequireReceiver );
		}

		//door1.GetComponent<DoorTrigger> ().activeDoor = false;
		//door2.GetComponent<DoorTrigger> ().activeDoor = false;

		//door1.GetComponentInParent <Animator> ().SetBool ("openDoor", false);
		//door2.GetComponentInParent <Animator> ().SetBool ("openDoor", false);
	}
	*/
	void OnDrawGizmos () {
		Gizmos.color = new Color( 1, 0, 0, 0.5f );
		foreach ( GameObject obj in poweredObjects ) {
			if ( obj != null )
				Gizmos.DrawLine( transform.position, obj.transform.position );
		}
	}



}