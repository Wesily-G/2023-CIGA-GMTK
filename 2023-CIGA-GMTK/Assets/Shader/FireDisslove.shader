// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33425,y:33050,varname:node_9361,prsc:2|custl-6278-OUT,clip-9299-OUT;n:type:ShaderForge.SFN_Tex2d,id:4767,x:31491,y:33261,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_4767,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:75221c3ce0101e34d9aa3f9427e7e0f8,ntxv:0,isnm:False|UVIN-936-UVOUT;n:type:ShaderForge.SFN_Panner,id:936,x:31227,y:33240,varname:node_936,prsc:2,spu:0,spv:-0.5|UVIN-7380-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:7380,x:30848,y:33225,varname:node_7380,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Color,id:2798,x:31481,y:33009,ptovrint:False,ptlb:NoiseCol,ptin:_NoiseCol,varname:node_2798,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:91,x:32256,y:33252,varname:node_91,prsc:2|A-2798-RGB,B-4767-RGB;n:type:ShaderForge.SFN_Tex2d,id:3312,x:32059,y:32887,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_3312,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7620e3d0147a632438d04028f2fb3c8d,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1577,x:31490,y:33907,ptovrint:False,ptlb:DissolveTex,ptin:_DissolveTex,varname:node_1577,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:75221c3ce0101e34d9aa3f9427e7e0f8,ntxv:0,isnm:False|UVIN-936-UVOUT;n:type:ShaderForge.SFN_Step,id:9299,x:32131,y:33836,varname:node_9299,prsc:2|A-3239-OUT,B-651-OUT;n:type:ShaderForge.SFN_Multiply,id:651,x:31714,y:33986,varname:node_651,prsc:2|A-1577-R,B-729-OUT;n:type:ShaderForge.SFN_Subtract,id:2255,x:32019,y:34170,varname:node_2255,prsc:2|A-3239-OUT,B-836-OUT;n:type:ShaderForge.SFN_Vector1,id:836,x:31727,y:34236,varname:node_836,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Step,id:7191,x:32214,y:34095,varname:node_7191,prsc:2|A-2255-OUT,B-651-OUT;n:type:ShaderForge.SFN_Subtract,id:277,x:32449,y:33965,varname:node_277,prsc:2|A-7191-OUT,B-9299-OUT;n:type:ShaderForge.SFN_Add,id:9338,x:32726,y:33353,varname:node_9338,prsc:2|A-91-OUT,B-4114-OUT;n:type:ShaderForge.SFN_Color,id:235,x:32404,y:34187,ptovrint:False,ptlb:RimCol,ptin:_RimCol,varname:node_235,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.3847525,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:4114,x:32621,y:34043,varname:node_4114,prsc:2|A-277-OUT,B-235-RGB;n:type:ShaderForge.SFN_Add,id:6278,x:32977,y:33221,varname:node_6278,prsc:2|A-3312-RGB,B-9338-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3239,x:31832,y:33815,ptovrint:False,ptlb:node_3239,ptin:_node_3239,varname:node_3239,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.001;n:type:ShaderForge.SFN_Vector2,id:3495,x:30862,y:34179,varname:node_3495,prsc:2,v1:0.75,v2:0.2;n:type:ShaderForge.SFN_Distance,id:9964,x:31144,y:34111,varname:node_9964,prsc:2|A-3495-OUT,B-7380-UVOUT;n:type:ShaderForge.SFN_OneMinus,id:729,x:31323,y:34111,varname:node_729,prsc:2|IN-9964-OUT;proporder:4767-2798-3312-1577-235-3239;pass:END;sub:END;*/

Shader "Shader Forge/FireDisslove" {
    Properties {
        _Noise ("Noise", 2D) = "white" {}
        [HDR]_NoiseCol ("NoiseCol", Color) = (1,0,0,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _DissolveTex ("DissolveTex", 2D) = "white" {}
        [HDR]_RimCol ("RimCol", Color) = (1,0.3847525,0,1)
        _node_3239 ("node_3239", Float ) = 0.001
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _DissolveTex; uniform float4 _DissolveTex_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _NoiseCol)
                UNITY_DEFINE_INSTANCED_PROP( float4, _RimCol)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_3239)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float _node_3239_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_3239 );
                float4 node_3104 = _Time;
                float2 node_936 = (i.uv0+node_3104.g*float2(0,-0.5));
                float4 _DissolveTex_var = tex2D(_DissolveTex,TRANSFORM_TEX(node_936, _DissolveTex));
                float node_651 = (_DissolveTex_var.r*(1.0 - distance(float2(0.75,0.2),i.uv0)));
                float node_9299 = step(_node_3239_var,node_651);
                clip(node_9299 - 0.5);
////// Lighting:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _NoiseCol_var = UNITY_ACCESS_INSTANCED_PROP( Props, _NoiseCol );
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(node_936, _Noise));
                float4 _RimCol_var = UNITY_ACCESS_INSTANCED_PROP( Props, _RimCol );
                float3 node_9338 = ((_NoiseCol_var.rgb*_Noise_var.rgb)+((step((_node_3239_var-0.1),node_651)-node_9299)*_RimCol_var.rgb));
                float3 finalColor = (_MainTex_var.rgb+node_9338);
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _DissolveTex; uniform float4 _DissolveTex_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _node_3239)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float _node_3239_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_3239 );
                float4 node_285 = _Time;
                float2 node_936 = (i.uv0+node_285.g*float2(0,-0.5));
                float4 _DissolveTex_var = tex2D(_DissolveTex,TRANSFORM_TEX(node_936, _DissolveTex));
                float node_651 = (_DissolveTex_var.r*(1.0 - distance(float2(0.75,0.2),i.uv0)));
                float node_9299 = step(_node_3239_var,node_651);
                clip(node_9299 - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    //CustomEditor "ShaderForgeMaterialInspector"
}
