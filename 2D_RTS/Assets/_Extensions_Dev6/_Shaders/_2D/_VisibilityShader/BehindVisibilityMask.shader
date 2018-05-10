// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/BehindVisibilityMask"
{
	/*****************************************************
		This are the Properties shown in the unity exploer
	*******************************************************/
	Properties
	{
		[Header(Default Sprite Shader)]
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint Everything", Color) = (1, 1, 1, 1)

		[Header(Hidden Area)]
		[Toggle(ENABLE_HIDDEN)] _HIDDEN("Enable?", Float) = 1
		_HiddenColor("Area Tint", Color) = (1, 1, 1, 1)
		_EffectAmountH("Greyscale", Range(0, 1)) = 1.0
		
		[Header(Visible Area)]
		[Toggle(ENABLE_VISIBLE)] _VISIBLE("Enable?", Float) = 1
		_VisibleColor("Area Tint", Color) = (1, 1, 1, 1)
		_EffectAmountV("Greyscale", Range(0, 1)) = 0.0
	}


	/*****************************************************
		Shared content START : for all passes inside of CG include
	*******************************************************/
	CGINCLUDE
		struct appdata_t
		{
			float4 vertex   : POSITION;
			float4 color    : COLOR;
			float2 texcoord : TEXCOORD0;
		};
		struct v2f
		{
			float4 vertex   : SV_POSITION;
			fixed4 color : COLOR;
			half2 texcoord  : TEXCOORD0;
		};
		fixed4 _Color;
		sampler2D _MainTex;
		uniform float _HIDDEN;
		uniform float _VISIBLE;
		uniform float _EffectAmountH;
		uniform float _EffectAmountV;
	ENDCG //end of shared content of CGINCLUDE
	/*****************************************************
		Shared content END
	*******************************************************/


	/*****************************************************
		Actual Shader Settings
	*******************************************************/
	SubShader
	{
		Tags{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
			}
		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		/*****************************************************************
			FIRST SHADER PASS : Hidden Area not behind visibility mask
		******************************************************************/
		Pass
		{
			Stencil
			{
				Ref 4
				Comp NotEqual
			}


			CGPROGRAM //CGPROGRAMM START
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile _ PIXELSNAP_ON
				#include "UnityCG.cginc"

				fixed4 _HiddenColor;

				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);
					OUT.texcoord = IN.texcoord;
					OUT.color = IN.color * _Color;
					#ifdef PIXELSNAP_ON
						OUT.vertex = UnityPixelSnap(OUT.vertex);
					#endif
					return OUT;
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					//fixed4 c = tex2D(_MainTex, IN.texcoord) * IN.color;
					//c.rgb *= c.a;
					//return c;
					if(_HIDDEN > 0)
					{
						half4 texcol = tex2D(_MainTex, IN.texcoord);
						texcol.rgb = lerp(texcol.rgb, dot(texcol.rgb, float3(0.3, 0.59, 0.11)), _EffectAmountH);
						//half4 hcwna = _HiddenColor;
						texcol = texcol * IN.color * _HiddenColor;
						texcol.a = 1;
						return texcol;
					}
					else
					{
						return fixed4(0,0,0,1);
					}
				}
			ENDCG //CGPROGRAMM END
		}
		//FIRST PASS END


		/*****************************************************
			SECOND SHADER PASS : Visible Area behind Mask
		*******************************************************/
		Pass
		{
			Stencil
			{
				Ref 4
				Comp Equal
			}

			CGPROGRAM //CGPROGRAMM START
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile _ PIXELSNAP_ON
				#include "UnityCG.cginc"

				fixed4 _VisibleColor;

				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);
					OUT.texcoord = IN.texcoord;
					OUT.color = IN.color * _Color;
					#ifdef PIXELSNAP_ON
						OUT.vertex = UnityPixelSnap(OUT.vertex);
					#endif
					return OUT;
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					if(_VISIBLE)
					{
						half4 texcol = tex2D(_MainTex, IN.texcoord);
						texcol.rgb = lerp(texcol.rgb, dot(texcol.rgb, float3(0.3, 0.59, 0.11)), _EffectAmountV);
						texcol = texcol * IN.color * _VisibleColor;
						texcol.a = 1;
						return texcol;
					}
					else
					{
						return fixed4(0,0,0,1);
					}
				}
			ENDCG //CGPROGRAMM END
		}
		//SECOND PASS END
	}
}