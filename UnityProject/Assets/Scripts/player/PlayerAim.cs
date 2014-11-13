using UnityEngine;
using System.Collections;

public class PlayerAim : MonoBehaviour {

	private Transform _camera;
	private float aim_y = 0;

	public GameObject Arm_model;
	public float speedX = 2.0f;
	public float speedY = -2.0f;

	// Use this for initialization
	void Awake () {
		this._camera = this.gameObject.GetComponentInChildren<Camera>().transform;
	}
	
	// Update is called once per frame
	void Update () {
		this._camera.rotation = this.transform.rotation * Quaternion.Euler( this.aim_y, 0, 0 );
		this.transform.Rotate( 0, Input.GetAxis( "Mouse X" ) * speedX, 0 );
		this.aim_y = Mathf.Clamp( this.aim_y + Input.GetAxis( "Mouse Y" ) * speedY, -80, 80 );
	}
}
