using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
	// Defines global variables
	public Animator elevatorAnimator;

	void OnTriggerEnter(Collider other)
	{
		gameObject.collider.enabled = false;
		elevatorAnimator.SetBool ("moveUp", true);
	}
}