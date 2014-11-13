using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	private bool main_menu;
	private bool control_menu;
	public GameObject title_text, tg_text, control_img;
	public Font controls_font;

	void Start() {
		main_menu = true;
		control_menu = false;
	}

	void OnGUI() {
		if(main_menu) {
			if(GUI.Button(new Rect(Screen.width/2.5f,Screen.height/2,Screen.width/7,Screen.height/14), "Start Game")) {
				Application.LoadLevel (1);
			}
			if(GUI.Button(new Rect(Screen.width/2.5f,Screen.height/1.72f,Screen.width/7,Screen.height/14), "Controls")) {
				main_menu = false;
				control_menu = true;
			}
			if(GUI.Button(new Rect(Screen.width/2.5f,Screen.height/1.5f,Screen.width/7,Screen.height/14), "Quit")) {
				Application.Quit();
			}
		} else if(control_menu) {
			if(title_text && tg_text) {
				title_text.GetComponent<GUITexture>().enabled = false;
				tg_text.GetComponent<GUITexture>().enabled = false;
			}
			if(controls_font != null)
				GUI.skin.font = controls_font;
			GUI.Label(new Rect(Screen.width/2.5f,Screen.height/5f,500,200),"Player Movement:\t\tW,A,S,D");
			GUI.Label(new Rect(Screen.width/2.5f,Screen.height/3.75f,500,200),"Jump:\tSpace");
			GUI.Label(new Rect(Screen.width/2.5f,Screen.height/3f,500,200),"Sprint:\t\tShift");
			GUI.Label(new Rect(Screen.width/2.5f,Screen.height/2.5f,500,200),"Shoot Singularity:\t\tLeft Click");
			GUI.Label(new Rect(Screen.width/2.5f,Screen.height/2.1f,500,200),"Destroy Singularity:\t\tRight Click");
			GUI.Label(new Rect(Screen.width/2.5f,Screen.height/1.8f,500,200),"Options Menu:\t\tQ");
			GUI.Label(new Rect(Screen.width/2.5f,Screen.height/1.6f,500,200),"Debug Menu:\t~ (tilde)");
			if(GUI.Button(new Rect(Screen.width/2.5f,Screen.height/1.2f,Screen.width/9,Screen.height/14), "Back")) {
				if(title_text && tg_text) {
					title_text.GetComponent<GUITexture>().enabled = true;
					tg_text.GetComponent<GUITexture>().enabled = true;
				}
				if(control_img) {
					control_img.GetComponent<GUITexture>().enabled = false;
				}
				control_menu = false;
				main_menu = true;
			}
		}
	}
}
