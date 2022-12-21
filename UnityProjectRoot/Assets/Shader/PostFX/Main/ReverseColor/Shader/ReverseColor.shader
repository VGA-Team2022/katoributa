Shader "TK/PostFX/ReverseColor"
{
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

            float3 rgb2hsv(float3 rgb)
            {
                float3 hsv;

                // RGBの三つの値で最大のもの
                float maxValue = max(rgb.r, max(rgb.g, rgb.b));
                // RGBの三つの値で最小のもの
                float minValue = min(rgb.r, min(rgb.g, rgb.b));
                // 最大値と最小値の差
                float delta = maxValue - minValue;

                hsv.z = maxValue;

                if (maxValue != 0.0)
                {
                    hsv.y = delta / maxValue;
                }
                else
                {
                    hsv.y = 0.0;
                }

                if (hsv.y > 0.0)
                {
                    if (rgb.r == maxValue)
                    {
                        hsv.x = (rgb.g - rgb.b) / delta;
                    }
                    else if (rgb.g == maxValue)
                    {
                        hsv.x = 2 + (rgb.b - rgb.r) / delta;
                    }
                    else
                    {
                        hsv.x = 4 + (rgb.r - rgb.g) / delta;
                    }
                    hsv.x /= 6.0;
                    if (hsv.x < 0)
                    {
                        hsv.x += 1.0;
                    }
                }

                return hsv;
            }

            float3 hsv2rgb(float3 hsv)
            {
                float3 rgb;

                if (hsv.y == 0)
                {
                    rgb.r = rgb.g = rgb.b = hsv.z;
                }
                else
                {
                    hsv.x *= 6.0;
                    float i = floor(hsv.x);
                    float f = hsv.x - i;
                    float aa = hsv.z * (1 - hsv.y);
                    float bb = hsv.z * (1 - (hsv.y * f));
                    float cc = hsv.z * (1 - (hsv.y * (1 - f)));
                    if (i < 1)
                    {
                        rgb.r = hsv.z;
                        rgb.g = cc;
                        rgb.b = aa;
                    }
                    else if (i < 2)
                    {
                        rgb.r = bb;
                        rgb.g = hsv.z;
                        rgb.b = aa;
                    }
                    else if (i < 3)
                    {
                        rgb.r = aa;
                        rgb.g = hsv.z;
                        rgb.b = cc;
                    }
                    else if (i < 4)
                    {
                        rgb.r = aa;
                        rgb.g = bb;
                        rgb.b = hsv.z;
                    }
                    else if (i < 5)
                    {
                        rgb.r = cc;
                        rgb.g = aa;
                        rgb.b = hsv.z;
                    }
                    else {
                        rgb.r = hsv.z;
                        rgb.g = aa;
                        rgb.b = bb;
                    }
                }
                return rgb;
            }

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

			v2f vert(appdata v) 
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 color = tex2D(_MainTex, i.uv);
				
				color.rgb = 1 - color.rgb;

                fixed3 subColor = rgb2hsv(color.rgb);
                subColor = abs(subColor) * 0.5;
                color.rgb = hsv2rgb(subColor);

				return color;
			}
		ENDCG
		}
	}
}
