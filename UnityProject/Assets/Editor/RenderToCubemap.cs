using UnityEngine;
using UnityEditor;
using System.Collections;

// Render scene from a given point into a static cube map.
// Place this script in Editor folder of your project.
// Then use the cubemap with one of Reflective shaders!
public class RenderCubemapWizard : ScriptableWizard {
	public Transform renderFromPosition;
	public Cubemap cubemap;
	
	public void OnWizardUpdate () {
		string helpString = "Select transform to render from and cubemap to render into";
		bool isValid = (renderFromPosition != null) && (cubemap != null);
	}
	
	public void OnWizardCreate () {
		// create temporary camera for rendering
		GameObject go = new GameObject( "CubemapCamera", typeof(Camera) );
		// place it on the object
		go.transform.position = renderFromPosition.position;
		go.transform.rotation = Quaternion.identity;
		// render into cubemap		
		go.camera.RenderToCubemap( cubemap );
		
		// destroy temporary camera
		DestroyImmediate( go );
	}
	
	[MenuItem("GameObject/Render into Cubemap")]
	static void RenderCubemap () {
		ScriptableWizard.DisplayWizard<RenderCubemapWizard>(
			"Render cubemap", "Render!");
	}
}