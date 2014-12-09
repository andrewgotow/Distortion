using UnityEngine;
using System.Collections;

public class AnimationEventForwarder : MonoBehaviour {

	public void CallEventHandler ( string function ) {
		gameObject.SendMessageUpwards ( function );
	}

}
