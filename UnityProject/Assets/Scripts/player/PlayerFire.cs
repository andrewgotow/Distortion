using UnityEngine;
using System.Collections;

public class PlayerFire : MonoBehaviour {
	
	private Transform _camera;
	private GameObject _singularity;
	public AudioClip fire_audio;

	public GameObject singularityPrefab;

	// Use this for initialization
	void Awake () {
		this._camera = this.gameObject.GetComponentInChildren<Camera>().transform;
	}

	// Update is called once per frame
	void Update () {
		if ( Input.GetButtonDown ( "Fire2" ) ) {
			if(this._singularity != null) {
				Destroy( this._singularity );
			}
		}

		if ( Input.GetButtonDown ( "Fire1" ) ) {
			Destroy( this._singularity );

			RaycastHit hit;

			if ( Physics.Raycast( this._camera.position, this._camera.forward, out hit ) ) {
				this._singularity = (GameObject)GameObject.Instantiate( this.singularityPrefab, hit.point + hit.normal, Quaternion.LookRotation( hit.normal) );
				AudioSource.PlayClipAtPoint( fire_audio, transform.position );
			}
		}
	}
}
