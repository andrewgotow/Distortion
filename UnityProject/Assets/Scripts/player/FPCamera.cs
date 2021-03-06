﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (FPController))]
public class FPCamera : MonoBehaviour {

	// These properties are used to bounce the main camera around.
	public Vector3 _position_target;			// The desired location of the camera (relative to the player object)
	public float _position_spring = 4.0f;		// The spring force applied to the camera to restore its position
	public float _position_damping = 0.25f;		// The damping applied to the camera position spring

	private Vector3 _position_velocity;			// A value to keep track of the current velocity of the camera
	private Vector3 _position_offset;			// A value to keep track of the local position of the camera (straight interpolation causes unwanted side effects)

	// This handles the imapact effects.
	public float _max_offset = 0.5f;
	public float _max_offset_velocity = 20.0f;

	// And these handle orientation
	public Vector3 _rotation_target;			// The desired rotation of the camera (global)
	public float _rotation_speed = 2.0f;		// The speed at which the camera rotates towards the desired orientation (lerp multiplier)

	public Transform _camera;					// The camera transform itself.

	// These are just input modifiers.
	public string _input_axis_rotateY = "Mouse X";
	public string _input_axis_rotateX = "Mouse Y";
	public float _input_rotateY_scale = 4.0f;
	public float _input_rotateX_scale = 4.0f;

	private Vector3 _firstPersonSway = Vector3.zero;
	private Vector3 _firstPersonSwayVelocity = Vector3.zero;
	public Transform _firstPersonModel;
	private float _firstPersonSwaySpring = 75.0f;
	private float _firstPersonSwayDamping = 0.175f;
	private float _maxFirstPersonSwayOffset = 15.0f;

	private float fall_duration = 0;
	public CharacterController characterController;

	void Start () {
		this.characterController = this.GetComponent<CharacterController>();
		Screen.lockCursor = true;
	}


	void Update () {		
		transform.Rotate( 0, Input.GetAxis( _input_axis_rotateY ) * _input_rotateY_scale, 0 );

		_firstPersonSwayVelocity.y -= _input_rotateY_scale * Input.GetAxis( _input_axis_rotateY );
		_firstPersonSwayVelocity.x += _input_rotateX_scale * Input.GetAxis( _input_axis_rotateX );
		_firstPersonSway += _firstPersonSwayVelocity * Time.deltaTime;
		_firstPersonSwayVelocity +=  _firstPersonSwaySpring * -_firstPersonSway * Time.deltaTime;
		_firstPersonSwayVelocity *= (1.0f - _firstPersonSwayDamping);

		_rotation_target.y = transform.eulerAngles.y;
		_rotation_target.x = Mathf.Clamp( _rotation_target.x + -Input.GetAxis( _input_axis_rotateX ) * _input_rotateX_scale, -90, 90 );
	
		if ( !this.characterController.isGrounded ) {
			this.fall_duration += Time.deltaTime;
		}
	}


	void OnControllerColliderHit(ControllerColliderHit collision) {
		this._position_velocity.y += this.fall_duration * 2.0f;
		this.fall_duration = 0;
		this._position_velocity = Vector3.ClampMagnitude( _position_velocity, _max_offset_velocity );
	}
	
	void LateUpdate () {
		// Damp the translational velocity
		_position_velocity *= 1.0f - _position_damping;	
		// Apply the translational spring force			
		_position_velocity += _position_offset * _position_spring;
		// Update the position offset
		_position_offset = Vector3.ClampMagnitude( _position_offset - _position_velocity * Time.deltaTime, _max_offset );
		// Update the camera transform.
		_camera.position = transform.TransformPoint( _position_target + _position_offset );

		_camera.rotation = Quaternion.Euler( _rotation_target );
	
		_firstPersonModel.transform.rotation = _camera.rotation * Quaternion.Euler( Vector3.ClampMagnitude(_firstPersonSway, _maxFirstPersonSwayOffset) );
	}
}
