Shader "TK/Custom/MovieShader"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_Amount("Distort", Float) = 0.0
		_Color("Color", Color) = (1, 1, 1, 1)
		_Cutoff("Cutoff", Range(0, 1)) = 0.5
	}
	SubShader
	{
		Tags 
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			fixed4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed _Cutoff;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv) * _Color;
				clip(col.rgb - _Cutoff);
				col.a = _Color.a;
				return col;
			}

			ENDCG
		}
	}
}
