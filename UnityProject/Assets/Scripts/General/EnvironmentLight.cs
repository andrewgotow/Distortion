using UnityEngine;
using System.Collections;

public class EnvironmentLight : MonoBehaviour {

	public bool isActive = true;
	private float finalIntensity = 1.0f;

	// Use this for initialization
	void Start () {
		this.finalIntensity = this.light.intensity;
	}
	
	// Update is called once per frame
	void Update () {
		if ( this.isActive == false )
			this.light.intensity = 0;
		else
			this.light.intensity = Mathf.Lerp( this.light.intensity, finalIntensity, 2.0f * Time.deltaTime );
	}

	void SetActive ( bool active ) {
		this.isActive = true;
	}

}
