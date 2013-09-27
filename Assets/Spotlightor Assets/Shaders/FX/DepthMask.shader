Shader "FX/DepthMask" {
	SubShader
	{
		Lod 100

		Tags
		{
			"Queue" = "Geometry+1"
			"RenderType"="Opaque"
		}
		
		Pass
        {
            ZWrite On
            ZTest LEqual
            ColorMask 0
        }
	}
}
