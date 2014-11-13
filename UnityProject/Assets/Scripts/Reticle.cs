using UnityEngine;
using System.Collections;

public class Reticle : MonoBehaviour {

	public Texture2D texture;
	protected Rect position;

	// Use this for initialization
	void Start () {
		position = new Rect((Screen.width-texture.width)/2,(Screen.height-texture.height)/2,texture.width,texture.height);
		//Screen.showCursor = false;
		//Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		//Color prevColor = GUI.color;
		//GUI.color = new Color(prevColor.r,prevColor.g,prevColor.b,100);
		if(texture != null)
			GUI.DrawTexture(position,texture);
	}


}
