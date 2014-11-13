using UnityEngine;
using System.Collections;

public class FootSteps : MonoBehaviour {
	public AudioClip[] footsteps;
	private AudioSource audio_source;
	//private bool step = true;
	private CharacterController charController;
	//public float audioStepLengthWalk = 0.45f;
	//public float audioStepLengthSprint = 0.4f;
	public float stepTimer = 0f;
	public float walkStepCool = 0.55f;
	public float sprintStepCool = 0.4f;
	private AudioClip lastClipPlayed;


	// Use this for initialization
	void Start () {
		charController = GetComponent<CharacterController>();
		AudioSource[] srcs = GetComponents<AudioSource>();
		if(srcs.Length > 0)
			audio_source = srcs[0];
		if(footsteps.Length > 0) {
			lastClipPlayed = footsteps[0];
		}
	}
	
	// Update is called once per frame
	void Update () {
		if( charController.isGrounded ) {
			if(Input.GetAxis ("Horizontal") > 0 || Input.GetAxis ("Horizontal") < 0 || Input.GetAxis ("Vertical") > 0 || Input.GetAxis ("Vertical") < 0) {
				//StartCoroutine(PlayStep ());
				stepAudio ();
			}
		}
		if(stepTimer > 0)
			stepTimer -= Time.deltaTime;
		if(stepTimer < 0)
			stepTimer = 0;
	}


	void stepAudio () {
		if(stepTimer == 0) {
			audio_source.clip = footsteps[Random.Range(0,footsteps.Length)];
			while(audio_source.clip == lastClipPlayed) {
				audio_source.clip = footsteps[Random.Range(0,footsteps.Length)];
			}
			audio_source.Play ();
			lastClipPlayed = audio_source.clip;
			if(Input.GetButton ("Sprint"))
				stepTimer = sprintStepCool;
			else
				stepTimer = walkStepCool;
		}
	}
}
