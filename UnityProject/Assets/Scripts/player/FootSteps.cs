using UnityEngine;
using System.Collections;

[RequireComponent (typeof (FPController))]
public class FootSteps : MonoBehaviour {
	public AudioClip[] footsteps;
	private AudioSource audio_source;
	//private bool step = true;
	private FPController charController;
	//public float audioStepLengthWalk = 0.45f;
	//public float audioStepLengthSprint = 0.4f;
	public float stepTimer = 0f;
	public float walkStepCool = 0.55f;
	public float sprintStepCool = 0.4f;


	// Use this for initialization
	void Start () {
		charController = GetComponent<FPController>();
		AudioSource[] srcs = GetComponents<AudioSource>();
		if(srcs.Length > 0)
			audio_source = srcs[0];
	}
	
	// Update is called once per frame
	void Update () {
		if( charController.isGrounded() ) {
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

	/*void PlayStep () {
		step = false;
		audio.clip = footsteps[Random.Range(0,footsteps.Length)];
		audio.Play();
		yield return new WaitForSeconds(0.45f);
		step = true;
	}*/

	void stepAudio () {
		if(stepTimer == 0) {
			audio_source.clip = footsteps[Random.Range(0,footsteps.Length)];
			audio_source.Play ();
			if(Input.GetButton ("Sprint"))
				stepTimer = sprintStepCool;
			else
				stepTimer = walkStepCool;
		}
	}
}
