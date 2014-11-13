using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {

	float speed = 20.0f;
	float time = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		transform.Translate (Vector3.forward * speed * Time.deltaTime, Space.Self);
		if(time > 3.0f)
		{
			Destroy(gameObject);
		}
	}

}
