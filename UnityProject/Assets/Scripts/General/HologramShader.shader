Shader "Custom/Hologram" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_NoiseTex ("Base (RGB)", 2D) = "white" {}
      	_Brightness ("Brightness", Range(0,1)) = 0.5
		_GlitchAmount ( "Giltch", Range(0,1)) = 0.1
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
     	#pragma surface surf Unlit decal:add 

		sampler2D _MainTex;
		sampler2D _NoiseTex;
		float _Brightness;
		float _GlitchAmount;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		float rand(float3 co)
		{
		    return frac(sin( dot(co.xyz ,float3(12.9898,78.233,45.5432) )) * 43758.5453);
		}

		half4 LightingUnlit(SurfaceOutput s, half3 lightDir, half atten)
    	{
         	return half4(s.Albedo, s.Alpha);
    	}

		void surf (Input IN, inout SurfaceOutput o) {
			float2 glitch_vec = _GlitchAmount * tex2D( _NoiseTex, float2(_Time.x, 0) ).xyz;
			float2 glitch_offset = glitch_vec * rand( float3(IN.worldPos.x, IN.worldPos.y, _Time.w) );
			
			float stripe = 1.0 - clamp( tan( IN.worldPos.y * 50 - _Time.w ), 0, 0.45 );

			half4 c = tex2D (_MainTex, IN.uv_MainTex + glitch_offset ) * stripe;
         	
			o.Albedo = c.rgb * _Brightness;
			o.Alpha = c.a;
		}


		ENDCG
	} 
	FallBack "Diffuse"
}
