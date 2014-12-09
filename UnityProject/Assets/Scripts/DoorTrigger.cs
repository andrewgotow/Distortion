using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
	// Defines global variables
	public Animator doorAnimator;		// Reference the animator for this door
	public bool isActive = false;		// Is the door powered?
	private AudioSource audioSrc;		// AudioSource for Door Open SFX
	public AudioClip doorOpen;
	public AudioClip doorLocked;

	void Start() {
		if(GetComponent<AudioSource>() != null)
			audioSrc = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other) {
		if (isActive && other.tag == "Player") {
			doorAnimator.SetTrigger ("open");
			if(doorOpen != null) {
				audioSrc.clip = doorOpen;
				audioSrc.volume = 1f;
				audioSrc.dopplerLevel = 1f;
				audioSrc.Play ();
			}
		} else if(!isActive && other.tag == "Player") {
			if(doorLocked != null) {
				audioSrc.clip = doorLocked;
				audioSrc.volume = .5f;
				audioSrc.dopplerLevel = 0f;
				audioSrc.Play();
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