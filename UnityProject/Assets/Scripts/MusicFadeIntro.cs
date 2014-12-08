using UnityEngine;
using System.Collections;

public class MusicFadeIntro : MonoBehaviour {

	private AudioSource audioS;
	private float timer;

	// Use this for initialization
	void Start () {
		timer = 0f;
		audioS = GetComponent<AudioSource>();
		audioS.volume = 0f;
		audioS.Play ();
		StartCoroutine (FadeIn (3.0f, 0.4f));

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if((timer % 60) > 37.0f)
			StartCoroutine(FadeOut());
	}

	IEnumerator FadeIn(float fadeTime, float vol) {
		float speed = 1.0f / fadeTime;
		for(float t = 0.0f; t<1.0f; t+=Time.deltaTime*speed) {
			float newVol = Mathf.Lerp(0.0f, vol, t);
			audioS.volume = newVol;
			yield return 0;
		}
	}

	IEnumerator FadeOut() {
		if(audioS.volume > 0.1f) {
			audioS.volume -= 0.1f*Time.deltaTime;
		}
		yield return 0;
	}
}
