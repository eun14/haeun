Shader "Unlit/YellowShader"
{
    Properties
    {
        _DiffuseColor("DiffuseColor", Color) = (1,0.8,0,1)
        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
        _LightDirection("LightDirection", Vector) = (1,-1,-1,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }


        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
           
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float3 viewDir : TEXCOORDO; // 뷰 방향 저장
            };

            float4 _DiffuseColor;
            float4 _LightDirection;
            float4 _OutlineColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = normalize(UnityObjectToWorldNormal(v.normal));
                o.viewDir = normalize(UnityWorldSpaceViewDir(v.vertex));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                float lightIntensity = max(dot(i.normal, normalize(_LightDirection)),0);

                float ramped = round(lightIntensity * 5) / 5;       // 카툰 쉐이딩을 위해 라운드 처리
                float rim = saturate(dot(i.normal, i.viewDir));    // 림 라이팅 계산
                float4 col = _DiffuseColor * rim * ramped;          // 카툰 쉐이딩 색상 계산
                // float lightDir = normalize(_LightDirection);
                // float lightIntensity = max(dot(i.normal,lightDir),0);
                // float4 col = _DiffuseColor * lightIntensity;


                return col;
            }
            ENDCG
        }
    }
}