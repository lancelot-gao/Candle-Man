using UnityEngine;
using System.Collections;

public class TextureAtlas : ScriptableObject
{
	public int textureSize = 1024;
	public int padding = 2;
	public Texture2D[] textures;
	[HideInInspector]
	[SerializeField]
	public Rect[] textureRects;
	
	public Texture2D Pack ()
	{
		Texture2D atlas = new Texture2D (textureSize, textureSize);
		textureRects = atlas.PackTextures (textures, padding, textureSize, false);
		return atlas;
	}
	
	public Rect GetTextureAtlasRect (Texture texture)
	{
		int textureIndex = System.Array.IndexOf (textures, texture);
		return textureRects [textureIndex];
	}
}
