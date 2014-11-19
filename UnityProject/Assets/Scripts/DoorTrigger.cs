using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
	// Defines global variables
	public Animator door;				// Which door is the trigger affecting?
	public bool activeDoor = false;		// Is the door powered?
	private AudioSource audioSrc;		// AudioSource for Door Open SFX

	void Start() {
		if(GetComponent<AudioSource>() != null)
			audioSrc = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other) {
		if (activeDoor && other.tag == "Player") {
			door.SetBool ("openDoor", true);
			if(audioSrc.clip != null) {
				audioSrc.Play ();
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (activeDoor && other.tag == "Player") {
			door.SetBool ("openDoor", false);
		}
	}






}