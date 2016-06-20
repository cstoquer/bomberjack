Shader "Sprites/Indexed" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture",  2D) = "white" {}

		// palette index: this is defined for the material (all sprites share this value)
		_Index("Palette Index", Range(0, 1)) = 0

		// NOTA: for this texture, it is important to have it setup like this:
		//       - Generate MipMap: OFF
		//       - Filter Mode: Point (no filter)
		// The texture type can be left as Sprite 2D
		_Palette ("Palette Texture", 2D) = "white" {}

		

		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}

	SubShader {
		Tags { 
			"Queue"            = "Transparent" 
			"IgnoreProjector"  = "True" 
			"RenderType"       = "Transparent" 
			"PreviewType"      = "Plane"
			"CanUseSpriteAtlas"= "True"
		}

		Cull     Off
		Lighting Off
		ZWrite   Off
		Blend    One OneMinusSrcAlpha

		Pass {
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
			struct appdata_t {
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex   : SV_POSITION;
				//fixed4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};
			
			//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
			v2f vert(appdata_t IN) {
				v2f OUT;
				OUT.vertex   = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				//OUT.color    = IN.color;

				#ifdef PIXELSNAP_ON
				OUT.vertex   = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
			sampler2D _MainTex;
			sampler2D _Palette;
			float     _Index;
			sampler2D _AlphaTex;
			float     _AlphaSplitEnabled;


			//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
			fixed4 frag(v2f IN) : SV_Target {
				fixed4 p = tex2D(_MainTex, IN.texcoord);
				fixed4 c = tex2D(_Palette, float2(p.r, _Index));
				c.a = p.a;

				#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED

					if (_AlphaSplitEnabled) c.a = tex2D(_AlphaTex, IN.texcoord).r;

				#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
}
