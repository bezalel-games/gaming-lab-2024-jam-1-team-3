Shader "Unlit/PaperOverlay"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Overlay ("Overlay", 2D) = "white" {}
        _Intensity("Intensity", Range(0, 1)) = 0.5

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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

            sampler2D _Overlay;
            float4 _Overlay_ST;

            float _Intensity;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                return o;
            }
            
            fixed4 lightenBlendPremultiplied(fixed4  src, fixed4 dst) {
                fixed outA = src.a + dst.a - src.a * dst.a;
                fixed3 outRGB = max(src.rgb * dst.a, dst.rgb * src.a) + src.rgb * (1.0 - dst.a) + dst.rgb *
                  (1.0 - src.a);
                
              return fixed4(outRGB, outA);
            }

            
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 mainColor = tex2D(_MainTex, i.uv);
                
                fixed4 overlayColor = tex2D(_Overlay, i.uv);
                overlayColor.rgb *= overlayColor.a;
                
                fixed4 col = lightenBlendPremultiplied(mainColor, overlayColor);

                col = lerp(mainColor, col, _Intensity);
                return col;
            }
            ENDCG
        }
    }
}
