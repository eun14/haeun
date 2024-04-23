Shader "Unlit/YellowShader"
{
    Properties
    {
        _DiffuseColor("DiffuseColor", Color) = (1,1,1,1)
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
            };

            float4 _DiffuseColor;
            float4 _LightDirection;

            // vertex Shader : 정점 쉐이더 함수는 입력으로 받은 모델의 정점을 변환하여 화면 공간으로 변환함.
            // appdata 구조체를 사용하여 정점의 위치와 법선을 받아와서, UnityObjectToClipPos 함수를 사용하여 정점을 클립 공간으로 변환함.
            // 변환된 정점과 법선은 v2f 구조체에 저장되어 프래그먼트 쉐이더로 전달
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                return o;
            }

            // fragment Shader :  각 픽셀의 색상을 계산하는 쉐이더로 fixed4 형식의 값을 반환해야 하며, 이 값은 화면에 픽셀을 그리는 데 사용됨.
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = float4(1.0f,1.0f,0.0,1.0f);    // 노란색으로 고정된 값
                // float lightDir = normalize(_LightDirection);
                // float lightIntensity = max(dot(i.normal,lightDir),0);

                // float4 col = _DiffuseColor * lightIntensity;


                return col;
            }
            ENDCG
        }
    }
}