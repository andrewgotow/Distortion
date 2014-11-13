using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}
