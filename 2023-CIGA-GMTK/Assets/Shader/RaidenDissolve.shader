// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "RaidenDissolve"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_DissolveTex("DissolveTex", 2D) = "white" {}
		_dissolve("dissolve", Range( -0.1 , 1)) = 1
		_T_noise_01("T_noise_01", 2D) = "white" {}
		[HDR]_Color("Color ", Color) = (0,0,0,0)
		_Intensity("Intensity", Float) = 0
		_EdgeTex("EdgeTex", 2D) = "white" {}
		_EdgeMask("EdgeMask", 2D) = "white" {}
		_Edge_p("Edge_p", Float) = 0
		[HDR]_EdgeCol("EdgeCol", Color) = (0,0,0,0)
		_EdgeIntensity("EdgeIntensity", Float) = 0
		_CardTex("CardTex", 2D) = "white" {}
		_EdgeMask_P("EdgeMask_P", Float) = 0
		_MainIntensity("MainIntensity", Float) = 0
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
		Cull Off
		ColorMask RGBA
		ZWrite On
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
			#include "UnityShaderVariables.cginc"


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
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform sampler2D _EdgeTex;
			SamplerState sampler_EdgeTex;
			uniform float4 _EdgeTex_ST;
			uniform sampler2D _EdgeMask;
			SamplerState sampler_EdgeMask;
			uniform float _Edge_p;
			uniform float4 _EdgeCol;
			uniform float _EdgeIntensity;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform sampler2D _T_noise_01;
			SamplerState sampler_T_noise_01;
			uniform float4 _Color;
			uniform float _Intensity;
			uniform sampler2D _CardTex;
			SamplerState sampler_CardTex;
			uniform float _EdgeMask_P;
			SamplerState sampler_MainTex;
			uniform float _MainIntensity;
			uniform sampler2D _DissolveTex;
			SamplerState sampler_DissolveTex;
			uniform float4 _DissolveTex_ST;
			uniform float _dissolve;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				
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
				float2 uv_EdgeTex = i.ase_texcoord1.xy * _EdgeTex_ST.xy + _EdgeTex_ST.zw;
				float2 texCoord21 = i.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult25 = (float2(0.0 , _Edge_p));
				float temp_output_20_0 = ( tex2D( _EdgeTex, uv_EdgeTex ).r * tex2D( _EdgeMask, ( texCoord21 + appendResult25 ) ).r );
				float2 uv_MainTex = i.ase_texcoord1.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode1 = tex2D( _MainTex, uv_MainTex );
				float2 texCoord8 = i.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner9 = ( 1.0 * _Time.y * float2( 0,0.2 ) + texCoord8);
				float4 temp_cast_0 = (0.0).xxxx;
				float2 texCoord30 = i.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult31 = (float2(0.0 , _EdgeMask_P));
				float2 uv_DissolveTex = i.ase_texcoord1.xy * _DissolveTex_ST.xy + _DissolveTex_ST.zw;
				float4 appendResult7 = (float4(( ( temp_output_20_0 * _EdgeCol * _EdgeIntensity ) + tex2DNode1 + max( ( tex2D( _T_noise_01, panner9 ).r * _Color * _Intensity ) , temp_cast_0 ) + ( tex2D( _CardTex, ( texCoord30 + appendResult31 ) ).r * _EdgeCol ) + ( tex2DNode1 * pow( tex2DNode1.r , 0.5 ) * _MainIntensity ) ).rgb , ( temp_output_20_0 + ( tex2DNode1.a * step( saturate( ( 1.0 - tex2D( _DissolveTex, uv_DissolveTex ).r ) ) , _dissolve ) ) )));
				
				
				finalColor = appendResult7;
				return finalColor;
			}
			ENDCG
		}
	}
	//CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18500
338;393;1535;788;1758.766;1048.002;1.834679;True;True
Node;AmplifyShaderEditor.RangedFloatNode;23;-2036.912,-975.8088;Inherit;False;Constant;_Float1;Float 1;8;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-2076.757,-845.6479;Inherit;False;Property;_Edge_p;Edge_p;8;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;8;-2012.134,-172.3304;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;25;-1803.152,-937.2919;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;21;-2047.682,-1187.214;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;32;-2405.866,-1517.233;Inherit;False;Constant;_Float2;Float 2;12;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-2423.866,-1399.887;Inherit;False;Property;_EdgeMask_P;EdgeMask_P;12;0;Create;True;0;0;False;0;False;0;0.07;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-1651.002,509.1741;Inherit;True;Property;_DissolveTex;DissolveTex;1;0;Create;True;0;0;False;0;False;-1;ca81fd1364a5dbe40ae09686d8286594;ca81fd1364a5dbe40ae09686d8286594;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;3;-1314.978,536.8556;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;9;-1507.412,-117.6539;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.2;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;22;-1639.786,-1095.345;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;30;-2138.465,-1690.233;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;31;-2081.466,-1477.733;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;10;-1245.412,-126.6539;Inherit;True;Property;_T_noise_01;T_noise_01;3;0;Create;True;0;0;False;0;False;-1;c39acf12e3b30ec4ab31ed2f4098ace1;c39acf12e3b30ec4ab31ed2f4098ace1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;6;-1302.188,731.1862;Inherit;False;Property;_dissolve;dissolve;2;0;Create;True;0;0;False;0;False;1;1;-0.1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1041.533,-411.5344;Inherit;True;Property;_MainTex;MainTex;0;0;Create;True;0;0;False;0;False;-1;None;7620e3d0147a632438d04028f2fb3c8d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;39;-577.1926,-64.38293;Inherit;False;Constant;_Float3;Float 3;13;0;Create;True;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;19;-1402.042,-934.6358;Inherit;True;Property;_EdgeMask;EdgeMask;7;0;Create;True;0;0;False;0;False;-1;b549c8779122f3f44a62431aafa9a417;b549c8779122f3f44a62431aafa9a417;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;18;-1402.043,-1222.85;Inherit;True;Property;_EdgeTex;EdgeTex;6;0;Create;True;0;0;False;0;False;-1;d3db4ab898673b6418cedb6fe9640716;d3db4ab898673b6418cedb6fe9640716;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;11;-1351.071,75.14673;Inherit;False;Property;_Color;Color ;4;1;[HDR];Create;True;0;0;False;0;False;0,0,0,0;0.2196078,0.8941177,3.654902,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;4;-1051.566,510.7409;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1350.06,282.8737;Inherit;False;Property;_Intensity;Intensity;5;0;Create;True;0;0;False;0;False;0;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;35;-1766.466,-1629.69;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-963.744,-1047.53;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;5;-906.2272,576.6343;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-814.4328,129.4799;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;29;-1593.443,-1662.346;Inherit;True;Property;_CardTex;CardTex;11;0;Create;True;0;0;False;0;False;-1;f324f881080ea4a4f902690a4bfed146;f324f881080ea4a4f902690a4bfed146;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;38;-327.4145,-87.54132;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-1000.597,-516.6761;Inherit;False;Property;_EdgeIntensity;EdgeIntensity;10;0;Create;True;0;0;False;0;False;0;2.52;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-804.13,313.8037;Inherit;False;Constant;_Float0;Float 0;6;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-335.9546,-310.1292;Inherit;False;Property;_MainIntensity;MainIntensity;13;0;Create;True;0;0;False;0;False;0;1.4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;27;-1012.888,-755.3318;Inherit;False;Property;_EdgeCol;EdgeCol;9;1;[HDR];Create;True;0;0;False;0;False;0,0,0,0;0,2.069816,5.278032,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-580.7891,-1444.849;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-592.0013,385.2398;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;15;-433.4102,157.6054;Inherit;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-419.2056,-653.7432;Inherit;True;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;134.1702,-496.6579;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;14;288.6503,-130.6726;Inherit;True;5;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;42;-71.49878,488.2903;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;7;643.2709,68.67403;Inherit;False;COLOR;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;904.7527,63.19625;Float;False;True;-1;2;ASEMaterialInspector;100;1;RaidenDissolve;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;True;0;False;-1;True;2;False;-1;True;True;True;True;True;0;False;-1;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;2;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;2;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;False;0
WireConnection;25;0;23;0
WireConnection;25;1;24;0
WireConnection;3;0;2;1
WireConnection;9;0;8;0
WireConnection;22;0;21;0
WireConnection;22;1;25;0
WireConnection;31;0;32;0
WireConnection;31;1;33;0
WireConnection;10;1;9;0
WireConnection;19;1;22;0
WireConnection;4;0;3;0
WireConnection;35;0;30;0
WireConnection;35;1;31;0
WireConnection;20;0;18;1
WireConnection;20;1;19;1
WireConnection;5;0;4;0
WireConnection;5;1;6;0
WireConnection;12;0;10;1
WireConnection;12;1;11;0
WireConnection;12;2;13;0
WireConnection;29;1;35;0
WireConnection;38;0;1;1
WireConnection;38;1;39;0
WireConnection;37;0;29;1
WireConnection;37;1;27;0
WireConnection;17;0;1;4
WireConnection;17;1;5;0
WireConnection;15;0;12;0
WireConnection;15;1;16;0
WireConnection;26;0;20;0
WireConnection;26;1;27;0
WireConnection;26;2;28;0
WireConnection;40;0;1;0
WireConnection;40;1;38;0
WireConnection;40;2;41;0
WireConnection;14;0;26;0
WireConnection;14;1;1;0
WireConnection;14;2;15;0
WireConnection;14;3;37;0
WireConnection;14;4;40;0
WireConnection;42;0;20;0
WireConnection;42;1;17;0
WireConnection;7;0;14;0
WireConnection;7;3;42;0
WireConnection;0;0;7;0
ASEEND*/
//CHKSM=AEE1AEBC4DC63257E8854613005E117D9E5D2BF2