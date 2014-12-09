using UnityEngine;
using System.Collections;

public class SpinningPipe : MonoBehaviour {

	public int sequenceState = 0;

	private Animator spinningPipeAnimator;
	public ParticleSystem secondaryParticles;
	public ParticleSystem mainParticles;


	// Use this for initialization
	void Start () {
		spinningPipeAnimator = gameObject.GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void SetActive ( bool active ) {
		sequenceState++;
		Debug.Log (sequenceState);

		pipeSequence ();
	}

	void pipeSequence () {
		if (sequenceState == 1) {
			spinningPipeAnimator.SetTrigger ("Active");
		}

		else if (sequenceState == 2) {
			secondaryParticles.enableEmission = true;
		}

		else if (sequenceState == 3) {
			mainParticles.enableEmission = true;
		}

	}
}
