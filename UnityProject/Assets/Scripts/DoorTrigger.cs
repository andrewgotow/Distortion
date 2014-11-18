using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
	// Defines global variables
	public Animator door;				// Which door is the trigger affecting?
	public bool activeDoor = false;		// Is the door powered?

	void OnTriggerEnter(Collider other) {
		if (activeDoor && other.tag == "Player") {
			door.SetBool ("openDoor", true);
		}
	}

	void OnTriggerExit(Collider other) {
		if (activeDoor && other.tag == "Player") {
			door.SetBool ("openDoor", false);
		}
	}






}