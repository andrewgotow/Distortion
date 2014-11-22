Shader "Custom/PowerCore" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_EmissiveTex ("Emissive (RGB)", 2D) = "white" {}
		_StripeSize ("Stripe Size", Range(0,10) ) = 5
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
     	#pragma surface surf Lambert

		sampler2D _MainTex;
		sampler2D _EmissiveTex;
		float _StripeSize;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex );
         	half4 e = tex2D (_EmissiveTex, IN.uv_MainTex ) * clamp( sin( IN.worldPos.y * _StripeSize + _Time.y ), 0, 1 );

			o.Albedo = c.rgb;
			o.Emission = e.rgba;
		}


		ENDCG
	} 
	FallBack "Diffuse"
}
