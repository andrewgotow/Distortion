using UnityEngine;
using System.Collections;

public class PowercellSound : MonoBehaviour {
	public AudioClip[] clips;
	private AudioSource audioSrc;
	public float minAngle = 5;
	public float maxAngle = 95;

	void Start() {
		audioSrc = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col) {
		float vol = 1.0f;
		if (col.relativeVelocity.magnitude > 8) {
			Vector3 normal = col.contacts[0].normal;
			Vector3 vel = GetComponent<Rigidbody>().velocity;
			// measure angle
			float angle = Vector3.Angle (vel, -normal);
			if(angle > 89.9) {
				if(col.relativeVelocity.magnitude < 10)
					vol = 0.6f;
				else if(col.relativeVelocity.magnitude < 12)
					vol = .8f;
				else
					vol = 1.0f;
				audioSrc.clip = clips[Random.Range(0,clips.Length)];
				audioSrc.PlayOneShot (audioSrc.clip, vol);
				//Debug.Log (col.gameObject.name+" : "+angle+" : "+col.relativeVelocity.magnitude);
				//Debug.Log (col.relativeVelocity.magnitude);
			}
		}	
	}

}
