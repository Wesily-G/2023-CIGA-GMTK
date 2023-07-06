// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "WaterDissolve"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_FlowMap("FlowMap", 2D) = "white" {}
		_Dissolve("Dissolve", Range( 0 , 1)) = 0.05253153
		_DissolveTex("DissolveTex", 2D) = "white" {}
		_SmoothDisslove("SmoothDisslove", Range( 0.5 , 1)) = 0.5
		[HDR]_TwistCol("TwistCol", Color) = (0,0.499094,1,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaToMask Off
		Cull Back
		ColorMask RGBA
		ZWrite Off
		ZTest LEqual
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#define ASE_NEEDS_FRAG_COLOR


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform sampler2D _TextureSample0;
			uniform sampler2D _FlowMap;
			SamplerState sampler_FlowMap;
			uniform float4 _FlowMap_ST;
			uniform float _Dissolve;
			uniform float _SmoothDisslove;
			uniform sampler2D _DissolveTex;
			SamplerState sampler_DissolveTex;
			uniform float4 _TwistCol;
			SamplerState sampler_TextureSample0;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				o.ase_color = v.color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.zw = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				float2 texCoord10 = i.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float2 uv_FlowMap = i.ase_texcoord1.xy * _FlowMap_ST.xy + _FlowMap_ST.zw;
				float4 tex2DNode8 = tex2D( _FlowMap, uv_FlowMap );
				float2 appendResult9 = (float2(tex2DNode8.r , tex2DNode8.g));
				float2 lerpResult11 = lerp( texCoord10 , appendResult9 , _Dissolve);
				float4 tex2DNode2 = tex2D( _TextureSample0, lerpResult11 );
				float smoothstepResult19 = smoothstep( ( 1.0 - _SmoothDisslove ) , _SmoothDisslove , ( tex2D( _DissolveTex, lerpResult11 ).r + 1.0 + ( _Dissolve * -2.0 ) ));
				float4 appendResult6 = (float4(( (( tex2DNode2 * i.ase_color )).rgba + ( smoothstepResult19 * _TwistCol ) ).rgb , ( i.ase_color.a * smoothstepResult19 * tex2DNode2.b )));
				
				
				finalColor = appendResult6;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18500
280;467;1535;788;911.4667;207.7909;1.661374;True;True
Node;AmplifyShaderEditor.SamplerNode;8;-1979.62,-287.2699;Inherit;True;Property;_FlowMap;FlowMap;1;0;Create;True;0;0;False;0;False;-1;22f43742875ebd04f972342399c40216;e2aa742560ea5ec49a36069a17783227;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;12;-2112.616,116.2513;Inherit;False;Property;_Dissolve;Dissolve;2;0;Create;True;0;0;False;0;False;0.05253153;0.4381265;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;9;-1429.625,-196.7259;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;10;-1593.617,-512.7695;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;18;-1715.051,1119.358;Inherit;False;Constant;_Float3;Float 3;4;0;Create;True;0;0;False;0;False;-2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;11;-1143.609,-398.4783;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-1483.029,797.4249;Inherit;False;Constant;_Float1;Float 1;4;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-753.4308,1032.625;Inherit;False;Property;_SmoothDisslove;SmoothDisslove;5;0;Create;True;0;0;False;0;False;0.5;1;0.5;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;13;-1477.553,465.7257;Inherit;True;Property;_DissolveTex;DissolveTex;3;0;Create;True;0;0;False;0;False;-1;1c5c6a547f3c0624997313cda5d0396f;1c5c6a547f3c0624997313cda5d0396f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-1404.63,1088.625;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;3;-636.681,-98.29288;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-755.4862,-505.1421;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;False;-1;7620e3d0147a632438d04028f2fb3c8d;7620e3d0147a632438d04028f2fb3c8d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;21;-694.2307,765.4251;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;14;-1009.43,667.8251;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;19;-312.1732,627.4112;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-308.6095,-98.91231;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;27;-280.8073,906.3926;Inherit;False;Property;_TwistCol;TwistCol;5;1;[HDR];Create;True;0;0;False;0;False;0,0.499094,1,0;0,0.9960784,2,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;5;-76.4964,-113.8734;Inherit;False;True;True;True;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;100.6985,750.1959;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;130.7879,434.2019;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;28;369.4516,63.01306;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;6;711.3928,139.5658;Inherit;True;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;981.1779,210.2282;Float;False;True;-1;2;ASEMaterialInspector;100;1;WaterDissolve;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;True;0;False;-1;True;0;False;-1;True;True;True;True;True;0;False;-1;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;2;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;2;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;False;0
WireConnection;9;0;8;1
WireConnection;9;1;8;2
WireConnection;11;0;10;0
WireConnection;11;1;9;0
WireConnection;11;2;12;0
WireConnection;13;1;11;0
WireConnection;16;0;12;0
WireConnection;16;1;18;0
WireConnection;2;1;11;0
WireConnection;21;0;20;0
WireConnection;14;0;13;1
WireConnection;14;1;15;0
WireConnection;14;2;16;0
WireConnection;19;0;14;0
WireConnection;19;1;21;0
WireConnection;19;2;20;0
WireConnection;4;0;2;0
WireConnection;4;1;3;0
WireConnection;5;0;4;0
WireConnection;26;0;19;0
WireConnection;26;1;27;0
WireConnection;7;0;3;4
WireConnection;7;1;19;0
WireConnection;7;2;2;3
WireConnection;28;0;5;0
WireConnection;28;1;26;0
WireConnection;6;0;28;0
WireConnection;6;3;7;0
WireConnection;0;0;6;0
ASEEND*/
//CHKSM=22B8573C024491AE4A6F73007E2000C4F1707DE7