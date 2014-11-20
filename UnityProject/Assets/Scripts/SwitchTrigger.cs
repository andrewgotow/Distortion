using UnityEngine;
using System.Collections;

public class SwitchTrigger : MonoBehaviour
{
	// Defines global variables
	//public Animator doorAnimator;
	//public GameObject door1;
	//public GameObject door2;

	public GameObject[] poweredObjects;

	private AudioSource a_src;

	void Start() {
		a_src = GetComponent<AudioSource>();
	}



	void OnTriggerEnter (Collider col) {
		if(a_src.clip != null) {
			a_src.Play();
		}
		
		foreach ( GameObject obj in poweredObjects ) {
			obj.BroadcastMessage( "SetActive", true, SendMessageOptions.DontRequireReceiver );
		}
		//doorAnimator.SetInteger ("doorAnim", 1);

		//door1.GetComponent<DoorTrigger> ().activeDoor = true;
		//door2.GetComponent<DoorTrigger> ().activeDoor = true;
	}

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

	void OnDrawGizmos () {
		Gizmos.color = new Color( 1, 0, 0, 0.5f );
		foreach ( GameObject obj in poweredObjects ) {
			Gizmos.DrawLine( transform.position, obj.transform.position );
		}
	}



}