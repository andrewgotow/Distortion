using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour
{
	// Defines global variables
	public int levelNumber;
	
	void OnTriggerEnter(Collider other)
	{
		Application.LoadLevel (levelNumber);
	}

}
