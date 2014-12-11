using UnityEngine;
using System.Collections;

public class BackgroundImg : MonoBehaviour {

	public GUITexture texture;

	// Use this for initialization
	void Start () {
		texture = GetComponent<GUITexture> ();
		if(texture!=null) {
			transform.position = Vector3.zero;
			transform.localScale = Vector3.zero;
			texture.pixelInset = new Rect(0,0,Screen.width,Screen.height);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
