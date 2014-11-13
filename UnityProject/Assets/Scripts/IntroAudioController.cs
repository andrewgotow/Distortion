using UnityEngine;
using System.Collections;

public class IntroAudioController : MonoBehaviour {

	private AudioSource audioSrc;

	// Use this for initialization
	void Start () {
		audioSrc = GetComponent<AudioSource>();
		audioSrc.PlayDelayed(3.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
