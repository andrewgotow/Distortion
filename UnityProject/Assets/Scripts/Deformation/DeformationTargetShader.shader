Shader "Custom/Deformation Target" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	    _DistortionScale ("Distortion Scale", Range (0.0,1.0)) = 0.0
	   	_WobbleScale ( "Wobble", Range(0,0.1)) = 0.05
	   	_WobbleSpeed ( "Wobble Speed", Range(0,10) ) = 3
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		sampler2D _MainTex;
		float _DistortionScale;
		float _WobbleScale;
		float _WobbleSpeed;

		struct Input {
			float2 uv_MainTex;
            float4 color: Color; // Vertex color
		};

		void vert (inout appdata_full v) {
			float wobble_scale = _WobbleScale * sin( _Time.y * _WobbleSpeed );
			float offset_scale = 1.0 - (_DistortionScale - wobble_scale);
			float3 offset_vec = float3( v.color.r - 0.5, v.color.g - 0.5, v.color.b - 0.5 );

			v.vertex.xyz -= offset_vec * 20.0 * offset_scale;
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
