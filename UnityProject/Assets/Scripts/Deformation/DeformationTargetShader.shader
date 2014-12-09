Shader "Custom/Deformation Target" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	    _DistortionScale ("Distortion Scale", Range (0.0,1.0)) = 0.0
		_EmissiveTex ("Emissive (RGB)", 2D) = "white" {}
		_StripeSize ("Stripe Size", Range(0,10) ) = 5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		sampler2D _MainTex;
		float _DistortionScale;
		sampler2D _EmissiveTex;
		float _StripeSize;

		struct Input {
			float2 uv_MainTex;
            float4 color: Color; // Vertex color
			float3 worldPos;
		};

		void vert (inout appdata_full v) {
			float offset_scale = 1.0 - _DistortionScale;
			float3 offset_vec = float3( v.color.r - 0.5, v.color.g - 0.5, v.color.b - 0.5 );

			v.vertex.xyz -= offset_vec * 20.0 * offset_scale;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
         	half4 e = tex2D (_EmissiveTex, IN.uv_MainTex ) * clamp( sin( IN.worldPos.y * _StripeSize + _Time.y ), 0, 1 );

			o.Albedo = c.rgb;
			o.Emission = e.rgb;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
