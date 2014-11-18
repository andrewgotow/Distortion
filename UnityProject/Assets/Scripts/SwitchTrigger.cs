using UnityEngine;
using System.Collections;

public class SwitchTrigger : MonoBehaviour
{
	// Defines global variables
	public Animator buttonObject;
	public GameObject door1;
	public GameObject door2;

	private AudioSource a_src;

	void Start() {
		a_src = GetComponent<AudioSource>();
	}



	void OnTriggerEnter (Collider col) {
		if(a_src.clip != null) {
			a_src.Play();
		}
		buttonObject.SetInteger ("switchAnim", 1);

		door1.GetComponent<DoorTrigger> ().activeDoor = true;
		door2.GetComponent<DoorTrigger> ().activeDoor = true;
	}

	void OnTriggerExit (Collider col) {
		buttonObject.SetInteger ("switchAnim", 2);

		door1.GetComponent<DoorTrigger> ().activeDoor = false;
		door2.GetComponent<DoorTrigger> ().activeDoor = false;

		door1.GetComponentInParent <Animator> ().SetBool ("openDoor", false);
		door2.GetComponentInParent <Animator> ().SetBool ("openDoor", false);
	}



}