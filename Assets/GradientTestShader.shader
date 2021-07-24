// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/GradientTestShader"
{
	Properties
	{
		_StartColor("Start Color",Color) = (1,1,1,1)
		_EndColor("End Color",Color) = (1,1,1,1)
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

			fixed4 _StartColor;
			fixed4 _EndColor;

            struct v2f
            {
                fixed4 col : COLOR;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_full v)
            {
                v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.col = lerp(_EndColor,_StartColor, v.texcoord.y);
                return o;
            }


            fixed4 frag (v2f i) : COLOR
            {
				fixed4 col = i.col;
                return col;
            }
            ENDCG
        }
    }
}
