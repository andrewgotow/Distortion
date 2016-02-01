using UnityEngine;
using System.Collections;

//[RequireComponent (typeof (Rigidbody))]
//[RequireComponent (typeof (CapsuleCollider))]
public class FPController : MonoBehaviour {

	public float move_speed = 5.0f;
	public float sprint_speed = 8.0f;
	public float jump_speed = 2.0f;

	public AudioClip[] landSounds;
	public AudioClip[] jumpSounds;

	private Vector3 movementVector;
	private CharacterController characterController;

	void Start () {
		this.movementVector = Vector3.zero;
		this.characterController = this.GetComponent<CharacterController>();
	}

	void Update () {
		// Read the input axes and create an input vector that is clamped/normalized.
		Vector3 inputVector = new Vector3( Input.GetAxis( "Horizontal" ), 0.0f, Input.GetAxis( "Vertical" ) );
		inputVector = this.transform.TransformDirection( inputVector );
		inputVector = Vector3.ClampMagnitude( inputVector, 1.0f );

		this.movementVector.y += Physics.gravity.y * Time.deltaTime;

		if ( Input.GetButton("Sprint") ) {
			this.movementVector.x = inputVector.x * this.sprint_speed;
			this.movementVector.z = inputVector.z * this.sprint_speed;
		}else{
			this.movementVector.x = inputVector.x * this.move_speed;
			this.movementVector.z = inputVector.z * this.move_speed;
		}

		if ( this.characterController.isGrounded ) {
			this.movementVector.y = 0;

			if (Input.GetButton("Jump") ) {
				this.movementVector.y = this.jump_speed;
				GetComponent<AudioSource>().clip = jumpSounds[Random.Range(0,jumpSounds.Length)];
				GetComponent<AudioSource>().pitch = Random.Range( 0.9f, 1.1f );
				GetComponent<AudioSource>().Play();
			}
		}

		this.characterController.Move( this.movementVector * Time.deltaTime );
	}

}
