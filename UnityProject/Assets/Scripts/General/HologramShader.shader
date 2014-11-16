Shader "Custom/Hologram" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_NoiseTex ("Base (RGB)", 2D) = "white" {}
      	_Brightness ("Brightness", Range(0,1)) = 0.5
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
     	#pragma surface surf Unlit decal:add 

		sampler2D _MainTex;
		sampler2D _NoiseTex;
		float _Brightness;

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
			float4 noise = tex2D( _NoiseTex, float2(_Time.x/10,0) );
			float2 glitchUVs = IN.uv_MainTex + noise.xy * sin(IN.worldPos.x + _Time.w*100);

			float stripeSample = (4 * 6.283 * IN.worldPos.y);
			float stripeVal = abs( sin( stripeSample - _Time.w ));

			half4 c = tex2D (_MainTex, glitchUVs);
         	
			o.Albedo = c.rgb * clamp( stripeVal, 0.25, 1.0 ) * _Brightness;
			o.Alpha = c.a;
		}


		ENDCG
	} 
	FallBack "Diffuse"
}
