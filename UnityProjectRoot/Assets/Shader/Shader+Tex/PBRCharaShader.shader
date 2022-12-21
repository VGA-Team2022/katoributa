Shader "TK/Material/PBRCharaShader"
{
Properties
    {
        _MainTex("Texture", 2D) = "white" { }
        _Distance("Dist", Range(0.001, 100)) = 5
        _PerRange("PerRange", Range(1, 3.5)) = 1
        [Normal] _NormalTex("Normal map", 2D) = "bump" { }
        _SpecularLevel("SpecularLevel", float) = 0.0
    }

    CGINCLUDE

    
    CBUFFER_START(UnityPerMaterial)
    sampler2D _MainTex;
    float4 _MainTex_ST;
    float _Distance;
    float _PerRange;
    sampler2D _NormalTex;
    float _SpecularLevel;
    CBUFFER_END
    ENDCG

    SubShader
    {
        Tags
        {
             "RenderType" = "Transparent" 
             "Queue" = "Transparent"
        }
        LOD 100
        Pass
        {
            Name "Base"
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            # include "UnityCG.cginc" 
            # include "UnityLightingCommon.cginc"
            # include "Lighting.cginc" 
            # include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD1;
                half4 tangent : TANGENT;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float4 wpos : TEXCOORD1;
                float4 spos : TEXCOORD2;
                fixed4 diff : COLOR0;
                half3 normal : TEXCOORD3; //法線
                half3 tangent : TEXCOORD4; //接線
                half3 binormal : TEXCOORD5; //従法線
                SHADOW_COORDS(1)
            };


            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.wpos = mul(unity_ObjectToWorld, v.vertex);
                o.spos = ComputeScreenPos(o.pos);
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                half NdotL = saturate(dot(worldNormal, _WorldSpaceLightPos0.xyz));
                o.diff = NdotL * _LightColor0;

                o.normal = UnityObjectToWorldNormal(v.normal); //法線をワールド座標系に変換
                o.tangent = normalize(mul(unity_ObjectToWorld, v.tangent)).xyz; //接線をワールド座標系に変換
                o.binormal = cross(v.normal, v.tangent) * v.tangent.w; //変換前の法線と接線から従法線を計算
                o.binormal = normalize(mul(unity_ObjectToWorld, o.binormal));

                TRANSFER_SHADOW(o)
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 ligDirection = normalize(_WorldSpaceLightPos0.xyz); //シーンのディレクショナルライト方向を取得
                fixed3 ligColor = _LightColor0.xyz; //ディレクショナルライトのカラーを取得

                half3 normalmap = UnpackNormal(tex2D(_NormalTex, i.uv)); //ノーマルマップをプラットフォームに合わせて自動解釈
                float3 normal = (i.tangent * normalmap.x) + (i.binormal * normalmap.y) + (i.normal * normalmap.z); //ノーマルマップをもとに法線を合成

                //////////ランバート拡散反射
                float t = dot(normal, ligDirection); //ライト方向と法線方向で内積を計算
                t = max(0, t); //計算した内積のうち、t < 0は必要ないのでクランプ

                float3 diffuseLig = ligColor * t; //ディフューズカラーを計算。内積が0に近いほど色が黒くなる
                //////////

                //////////フォン鏡面反射
                float3 refVec = reflect(-ligDirection, normal); //ライト方向と法線方向から反射ベクトルを計算

                float3 toEye = _WorldSpaceCameraPos - i.wpos; //カメラからの視線ベクトルを計算
                toEye = normalize(toEye); //視線ベクトルを正規化

                t = dot(refVec, toEye); //反射ベクトルと視線ベクトルで内積を計算
                t = max(0, t); //計算した内積のうち、t < 0は必要ないのでクランプ
                t = pow(t, _SpecularLevel); //反射の絞りを調整

                float3 specularLig = ligColor * t;



                fixed4 col = tex2D(_MainTex, i.uv);

                col.rgb *= (specularLig + diffuseLig);

                
                fixed4 shadow = SHADOW_ATTENUATION(i);
                col *= i.diff * shadow;

                float dist = distance(i.wpos, _WorldSpaceCameraPos);
                float c2Dis = _Distance / _PerRange;
                float dist2Per = dist / _PerRange;
                float dis = clamp(c2Dis - dist2Per, 0, 1);
                //float dis = clamp((_Distance - dist), 0, 1);
                //float distan = smoothstep(0.1, 1, (_Distance - dist));
                //clip(dis);
                col.a = (1 - dis) + 0.2;
                //clip(clamp_distance - threshold);

                return col;
            }
            ENDCG
        }
        Pass
        {
            Tags{ "LightMode" = "ShadowCaster" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster

            # include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                V2F_SHADOW_CASTER;
            };

            v2f vert(appdata v)
            {
                v2f o;
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }

    }
}
