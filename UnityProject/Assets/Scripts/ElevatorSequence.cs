using UnityEngine;
using System.Collections;

public class ElevatorSequence : MonoBehaviour {

	// Defines Global Variables
	public Animator elevatorAnimator;		// Reference the animator for this door
	public Animator playerAnimator;


	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			elevatorAnimator.SetTrigger ("CloseDoors");

			playerAnimator.enabled = true;
			playerAnimator.SetTrigger ("Hold");

			Invoke ("GoingUp", 2);
		}
	}

	void GoingUp() {
		elevatorAnimator.SetTrigger ("GoingUp");
		playerAnimator.SetTrigger ("GoingUp");
	}




}
