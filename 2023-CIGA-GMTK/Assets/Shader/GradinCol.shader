Shader "Chaptor 8/SHA_AlphaBlendZWrite"
{
    Properties
    {
        _Color1 ("Color1", Color) = (1, 1, 1, 1)
        _Color2 ("Color2", Color) = (1, 1, 1, 1)
        _MainTex ("Main Tex", 2D) = "white" {}
        // 用于控制整体的透明度
        _AlphaScale ("Alpha Scale", Range(0, 1.0)) = 1
        [PowerSlider(1)] _Pos1("第二个颜色位置", Range(0.0, 1.0)) = 0.2
    }
    SubShader
    {
        // 通过标签 Queue 指定渲染队列为 Transparent
        Tags {"Queue"="Transparent" "IgnoreProjector"="true" "RenderType"="Transparent"}

        Pass {
            // 第一个 Pass 开启深度写入
            ZWrite on
            // ColorMask 为渲染命令，用于设置颜色通道的写掩码
            // ColorMask 0 表示该 Pass 不写入任何颜色通道，即不会输出任何颜色
            ColorMask 0
        }

        Pass
        {
            Tags {"LightMode" = "ForwardBase"}
            // 深度写入设置为关闭状态
            ZWrite off
            // 开启混合，并设置相应混合因子
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            fixed4 _Color1;
            fixed4 _Color2;
            float _Pos1;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed _AlphaScale;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float2 uv : TEXCOORD2;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 worldNormal = normalize(i.worldNormal);
                float3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));

                //像素颜色
                fixed4 col;
                //插值
                float lp = 0.0;
				// 如果当前像素位置处于 0 - _Pos1 的范围内，就使用color1与color2的插值计算颜色
				if (i.uv.x >= _Pos1)
				{
					// 插值 = 当前像素在pos1范围内的比重 = 在当前像素点的y值 / _Pos1界限值 
					lp = (1 - i.uv.x) / (1 - _Pos1);
					//像素输出的颜色 = _Color1 - _Color2之间，处于lp位置的颜色
					//lerp 三个参数为起始值，终止值，比重
					col = lerp(_Color1, _Color2, lp);
				}
				// 否则就使用第二个颜色
				else
				{
					col = _Color2;
				}
                
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed3 albedo = texColor * col.rgb;
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;
                fixed3 diffuse = _LightColor0.rgb * albedo * max(0, dot(worldNormal, worldLightDir));
                return fixed4(ambient + diffuse, texColor.a * _AlphaScale);
            }
            ENDCG
        }
    }
    Fallback "Specular"
}

