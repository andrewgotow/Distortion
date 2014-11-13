using UnityEngine;
using System.Collections;

public class SingularityDeath : MonoBehaviour {

	// Use this for initialization
	void Awake() {
		StartCoroutine("LifeSpan");
	}

	IEnumerator LifeSpan() {
		yield return new WaitForSeconds(3);
		Destroy (this.gameObject);
	}
}
