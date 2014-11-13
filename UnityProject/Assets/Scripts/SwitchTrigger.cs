using UnityEngine;
using System.Collections;

public class SwitchTrigger : MonoBehaviour
{
	// Defines global variables
	public Animator buttonObject;
	public Animator doorObject;

	private AudioSource a_src;

	void Start() {
		a_src = GetComponent<AudioSource>();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(a_src.clip != null) {
			a_src.Play();
		}
		buttonObject.SetInteger ("switchAnim", 1);
		doorObject.SetInteger ("doorAnim", 1);
	}

	void OnTriggerExit(Collider other)
	{
		buttonObject.SetInteger ("switchAnim", 2);
		doorObject.SetInteger ("doorAnim", 2);
	}




}