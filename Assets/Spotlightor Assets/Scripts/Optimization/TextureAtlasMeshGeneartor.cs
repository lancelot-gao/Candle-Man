using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextureAtlasMeshGeneartor : ScriptableObject
{
	public enum MeshAnchorPositions
	{
		Center = 0,
		TopLeft = 1,
		TopRight = 2,
		BottomLeft = 3,
		BottomRight = 4,
		Left = 5,
		Right = 6,
		Top = 7,
		Bottom = 8,
	}
	
	[System.Serializable]
	public class TiledSpriteMeshSetting
	{
		[System.Serializable]
		public class SpriteInstanceSetting
		{
			public string name = "bg";
			public TextureAtlasMeshGeneartor.MeshAnchorPositions anchorPosition;
			public int tileX = 2;
			public int tileY = 2;
		}
		public Texture texture;
		public SpriteInstanceSetting[] instances;
		
		public Mesh[] GenerateMeshes (Rect textureRect)
		{
			Mesh[] meshes = new Mesh[instances.Length];
			for (int i = 0; i < instances.Length; i++) {
				SpriteInstanceSetting instanceSetting = instances [i];
				meshes [i] = GenerateMesh (textureRect, instanceSetting);
				SetMeshAnchorPosition (ref meshes [i], instanceSetting.anchorPosition);
			}
			return meshes;
		}
		
		private Mesh GenerateMesh (Rect textureRect, SpriteInstanceSetting instanceSetting)
		{
			Mesh mesh = new Mesh ();
			mesh.name = string.Format ("{0}_tiled_{1}", texture.name.ToLower (), instanceSetting.name);
			
			instanceSetting.tileX = Mathf.Max (1, instanceSetting.tileX);
			instanceSetting.tileY = Mathf.Max (1, instanceSetting.tileY);
			
			int verticeCountX = (instanceSetting.tileX + 1);
			int verticeCountY = (instanceSetting.tileY + 1);
			int verticeCount = verticeCountX * verticeCountY;
			
			Vector3[] vertices = new Vector3[verticeCount];
			Vector2[] uvs = new Vector2[verticeCount];
			for (int x =0; x < verticeCountX; x++) {
				for (int y = 0; y < verticeCountY; y++) {
					Vector3 vertice = Vector3.zero;
					vertice.x = (float)x / (float)(verticeCountX - 1);
					vertice.y = -(float)y / (float)(verticeCountY - 1);
					
					Vector2 uv = Vector2.zero;
					uv.x = x % 2 == 0 ? textureRect.x + 0.001f : textureRect.xMax - 0.001f;
					uv.y = y % 2 == 0 ? textureRect.y + 0.001f : textureRect.yMax - 0.001f;
					
					int verticeIndex = x + y * verticeCountX;
					vertices [verticeIndex] = vertice;
					uvs [verticeIndex] = uv;
				}
			}
			
			int[] triangles = new int[instanceSetting.tileX * instanceSetting.tileY * 2 * 3];
			for (int planeIndexX = 0; planeIndexX < instanceSetting.tileX; planeIndexX++) {
				for (int planeIndexY =0; planeIndexY < instanceSetting.tileY; planeIndexY++) {
					int planeIndex = planeIndexY * instanceSetting.tileX + planeIndexX;
					
					int topLeftVerticeIndex = planeIndexX + planeIndexY * 4;
					int topRightVerticeIndex = topLeftVerticeIndex + 1;
					int bottomLeftVerticeIndex = topLeftVerticeIndex + 4;
					int bottomRightVerticeIndex = bottomLeftVerticeIndex + 1;
					
					int triangleStartIndex = planeIndex * 2 * 3;
					triangles [triangleStartIndex] = topLeftVerticeIndex;
					triangles [triangleStartIndex + 1] = topRightVerticeIndex;
					triangles [triangleStartIndex + 2] = bottomLeftVerticeIndex;
					
					triangles [triangleStartIndex + 3] = topRightVerticeIndex;
					triangles [triangleStartIndex + 4] = bottomRightVerticeIndex;
					triangles [triangleStartIndex + 5] = bottomLeftVerticeIndex;
				}
			}
			mesh.vertices = vertices;
			mesh.triangles = triangles;
			mesh.uv = uvs;
			
			mesh.RecalculateNormals ();
			Vector4[] tangents = new Vector4[verticeCount];
			for (int i = 0; i < tangents.Length; i++)
				tangents [i] = new Vector4 (1, 0, 0, 1);
			mesh.tangents = tangents;
			
			return mesh;
		}
	}

	[System.Serializable]
	public class SlicedSpriteMeshSetting
	{
		/// <summary>
		/// 0	1	2	3
		/// 4	5	6	7
		/// 8	9	10	11
		/// 12	13	14	15
		/// </summary>
		[System.Serializable]
		public class SpriteInstanceSetting
		{
			public string name = "button";
			public TextureAtlasMeshGeneartor.MeshAnchorPositions anchorPosition;
			public int width = 200;
			public int height = 200;
			public float scale = 1f;

			public float WidthAfterScale{ get { return width * scale; } }

			public float HeightAfterScale{ get { return height * scale; } }
		}
		public Texture texture;
		public Texture[] moreTextures;
		public int leftBorder;
		public int rightBorder;
		public int topBorder;
		public int bottomBorder;
		public SpriteInstanceSetting[] instances;

		public Mesh[] GenerateMeshes (Rect textureRect, string textureName)
		{
			Mesh[] meshes = new Mesh[instances.Length];
			for (int i = 0; i < instances.Length; i++) {
				SpriteInstanceSetting instanceSetting = instances [i];
				meshes [i] = GenerateMesh (textureRect, textureName, instanceSetting);
				SetMeshAnchorPosition (ref meshes [i], instanceSetting.anchorPosition);
			}
			return meshes;
		}
		
		public string GetMeshNameForInstanceSetting (string textureName, SpriteInstanceSetting instanceSetting)
		{
			return string.Format ("{0}_sliced_{1}", textureName.ToLower (), instanceSetting.name);
		}
		
		private Mesh GenerateMesh (Rect textureRect, string textureName, SpriteInstanceSetting instanceSetting)
		{
			Mesh mesh = new Mesh ();
			mesh.name = GetMeshNameForInstanceSetting (textureName, instanceSetting);
			
			Vector2 innerPlaneTopLeft = new Vector2 ((float)leftBorder / (float)instanceSetting.WidthAfterScale, - (float)topBorder / (float)instanceSetting.HeightAfterScale);
			Vector2 innerPlaneBottomRight = new Vector2 (1f - (float)rightBorder / (float)instanceSetting.WidthAfterScale, -1 + (float)bottomBorder / (float)instanceSetting.HeightAfterScale);
			
			Rect innerTextureRect = new Rect ();		
			innerTextureRect.x = textureRect.x + textureRect.width * (float)leftBorder / (float)texture.width;
			innerTextureRect.y = textureRect.y + textureRect.height * (float)bottomBorder / (float)texture.height;
			innerTextureRect.width = textureRect.width * (float)(texture.width - leftBorder - rightBorder) / (float)texture.width;
			innerTextureRect.height = textureRect.height * (float)(texture.height - topBorder - bottomBorder) / (float)texture.height;
			
			Vector3[] vertices = new Vector3[16];
			Vector2[] uvs = new Vector2[16];
			for (int i = 0; i < vertices.Length; i++) {
				Vector3 vertice = Vector3.zero;
				Vector2 uv = Vector2.zero;
				
				int divide4Remainder = i % 4;
				if (divide4Remainder == 0) {
					vertice.x = 0;
					uv.x = textureRect.x;
				} else if (divide4Remainder == 1) {
					vertice.x = innerPlaneTopLeft.x;
					uv.x = innerTextureRect.x;
				} else if (divide4Remainder == 2) {
					vertice.x = innerPlaneBottomRight.x;
					uv.x = innerTextureRect.xMax;
				} else {
					vertice.x = 1;
					uv.x = textureRect.xMax;
				}
				
				if (i < 4) {
					vertice.y = 0;
					uv.y = textureRect.yMax;
				} else if (i < 8) {
					vertice.y = innerPlaneTopLeft.y;
					uv.y = innerTextureRect.yMax;
				} else if (i < 12) {
					vertice.y = innerPlaneBottomRight.y;
					uv.y = innerTextureRect.y;
				} else {
					vertice.y = -1;
					uv.y = textureRect.y;
				}
				
				vertices [i] = vertice;
				uvs [i] = uv;
			}
			
			int[] triangles = new int[9 * 2 * 3];
			for (int planeIndexX = 0; planeIndexX < 3; planeIndexX++) {
				for (int planeIndexY =0; planeIndexY < 3; planeIndexY++) {
					int planeIndex = planeIndexY * 3 + planeIndexX;
					
					int topLeftVerticeIndex = planeIndexX + planeIndexY * 4;
					int topRightVerticeIndex = topLeftVerticeIndex + 1;
					int bottomLeftVerticeIndex = topLeftVerticeIndex + 4;
					int bottomRightVerticeIndex = bottomLeftVerticeIndex + 1;
					
					int triangleStartIndex = planeIndex * 2 * 3;
					triangles [triangleStartIndex] = topLeftVerticeIndex;
					triangles [triangleStartIndex + 1] = topRightVerticeIndex;
					triangles [triangleStartIndex + 2] = bottomLeftVerticeIndex;
					
					triangles [triangleStartIndex + 3] = topRightVerticeIndex;
					triangles [triangleStartIndex + 4] = bottomRightVerticeIndex;
					triangles [triangleStartIndex + 5] = bottomLeftVerticeIndex;
				}
			}
			mesh.vertices = vertices;
			mesh.triangles = triangles;
			mesh.uv = uvs;
			
			mesh.RecalculateNormals ();
			Vector4[] tangents = new Vector4[16];
			for (int i = 0; i < tangents.Length; i++)
				tangents [i] = new Vector4 (1, 0, 0, 1);
			mesh.tangents = tangents;
			
			return mesh;
		}
	}

	[System.Serializable]
	public class SpriteMeshAnchorPositionSetting
	{
		public Texture texture;
		public MeshAnchorPositions anchorPosition;
	}

	public TextureAtlas atlas;
	public MeshAnchorPositions defaultMeshAnchorPosition;
	public SpriteMeshAnchorPositionSetting[] specialSpriteMeshAnchorPositions;
	public SlicedSpriteMeshSetting[] slicedSprites;
	public TiledSpriteMeshSetting[] tiledSprites;
	
	public Mesh[] GenerateAllMeshes ()
	{
		List<Mesh> result = new List<Mesh> ();
		if (atlas != null) {
			foreach (Texture texture in atlas.textures) {
				Rect textureRect = atlas.GetTextureAtlasRect (texture);
				Mesh spriteMesh = GenerateMeshForSprite (textureRect);
				
				MeshAnchorPositions anchorPos = defaultMeshAnchorPosition;
				if (specialSpriteMeshAnchorPositions.Length > 0) {
					SpriteMeshAnchorPositionSetting specialCase = System.Array.Find<SpriteMeshAnchorPositionSetting> (specialSpriteMeshAnchorPositions, element => element.texture == texture);
					if (specialCase != null)
						anchorPos = specialCase.anchorPosition;
				}
				
				SetMeshAnchorPosition (ref spriteMesh, anchorPos);
				spriteMesh.name = texture.name.ToLower ();
				result.Add (spriteMesh);
			}
			
			if (slicedSprites != null && slicedSprites.Length > 0) {
				foreach (SlicedSpriteMeshSetting meshSetting in slicedSprites) {
					Mesh[] slicedSpriteMeshes = meshSetting.GenerateMeshes (atlas.GetTextureAtlasRect (meshSetting.texture), meshSetting.texture.name);
					result.AddRange (slicedSpriteMeshes);
					foreach (Texture texture in meshSetting.moreTextures) {
						slicedSpriteMeshes = meshSetting.GenerateMeshes (atlas.GetTextureAtlasRect (texture), texture.name);
						result.AddRange (slicedSpriteMeshes);
					}
				}
			}
			
			if (tiledSprites != null && tiledSprites.Length > 0) {
				foreach (TiledSpriteMeshSetting meshSetting in tiledSprites) {
					Mesh[] tiledSpriteMeshes = meshSetting.GenerateMeshes (atlas.GetTextureAtlasRect (meshSetting.texture));
					result.AddRange (tiledSpriteMeshes);
				}
			}
		} else
			Debug.LogError ("atlas is null!");
		return result.ToArray ();
	}
	
	public Mesh GenerateMeshForSprite (Rect textureRect)
	{
		Mesh mesh = new Mesh ();
		
		Vector3[] vertices = new Vector3[4];
		vertices [0] = new Vector3 (0, 0, 0);
		vertices [1] = new Vector3 (1, 0, 0);
		vertices [2] = new Vector3 (1, -1, 0);
		vertices [3] = new Vector3 (0, -1, 0);
		
		Vector2[] uvs = new Vector2[4];
		
		uvs [0] = new Vector2 (textureRect.x, textureRect.yMax);
		uvs [1] = new Vector2 (textureRect.xMax, textureRect.yMax);
		uvs [2] = new Vector2 (textureRect.xMax, textureRect.y);
		uvs [3] = new Vector2 (textureRect.x, textureRect.y);
		
		int[] triangles = new int[6];
		triangles [0] = 0;
		triangles [1] = 1;
		triangles [2] = 3;
		triangles [3] = 1;
		triangles [4] = 2;
		triangles [5] = 3;
		
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		
		mesh.RecalculateNormals ();
		mesh.tangents = new Vector4[4]{
			new Vector4 (1, 0, 0, 1),
			new Vector4 (1, 0, 0, 1),
			new Vector4 (1, 0, 0, 1),
			new Vector4 (1, 0, 0, 1)
		};
		
		return mesh;
	}
	
	private static void SetMeshAnchorPosition (ref Mesh mesh, MeshAnchorPositions anchorPosition)
	{
		if (anchorPosition != MeshAnchorPositions.TopLeft) {
			Vector3 offset = Vector3.zero;
			if (anchorPosition == MeshAnchorPositions.BottomLeft)
				offset.y = 1;
			else if (anchorPosition == MeshAnchorPositions.BottomRight) {
				offset.x = -1;
				offset.y = 1;
			} else if (anchorPosition == MeshAnchorPositions.TopRight)
				offset.x = -1;
			else if (anchorPosition == MeshAnchorPositions.Center) {
				offset.x = -0.5f;
				offset.y = 0.5f;
			} else if (anchorPosition == MeshAnchorPositions.Left) {
				offset.y = 0.5f;
			} else if (anchorPosition == MeshAnchorPositions.Right) {
				offset.x = -1;
				offset.y = 0.5f;
			} else if (anchorPosition == MeshAnchorPositions.Top) {
				offset.x = -0.5f;
			} else if (anchorPosition == MeshAnchorPositions.Bottom) {
				offset.x = -0.5f;
				offset.y = 1;
			}
		
			Vector3[] vertices = new Vector3[mesh.vertices.Length];
			for (int i = 0; i < vertices.Length; i++) {
				vertices [i] = mesh.vertices [i] + offset;
			}
			mesh.vertices = vertices;
		}
		mesh.RecalculateNormals ();
		mesh.RecalculateBounds ();
	}
}
