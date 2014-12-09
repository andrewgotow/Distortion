using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameStateManager : MonoBehaviour {

	// A static instance reference for the singleton pattern. 
	private static GameStateManager _instance;

	// Debug Parameters
	public float maxSingularityStrength = 200.0f;
	public float maxSingularityDisplacement = 5.0f;
	public float singularityGravityMultiplier = 1.0f;
	public GameObject player;
	public GameObject checkpointMgr;

	// This defines a getter for the instance variable. When this static variable is called, it will
	// return the currently active manager instance, allowing for singleton style access.
	public static GameStateManager instance {
		get {
			if ( _instance == null )
				_instance = Object.FindObjectOfType<GameStateManager>();
			return _instance;
		}
	}

	public void Start() {
		if (checkpointMgr == null) {
			checkpointMgr = GameObject.FindGameObjectWithTag("CheckpointManager");
		}
	}

	private bool DEBUG_devToolsVisible = false;
	public bool PAUSE_menu = false;

	public void Update() {
		if(Time.timeScale != 1 && !PAUSE_menu)
			Time.timeScale = 1;
		if ( Input.GetKeyDown("`") ) {
			if(this.DEBUG_devToolsVisible) {
				this.DEBUG_devToolsVisible = false;
				Screen.lockCursor = true;
			} else {
				this.DEBUG_devToolsVisible = true;
				Screen.lockCursor = false;
			}
		}
		if(Input.GetButtonDown ("Pause")) {
			if(PAUSE_menu) {
				SetPauseMenu (false);
			}
			else{
				SetPauseMenu (true);
			}
		}
	}

	public void OnGUI () {
		if ( this.DEBUG_devToolsVisible ) {
			GUILayout.BeginArea( new Rect( 10, 10, 300, 500 ), "DEBUG", "window" );
			GUILayout.Label( "Max Singularity Strength: " + this.maxSingularityStrength );
			this.maxSingularityStrength = GUILayout.HorizontalSlider( this.maxSingularityStrength, 50, 1000 );
			GUILayout.Label( "Max Singularity Displacement: " + this.maxSingularityDisplacement );
			this.maxSingularityDisplacement = GUILayout.HorizontalSlider( this.maxSingularityDisplacement, 1, 10 );
			GUILayout.Label( "Singularity Gravity Multiplier: " + this.singularityGravityMultiplier );
			this.singularityGravityMultiplier = GUILayout.HorizontalSlider( this.singularityGravityMultiplier, 0, 2 );
			
			GUILayout.EndArea();
		}
		if(this.PAUSE_menu) {
			GUILayout.BeginArea(new Rect(300,100,500,300),"Options Menu", "window");
			if(GUI.Button(new Rect(150,30,150,50),"Resume Game")) {
				SetPauseMenu (false);
			}
			if(GUI.Button(new Rect(150,150,150,50), "Reset Game")) {
				Application.LoadLevel(1);
			}
			if(GUI.Button (new Rect(150,90,150,50), "Reload Last Checkpoint")) {
				checkpointMgr.GetComponent<CheckpointMgrScript>().Spawn();
			}
			if(GUI.Button(new Rect(150,210,150,50), "Quit Game")) {
				Application.LoadLevel(0);
			}
			GUILayout.EndArea();
		}
	}

	public void SetPauseMenu(bool b) {
		if (b) {
			PAUSE_menu = true;
			Screen.lockCursor = false;
			if(player != null) {
				player.GetComponent<FPController>().enabled = false;
				player.GetComponent<FPCamera>().enabled = false;
				player.GetComponent<PlayerFire>().enabled = false;
			}
			Time.timeScale = 0;
		} else {
			PAUSE_menu = false;
			Screen.lockCursor = true;
			if(player != null) {
				player.GetComponent<FPController>().enabled = true;
				player.GetComponent<FPCamera>().enabled = true;
				player.GetComponent<PlayerFire>().enabled = true;
			}
			Time.timeScale = 1;
		}
	}
}
