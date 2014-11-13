Shader "Custom/Deformation Target" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	    _DistortionScale ("Distortion Scale", Range (0.0,1.0)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		sampler2D _MainTex;
		float _DistortionScale;

		struct Input {
			float2 uv_MainTex;
            float4 color: Color; // Vertex color
		};

		void vert (inout appdata_full v) {
			v.vertex.xyz -= float3( v.color.r - 0.5, v.color.g - 0.5, v.color.b - 0.5 ) * 20.0 * (1.0-_DistortionScale);
		}

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
