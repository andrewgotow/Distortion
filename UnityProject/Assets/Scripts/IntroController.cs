using UnityEngine;
using System.Collections;

public class IntroController : MonoBehaviour {

	private float timer;
	public float fadeDuration = 2.0f;
	private AudioSource audioSrc;


	// Use this for initialization
	void Start () {
		audioSrc = GetComponent<AudioSource>();
		audioSrc.PlayDelayed (1.0f);
		timer = 0f;
	}

	void OnGUI() {
		if(GUI.Button (new Rect(Screen.width/2.2f,Screen.height/1.15f,100,50), "Skip")) {
			Application.LoadLevel (2);
		}
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if((timer % 60) > 40.0f)
			Application.LoadLevel (2);
	}
}
