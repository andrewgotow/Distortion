Shader "Hidden/HeatDistortion" {
	Properties {
		_MainTex ("Render Input", 2D) = "white" {}
		_DistortionTex ("Normal Map", 2D) = "white" {}
		_Magnitude ("Magnitude", Range(0,1) ) = 0.1
	}
	SubShader {
		ZTest Always Cull Off ZWrite Off Fog { Mode Off }
		Pass {
			CGPROGRAM
				#pragma vertex vert_img
				#pragma fragment frag
				#include "UnityCG.cginc"
			
				sampler2D _MainTex;
				sampler2D _DistortionTex;
				float _Magnitude;
			
				float4 frag(v2f_img IN) : COLOR {
					float2 uv_offset = (tex2D( _DistortionTex, IN.uv ).rg - float2(0.5,0.5)) * _Magnitude;
					half4 c = tex2D (_MainTex, IN.uv + uv_offset);
					return c;
				}
			ENDCG
		}
	}
}