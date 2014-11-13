using UnityEngine;
using System.Collections;

public class TurretMovement : MonoBehaviour {


	public Transform target;
	public Transform turret;
	public Transform bullet;
	public Transform bulletSpawn;

	Quaternion rotate;
	float timer;

	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
	

	}

	void OnTriggerStay (Collider objectTriggered)
	{
		if (objectTriggered.transform == target) {

			timer += Time.deltaTime;
			rotate = Quaternion.LookRotation (target.position - turret.position);
			turret.rotation = Quaternion.Slerp (turret.rotation, rotate, Time.deltaTime * 2);

			if (timer > 2.0f) {
				Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);
				timer = 0.0f;
			}
		}

	}
}
