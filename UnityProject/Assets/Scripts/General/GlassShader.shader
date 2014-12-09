Shader "Custom/Glass" {
	Properties {
		_MainTex ("Texture (RGB) alpha (A)", 2D) = "white" {}
		_Cube ("Cubemap", CUBE) = "" {}
    }
    SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		CGPROGRAM
		#pragma surface surf Lambert alpha
		struct Input {
			float2 uv_MainTex;
			float3 worldRefl;
      	};
		sampler2D _MainTex;
		samplerCUBE _Cube;
		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);

			o.Albedo = c.rgb * 0.5;
			o.Alpha = c.a;
			o.Emission = texCUBE (_Cube, IN.worldRefl).rgb;
     	}
		ENDCG
    } 
    Fallback "Diffuse"
}
