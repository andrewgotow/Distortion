using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentLight : MonoBehaviour {

	public bool isActive = true;
	private Dictionary<Light, float> lights = new Dictionary<Light,float>();

	// Use this for initialization
	void Start () {
		Light[] foundLights = this.GetComponentsInChildren<Light> ();
		foreach (Light light in foundLights) {
			lights[light] = light.intensity;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (this.isActive == false) {
			foreach( KeyValuePair<Light, float> light in lights ) {
				light.Key.intensity = 0;
			}
		} else {
			foreach( KeyValuePair<Light, float> light in lights ) {
				light.Key.intensity = Mathf.Lerp (light.Key.intensity, light.Value, 2.0f * Time.deltaTime);
			}
		}
	}

	void SetActive ( bool active ) {
		this.isActive = true;
	}

}
