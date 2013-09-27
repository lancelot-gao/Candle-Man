Shader "Unlit/Texture Color Tint" 
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
			SetTexture [_MainTex] 
			{
				ConstantColor [_Color]
				Combine Texture * constant
			}
		}
	}
}