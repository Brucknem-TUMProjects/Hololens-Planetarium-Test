Shader "Custom/Tetrahedron Surf"
{
	Properties
	{
		_Transparency("Transparency", Range(0,1)) = 0.2
	}

		SubShader
	{ 
		
		Tags { "Queue" = "Transparent"}
		
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite off

		//Pass{
		//	ZWrite On
		//	ColorMask 0
		//}

		CGPROGRAM
			//Cull front
		#pragma surface surf Lambert alpha:fade 
		struct Input {
			float4 color : COLOR;
		};

		float _Transparency;

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = IN.color.rgb;
			o.Alpha = IN.color.a * _Transparency;
		}

		ENDCG
	}
}