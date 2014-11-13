using UnityEngine;
using System.Collections;

public class IntroController : MonoBehaviour {

	private float timer;
	public GUIText text;
	public float fadeDuration = 2.0f;
	private AudioSource audioSrc;


	// Use this for initialization
	void Start () {
		audioSrc = GetComponent<AudioSource>();
		audioSrc.PlayDelayed (1.5f);
		timer = 0f;
		text.font.material.color = new Color(text.font.material.color.r,
		                                     text.font.material.color.g,
		                                     text.font.material.color.b, 0f);
	}

	void OnGUI() {
		if(GUI.Button (new Rect(Screen.width/2.2f,Screen.height/1.15f,100,50), "Skip")) {
			Application.LoadLevel (2);
		}
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if((timer % 60) > 2.5f)
			StartCoroutine(StartFading());
		if((timer % 60) > 24.0f)
			Application.LoadLevel (2);
	}

	private IEnumerator StartFading() {
		yield return StartCoroutine(Fade(0.0f, 1.0f, fadeDuration));
	}

	private IEnumerator Fade (float startLevel, float endLevel, float time) {
		float speed = 1.0f/time;
		for(float t = 0.0f; t<1.0; t += Time.deltaTime*speed) {
			float a = Mathf.Lerp(startLevel, endLevel, t);
			text.font.material.color = new Color(text.font.material.color.r,
			                                     text.font.material.color.g,
			                                     text.font.material.color.b, a);
			yield return 0;
		}
	}
}
