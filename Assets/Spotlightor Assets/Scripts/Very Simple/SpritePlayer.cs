using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class SpritePlayer : EnhancedMonoBehaviour
{
	public bool autoPlay = false;
	public float autoPlayTimePerLoop = 1f;
	public bool pingPongLoop = false;
	public int numImagesPerRow = 1;
	public int numImagesPerColumn = 1;
	private int totalNumberOfImages = 1;
	private int currentImageIndex = 0;
	private bool isLoopingForward = true;
	
	public int CurrentImageIndex {
		get { return currentImageIndex;}
	}
	
	public int ImageCount {
		get { return Mathf.Max (1, numImagesPerRow * numImagesPerColumn);}
	}
	
	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		if (forTheFirstTime) {
			Initialize ();
		}
		if (autoPlay)
			StartLooping (autoPlayTimePerLoop / ImageCount);
	}
	
	protected override void OnBecameUnFunctional ()
	{
		StopLooping ();
	}
	
	private void Initialize ()
	{
		totalNumberOfImages = numImagesPerRow * numImagesPerColumn;
		renderer.material.mainTextureScale = new Vector2 (1f / numImagesPerRow, 1f / numImagesPerColumn);
	}
	
	public void StartLooping (float delay)
	{
		StartCoroutine ("Loop", delay);
	}
	
	public void StopLooping ()
	{
		StopCoroutine ("Loop");
	}
	
	IEnumerator Loop (float delay)
	{
		while (true) {
			yield return new WaitForSeconds(delay);
			if (pingPongLoop) {
				if (CurrentImageIndex >= ImageCount - 1 && isLoopingForward)
					isLoopingForward = false;
				else if (CurrentImageIndex <= 0 && !isLoopingForward)
					isLoopingForward = true;
			}
			if (isLoopingForward)
				ShowNextImage ();
			else
				ShowPrevImage ();
		}
	}
	
	public void ShowNextImage ()
	{
		ShowImageByIndex (currentImageIndex + 1);
	}
	
	public void ShowPrevImage ()
	{
		ShowImageByIndex (currentImageIndex - 1);
	}
	
	public void ShowImageByIndex (int index)
	{
		int validIndex = index %= totalNumberOfImages;
		if (validIndex < 0)
			validIndex += totalNumberOfImages;
		
		this.currentImageIndex = validIndex;
		int uIndex = validIndex % numImagesPerRow;
		int vIndex = validIndex / numImagesPerRow;
		renderer.material.mainTextureOffset = new Vector2 ((float)uIndex * renderer.material.mainTextureScale.x, (float)vIndex * renderer.material.mainTextureScale.y);
	}
}
