Shader "FX/HSV Diffuse" {

    Properties {
   		_Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _HueShift("HueShift", Float ) = 0
        _Sat("Saturation", Float) = 1
        _Val("Value", Float) = 1
    }

    SubShader {
 		Tags {"RenderType"="Opaque"}
        //Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType" = "Transparent" }
        //ZWrite Off
        //Blend SrcAlpha OneMinusSrcAlpha
        //Cull Off
   
            CGPROGRAM
			 #pragma surface surf Lambert

      #pragma target 3.0

			
            #include "UnityCG.cginc"

            float3 shift_col(float3 RGB, float3 shift)
            {
	            float3 RESULT = float3(RGB);
	            float VSU = shift.z*shift.y*cos(shift.x*3.14159265/180);
	                float VSW = shift.z*shift.y*sin(shift.x*3.14159265/180);
	
	                RESULT.x = (.299*shift.z+.701*VSU+.168*VSW)*RGB.x
	                        + (.587*shift.z-.587*VSU+.330*VSW)*RGB.y
	                        + (.114*shift.z-.114*VSU-.497*VSW)*RGB.z;
	                
	                RESULT.y = (.299*shift.z-.299*VSU-.328*VSW)*RGB.x
	                        + (.587*shift.z+.413*VSU+.035*VSW)*RGB.y
	                        + (.114*shift.z-.114*VSU+.292*VSW)*RGB.z;
	                
	                RESULT.z = (.299*shift.z-.3*VSU+1.25*VSW)*RGB.x
	                        + (.587*shift.z-.588*VSU-1.05*VSW)*RGB.y
	                        + (.114*shift.z+.886*VSU-.203*VSW)*RGB.z;
	                
	            return (RESULT);
            }


 
            //float4 _MainTex_ST;
            fixed4 _Color;// by lancelot

 
            sampler2D _MainTex;
            float _HueShift;
            float _Sat;
            float _Val;
 			
 			struct Input {
				float2 uv_MainTex;
			};
			
            //half4 frag(v2f i) : COLOR
            //{
             //   half4 col = tex2D(_MainTex, i.uv);
             //   float3 shift = float3(_HueShift, _Sat, _Val);
                
              //  return half4( half3(shift_col(col, shift)), col.a) * _Color;// by lancelot
            //}
            void surf (Input IN, inout SurfaceOutput o) {
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				float3 shift = float3(_HueShift, _Sat, _Val);
				c = half4( half3(shift_col(c.rgb, shift)), c.a) * _Color;
				c *= _Color;
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}
            ENDCG
        
    }
    Fallback "Diffuse"
}