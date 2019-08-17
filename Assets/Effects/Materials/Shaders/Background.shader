Shader "Unlit/Background"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SubTex ("MidTexture", 2D) = "black" {}
        _UvRatio ("Uv Ratio", Vector) = (1,1,0,0)
        _Multiplier ("Multiplier", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        ZWrite off
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            Texture2D _SubTex;
            float4 _SubTex_ST;
            float4 _SubTex_TexelSize;
            float2 _UvRatio;
            SamplerState point_RepeatU_ClampV_sampler;
            float _Multiplier;

            v2f vert (appdata v)
            {
                v2f o;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                float2 uv2 = TRANSFORM_TEX(v.uv, _SubTex);
                o.uv2 = float2(_WorldSpaceCameraPos.x, _WorldSpaceCameraPos.y) / _UvRatio;
                o.uv2 += uv2 * float2(_SubTex_TexelSize.w / _SubTex_TexelSize.z, 1) * _Multiplier;
                o.vertex = float4(o.uv * 2 - 1, 1, 1);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 col2 = _SubTex.Sample(point_RepeatU_ClampV_sampler, float2(i.uv2.x, saturate(i.uv2.y)));
                return lerp(col, col2, col2.a);
            }
            ENDCG
        }
    }
}
