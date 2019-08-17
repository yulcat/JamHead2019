// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/SlicedSprite"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Clip ("Clipping Points", Vector) = (0.333, 0.333, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        
        blend srcalpha oneminussrcalpha
        zwrite off
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
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            float4 _Clip;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float xscale = length(unity_ObjectToWorld._m00_m01_m02) / length(unity_ObjectToWorld._m10_m11_m12); 
                xscale *= _MainTex_TexelSize.w / _MainTex_TexelSize.z;
                float x = xscale * i.uv.x;
                float xf = xscale;
                float mid = 1 - (_Clip.x + _Clip.y);
                i.uv.x = lerp(x, lerp(1 - _Clip.y + (x - xf + _Clip.y), _Clip.x + (x - _Clip.x) % mid, x < xf - _Clip.y), x > _Clip.x);
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
