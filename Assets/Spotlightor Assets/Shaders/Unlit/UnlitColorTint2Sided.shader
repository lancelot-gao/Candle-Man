Shader "Unlit/Texture Color Tint(2 Sided)" 
{
    Properties 
    {
        _Color ("Color Tint", Color) = (1,1,1,1)    
        _MainTex ("Base (RGB)", 2D) = "white"
    }
        
	SubShader{
		Tags {RenderType=Opaque}
		
		Pass {
			Lighting Off
			Cull Off
			SetTexture [_MainTex] 
			{
				ConstantColor [_Color]
				Combine Texture * constant
			}
		}
	}
}