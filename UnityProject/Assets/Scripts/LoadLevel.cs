using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour
{
	// Defines global variables
	public string levelString;
	
	void OnTriggerEnter(Collider other)
	{
		Application.LoadLevel (levelString);
	}

}
