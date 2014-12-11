using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	private bool main_menu, control_menu, options_menu;
	public Font controls_font;
	public Texture StartGameBtn, ControlBtn, OptionsBtn, QuitBtn;
	public GUIStyle guiStyleStart, guiStyleControls, guiStyleOptions, guiStyleQuit, guiStyleBack;

	void Start() {
		main_menu = true;
		control_menu = false;
	}

	void OnGUI() {
		if(main_menu) {
			if(GUI.Button(new Rect(Screen.width/2.5f,Screen.height/2,Screen.width/7,Screen.height/14), "", guiStyleStart)) {
				Application.LoadLevel (1);
			}
			if(GUI.Button(new Rect(Screen.width/2.5f,Screen.height/1.62f,Screen.width/7,Screen.height/14), "", guiStyleControls)) {
				main_menu = false;
				control_menu = true;
			}/*
			if(GUI.Button(new Rect(Screen.width/2.5f,Screen.height/1.4f,Screen.width/7,Screen.height/14), "", guiStyleOptions)) {
				Application.Quit();
			}*/
			if(GUI.Button(new Rect(Screen.width/2.5f,Screen.height/1.35f,Screen.width/7,Screen.height/14), "", guiStyleQuit)) {
				Application.Quit();
			}
		} else if(control_menu) {
			if(controls_font != null) {
				GUI.skin.font = controls_font;
			}
			// Left Side
			GUI.Label(new Rect(Screen.width/4f,Screen.height/2.1f,500,200),"Player Movement:\t\tW,A,S,D");
			GUI.Label(new Rect(Screen.width/4f,Screen.height/1.8f,500,200),"Jump:\t\tSpace");
			GUI.Label(new Rect(Screen.width/4f,Screen.height/1.6f,500,200),"Sprint:\t\tShift");
			GUI.Label(new Rect(Screen.width/4f,Screen.height/1.4f,500,200),"Shoot Singularity:\t\tLeft Click");
			// Right Side
			GUI.Label(new Rect(Screen.width/2f,Screen.height/2.1f,500,200),"Destroy Singularity:\t\tRight Click");
			GUI.Label(new Rect(Screen.width/2f,Screen.height/1.8f,500,200),"Options Menu:\t\tQ");
			GUI.Label(new Rect(Screen.width/2f,Screen.height/1.6f,500,200),"Debug Menu:\t\t~ (tilde)");
			if(GUI.Button(new Rect(Screen.width/2.5f,Screen.height/1.2f,Screen.width/9,Screen.height/14), "", guiStyleBack)) {
				control_menu = false;
				main_menu = true;
			}
		} else if(options_menu) {

		}
	}
}
