Shader "TK/Material/PBRCharaShader"
{
Properties
    {
        _MainTex("Texture", 2D) = "white" { }
        _Distance("Dist", Range(0.001, 100)) = 5
        _PerRange("PerRange", Range(1, 3.5)) = 1
    }

CGINCLUDE
    #include "UnityCG.cginc"

    struct appdata
    {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
        float3 normal : NORMAL;
    };

    struct v2f
    {
        float2 uv : TEXCOORD0;
        float4 pos : SV_POSITION;
        float4 wpos : TEXCOORD1;
        float4 spos : TEXCOORD2;
    };
    CBUFFER_START(UnityPerMaterial)
    sampler2D _MainTex;
    float4 _MainTex_ST;
    float _Distance;
    float _PerRange;
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


            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.wpos = mul(unity_ObjectToWorld, v.vertex);
                o.spos = ComputeScreenPos(o.pos);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

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

    }
}
