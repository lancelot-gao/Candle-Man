using UnityEngine;
using System.Collections;

public class SmartUnityGuiLabel : MonoBehaviour , ITransition
{

	public enum PositionMethod
	{
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight,
		
		Top,
		Bottom,
		Smart
	}
	
	private const float LazyRange = 50;// in pixels
	
	public string text = "LABEL";
	public GUISkin guiSkin;
	public string styleName = "Tooltip";
	public Color tintColor = Color.white;
	public PositionMethod positionMethod = PositionMethod.Smart;
	public float offsetX;
	public float offsetY;
	public float transitionTime = 0.3f;
	private GUIStyle labelStyle;
	private Vector2 screenDrawPosition = Vector2.zero;
	private bool drawAtRight = true;
	private bool drawAtBottom = true;
	
	public bool DrawAtRight {
		get { return drawAtRight;}
	}
	
	public bool DrawAtBottom {
		get { return drawAtBottom;}
	}
	
	public GUIStyle LabelStyle {
		get {
			if (labelStyle == null) {
				if (guiSkin != null) {
					labelStyle = guiSkin.GetStyle (styleName);
					if (labelStyle == null)
						Debug.LogError ("Cannot get style named " + styleName + " in " + guiSkin.name);
				} else
					Debug.LogError ("No GUI skin assigned!");
			}
			return labelStyle;
		}
	}
	
	public void UpdateDrawPositionInWorld (Vector3 worldPosition)
	{
		Vector3 screenPos = Camera.main.WorldToScreenPoint (worldPosition);
		UpdateDrawPositionOnScreen (new Vector2 (screenPos.x, screenPos.y));
	}
	
	public void UpdateDrawPositionOnScreen (Vector2 screenPosition)
	{
		this.screenDrawPosition = screenPosition;
		switch (positionMethod) {
		case PositionMethod.Smart:
			MakeSureDrawUpOrDown (screenDrawPosition);
			MakeSureDrawLeftOrRight (screenDrawPosition);
			break;
		case PositionMethod.Top:
			MakeSureDrawLeftOrRight (screenDrawPosition);
			drawAtBottom = false;
			break;
		case PositionMethod.Bottom:
			MakeSureDrawLeftOrRight (screenDrawPosition);
			drawAtBottom = true;
			break;
		case PositionMethod.BottomLeft:
			drawAtBottom = true;
			drawAtRight = false;
			break;
		case PositionMethod.BottomRight:
			drawAtBottom = true;
			drawAtRight = true;
			break;
		case PositionMethod.TopLeft:
			drawAtBottom = false;
			drawAtRight = false;
			break;
		case PositionMethod.TopRight:
			drawAtBottom = false;
			drawAtRight = true;
			break;
		}
	}
	
	protected void MakeSureDrawLeftOrRight (Vector2 screenPosition)
	{
		if (drawAtRight && screenPosition.x > 0.5f * Screen.width + LazyRange)
			drawAtRight = false;
		else if (!drawAtRight && screenPosition.x < 0.5f * Screen.width - LazyRange)
			drawAtRight = true;
	}
	
	protected void MakeSureDrawUpOrDown (Vector2 screenPosition)
	{
		if (drawAtBottom && screenPosition.y < 0.5f * Screen.height - LazyRange)
			drawAtBottom = false;
		else if (!drawAtBottom && screenPosition.y > 0.5f * Screen.height + LazyRange)
			drawAtBottom = true;
	}

	void OnGUI ()
	{
		if (tintColor.a > 0 && LabelStyle != null && text != null && text != "") {
			if (screenDrawPosition.x < 0 || screenDrawPosition.y < 0 || screenDrawPosition.x > Screen.width || screenDrawPosition.y > Screen.height)
				return;

			GUI.color = tintColor;
			
			GUIContent content = new GUIContent (text);
			
			float contentWidthMax = labelStyle.fixedWidth;
			float contentWidthMin = 0;
			float contentHeight = labelStyle.CalcHeight (content, contentWidthMax);
			//
			labelStyle.CalcMinMaxWidth (content, out contentWidthMin, out contentWidthMax);
			float labelHeight = contentHeight + /*LabelStyle.border.top + LabelStyle.border.bottom*/ + LabelStyle.overflow.top + LabelStyle.overflow.bottom;
			float labelWidth = contentWidthMax + LabelStyle.border.left + LabelStyle.border.right + LabelStyle.overflow.left + LabelStyle.overflow.right;
			
			float drawX = this.screenDrawPosition.x;
			float drawY = Screen.height - this.screenDrawPosition.y;
			drawX += drawAtRight ? offsetX : -labelWidth - offsetX;
			drawY += drawAtBottom ? offsetY : -labelHeight - offsetY;
			GUI.Label (new Rect (drawX, drawY, labelWidth, labelHeight), content, labelStyle);
			
		}
	}
	
	void OnDisable()
	{
		TransitionOut(true);
	}
	
	#region ITransition implementation
	public void TransitionIn ()
	{
		TransitionIn (false);
	}

	public void TransitionIn (bool instant)
	{
		if (instant) {
			tintColor.a = 1;
		} else {
			StopCoroutine ("TweenAlpha");
			StartCoroutine ("TweenAlpha", 1);
		}
		enabled = true;
	}

	public void TransitionOut ()
	{
		TransitionOut (false);
	}

	public void TransitionOut (bool instant)
	{
		if (instant) {
			tintColor.a = 0;
			enabled = false;
		} else {
			StopCoroutine ("TweenAlpha");
			StartCoroutine ("TweenAlpha", 0);
		}
	}
	#endregion
	
	IEnumerator TweenAlpha (float targetAlpha)
	{
		float startAlpha = tintColor.a;
		float duration = Mathf.Abs (targetAlpha - startAlpha) * transitionTime;
		float timeElapsed = 0;
		while (timeElapsed < duration) {
			yield return 1;
			timeElapsed += Time.deltaTime;
			float t = Mathf.Clamp01 (timeElapsed / duration);
			tintColor.a = Mathf.Lerp (startAlpha, targetAlpha, t);
		}
		if (tintColor.a == 0)
			enabled = false;
	}
}
