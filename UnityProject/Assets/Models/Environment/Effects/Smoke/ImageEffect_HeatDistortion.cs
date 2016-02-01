using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Displacement/HeatDistortion")]
public class ImageEffect_HeatDistortion : MonoBehaviour {
	public Camera NormalCamera;
	public float magnitude = 0.1f;
	private Material _mat;
	
	void Start () {
		NormalCamera.targetTexture = new RenderTexture( (int)NormalCamera.pixelWidth, (int)NormalCamera.pixelHeight, 16, RenderTextureFormat.ARGBHalf );
		
		Shader shader = Shader.Find( "Hidden/HeatDistortion" );
		_mat = new Material( shader );
		_mat.SetFloat( "Magnitude", magnitude );
		_mat.SetTexture( "_DistortionTex", NormalCamera.targetTexture );
	}

    void OnRenderImage(RenderTexture src, RenderTexture dest) {
        Graphics.Blit(src, dest, _mat);
    }
}
