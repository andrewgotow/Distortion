using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

	private CharacterController _controller;

	public float speed = 10.0f;
	public float jumpSpeed = 8.0f;
	private Vector3 moveVector;

	// Use this for initialization
	void Awake () {
		this._controller = this.gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if ( this._controller.isGrounded ) {
			this.moveVector = new Vector3( Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") );
			this.moveVector = this._controller.transform.TransformDirection( this.moveVector );
			this.moveVector = Vector3.ClampMagnitude( this.moveVector, 1.0f );
			this.moveVector *= this.speed;

			if ( Input.GetButton( "Jump" ) )
				this.moveVector.y = this.jumpSpeed;
		}

	    moveVector += Physics.gravity * Time.deltaTime;
		this._controller.Move( moveVector * Time.deltaTime );
		CollisionCorrection();
	}

	// ok, so here's the deal. When the ground beneath the player's feet is distorted, it's possible to pull it up
	// into the center of the collider. At this point, the character controller won't register a hit. In order to fix this,
	// Do a raycast from the player's head, down to their feet. If the ray hits something, the ground is somewhere intersecting
	// our body. Then, we just pull ourselves out of the ground, so that our feet are back at the point of contact!
	void CollisionCorrection () {
		RaycastHit hit;
		if ( Physics.Raycast( transform.position + Vector3.up * 2.0f , -Vector3.up, out hit, 1.0f ) ) {
			transform.position = hit.point;
			Debug.DrawLine( transform.position, hit.point, Color.red, 1.0f );
		}
	}


}
