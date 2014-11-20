using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
	// Defines global variables
	public Animator doorAnimator;		// Reference the animator for this door
	public bool isActive = false;		// Is the door powered?
	private AudioSource audioSrc;		// AudioSource for Door Open SFX

	void Start() {
		if(GetComponent<AudioSource>() != null)
			audioSrc = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other) {
		if (isActive && other.tag == "Player") {
			doorAnimator.SetTrigger ("open");
			if(audioSrc.clip != null) {
				audioSrc.Play ();
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (isActive && other.tag == "Player") {
			doorAnimator.SetTrigger ("close");
		}
	}


	void SetActive ( bool active ) {
		this.isActive = true;
	}






}