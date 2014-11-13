using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]
public class FPController : MonoBehaviour {

	// Movement parameters
	public float _move_acceleration = 1.0f;
	public float _move_damping = 0.15f;
	public float _move_airSpeed = 0.1f;
	public float _sprint_acceleration = 1.5f;

	// Jumping parameters
	public float _jump_force = 5.0f;
	public float _jump_hold = 0.1f;

	// Physics parameters
	public float _physics_mass = 70.0f;
	public float _physics_push = 1.0f;
	public float _physics_slide = 0.1f;

	// input parameters.
	public string _input_axis_vertical = "Vertical";
	public string _input_axis_horizontal = "Horizontal";
	public string _input_button_jump = "Jump";
	public string _input_button_sprint = "Sprint";

	// other variables.
	private bool _grounded = false;
	private AudioSource a_source;
	public AudioClip[] jump_grunts;


	void Start () {
		// setup the rigidbody.
		rigidbody.freezeRotation = true;	
		rigidbody.mass = _physics_mass;

		AudioSource[] srcs = GetComponents<AudioSource>();
		if(srcs.Length > 2)
			a_source = srcs[2];
	}
	
	void Update () {
		// Read the input axes and create an input vector that is clamped/normalized.
		Vector3 inputVector = new Vector3( Input.GetAxis( _input_axis_horizontal ), 0.0f, Input.GetAxis( _input_axis_vertical ) );
		inputVector = Vector3.ClampMagnitude( inputVector, 1.0f );

		// Calculate the acceleration to apply to our controller
		Vector3 velocityChange;
		if(Input.GetButton (_input_button_sprint) && inputVector.z > 0) {
			velocityChange = inputVector * _sprint_acceleration;
		} else {
			velocityChange = inputVector * _move_acceleration;
		}

		if ( !_grounded )
			velocityChange *= _move_airSpeed;

		// Damp the velocity when grounded (to simulate custom friciton)
		if ( _grounded )
			rigidbody.AddForce( -rigidbody.velocity * _move_damping, ForceMode.VelocityChange );

		// and apply it.
		rigidbody.AddRelativeForce( velocityChange, ForceMode.VelocityChange );
		Debug.DrawLine( transform.position, transform.position + velocityChange, Color.red );

		// Now handle jumps.
		if ( _grounded ) {
			if ( Input.GetButtonDown( _input_button_jump ) ) {
				rigidbody.AddRelativeForce( Vector3.up * _jump_force, ForceMode.VelocityChange );
				if(a_source != null) {
					a_source.clip = jump_grunts[Random.Range(0,jump_grunts.Length)];
					a_source.Play();
				}
			}
		}else{
			if ( Input.GetButton( _input_button_jump ) )
				rigidbody.AddRelativeForce( Vector3.up * _jump_hold, ForceMode.VelocityChange );
		}

		// refresh the _grounded property.
		CheckGrounded();
	}

	void CheckGrounded () {
		RaycastHit hit;
		if ( Physics.Raycast( collider.bounds.center, -Vector3.up, out hit, collider.bounds.extents.y + 0.25f ) ) {
			_grounded = true;
			Debug.DrawLine( hit.point, hit.point + hit.normal, Color.red );
		}else{
			_grounded = false;
		}
	}

	public bool isGrounded () {
		return this._grounded;
	}
}
