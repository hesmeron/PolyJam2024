// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "fogShaderY"
{
	Properties
	{
		_maxDistance("maxDistance", Float) = 100
		_fogDistance("fogDistance", Float) = 100
		_startingValue("startingValue", Float) = 1.5
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_color("color", Color) = (0,0,0,0)
		_blendColor("blendColor", Color) = (0.5518868,0.9382761,1,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float _startingValue;
		uniform float _maxDistance;
		uniform float4 _blendColor;
		uniform float _fogDistance;
		uniform float4 _color;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float4 appendResult12 = (float4(ase_worldPos.x , 0.0 , ase_worldPos.y , 0.0));
			v.vertex.xyz += ( ( max( ( _startingValue - ( ase_worldPos.y / _maxDistance ) ) , 0.0 ) * appendResult12 ) - appendResult12 ).xyz;
			v.vertex.w = 1;
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float3 ase_worldPos = i.worldPos;
			float clampResult24 = clamp( ( ase_worldPos.z / _fogDistance ) , 0.0 , 1.0 );
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			o.Emission = ( ( _blendColor * clampResult24 ) + ( ( _color * tex2D( _TextureSample0, uv_TextureSample0 ) ) * ( 1.0 - clampResult24 ) ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19105
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-212.3287,-48.80802;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;12;-414.6426,213.6174;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;13;-18.48201,42.72324;Inherit;True;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.WorldPosInputsNode;14;-664.7801,380.5578;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;6;-1043.3,-261.9002;Inherit;False;Property;_startingValue;startingValue;2;0;Create;True;0;0;0;False;0;False;1.5;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;412,-287;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;fogShaderY;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-273.7099,-621.2946;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;15;-333.0673,-142.2128;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-424.509,-440.5948;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;19;-549.309,-279.3948;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;5;-813.3558,-273.036;Inherit;False;2;0;FLOAT;1.1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;20;-146.309,-534.1946;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;17;-514.2099,-840.9942;Inherit;False;Property;_blendColor;blendColor;5;0;Create;True;0;0;0;False;0;False;0.5518868,0.9382761,1,0;0.5518868,0.9382761,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;8;-1413.5,-699.1005;Inherit;True;Property;_TextureSample0;Texture Sample 0;3;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;9;-985.5002,-792.1005;Inherit;False;Property;_color;color;4;0;Create;True;0;0;0;False;0;False;0,0,0,0;1,0.8291087,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-687.5,-613.1004;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;2;-989.8558,-57.03625;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;1;-1306.856,-39.03624;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;3;-1168.856,147.9638;Inherit;False;Property;_maxDistance;maxDistance;0;0;Create;True;0;0;0;False;0;False;100;1506.89;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;21;-1241.977,-323.8279;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;22;-1558.977,-305.8279;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;23;-1420.977,-118.8279;Inherit;False;Property;_fogDistance;fogDistance;1;0;Create;True;0;0;0;False;0;False;100;1506.89;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;24;-1056.31,-439.2944;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
WireConnection;11;0;15;0
WireConnection;11;1;12;0
WireConnection;12;0;14;1
WireConnection;12;2;14;2
WireConnection;13;0;11;0
WireConnection;13;1;12;0
WireConnection;0;2;20;0
WireConnection;0;11;13;0
WireConnection;16;0;17;0
WireConnection;16;1;24;0
WireConnection;15;0;5;0
WireConnection;18;0;10;0
WireConnection;18;1;19;0
WireConnection;19;0;24;0
WireConnection;5;0;6;0
WireConnection;5;1;2;0
WireConnection;20;0;16;0
WireConnection;20;1;18;0
WireConnection;10;0;9;0
WireConnection;10;1;8;0
WireConnection;2;0;1;2
WireConnection;2;1;3;0
WireConnection;21;0;22;3
WireConnection;21;1;23;0
WireConnection;24;0;21;0
ASEEND*/
//CHKSM=EE0537535852F595DE190239C43CC22F7BD8ABE4