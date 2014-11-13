using UnityEngine;
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

	public CharacterController characterController;

	void Start () {
		this.characterController = this.GetComponent<CharacterController>();
	}


	void Update () {		
		transform.Rotate( 0, Input.GetAxis( _input_axis_rotateY ) * _input_rotateY_scale, 0 );

		_rotation_target.y = transform.eulerAngles.y;
		_rotation_target.x = Mathf.Clamp( _rotation_target.x + -Input.GetAxis( _input_axis_rotateX ) * _input_rotateX_scale, -80, 80 );
	}


	void OnControllerColliderHit(ControllerColliderHit collision) {
		if ( !this.characterController.isGrounded ) 
			_position_velocity.y -= this.characterController.velocity.y;
		_position_velocity = Vector3.ClampMagnitude( _position_velocity, _max_offset_velocity );
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


		_camera.rotation = Quaternion.Lerp( _camera.rotation, Quaternion.Euler( _rotation_target ), _rotation_speed * Time.deltaTime );
	}
}
