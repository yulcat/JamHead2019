Shader "_myJam/WorldGradientShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_GradientTex("GradientTexture", 2D) = "white" {}
		_OffsetX("OffsetX", Float) = 1
		_OffsetY("OffsetY", Float) = 1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
                float4 worldPosition : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
				o.worldPosition = mul(unity_ObjectToWorld, v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
			sampler2D _GradientTex;
			float _OffsetX;
			float _OffsetY;

            fixed4 frag (v2f i) : SV_Target
            {// tex2D(_MainTex, i.uv) *
                fixed4 col = tex2D(_MainTex,i.uv)*tex2D(_GradientTex,i.worldPosition.xy*float2(_OffsetX,_OffsetY));

                // just invert the colors
                return col;
            }
            ENDCG
        }
    }
}
