using UnityEngine;
using System.Collections;

public class CustomCursorByTag : EnhancedMonoBehaviour {
	[System.Serializable]
	public class CursorOfTag{
		public string tag;
		public Texture2D cursorTexture;
		public Vector2 offset;
		public bool showDefaultCursor = false;
	}
	
	public CursorOfTag[] cursorOfTags;
	public LayerMask mask;
	private CursorOfTag _activeCursor;
	
	public CursorOfTag ActiveCursor{
		get {return _activeCursor;}
		set {
			if(_activeCursor == value)return;
			_activeCursor = value;
			if(_activeCursor != null){
				if(_activeCursor.showDefaultCursor)Screen.showCursor = true;
				else Screen.showCursor = false;
			} else{
				Screen.showCursor = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, Camera.main.farClipPlane, mask)){
			string tag = hit.collider.gameObject.tag;
			ActiveCursor = FindCursorOfTag(tag);
		}else{
			ActiveCursor = null;
		}
	}
	
	CursorOfTag FindCursorOfTag(string tag){
		foreach(CursorOfTag cot in cursorOfTags){
			if(cot.tag == tag)return cot;
		}
		return null;
	}
	
	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		if(forTheFirstTime){
			
		}
	}
	
	protected override void OnBecameUnFunctional ()
	{
		ActiveCursor = null;
	}
	
	void OnGUI ()
	{
		if (ActiveCursor != null) {
			Vector3 mousePos = Input.mousePosition;
			Rect drawRect = new Rect (mousePos.x + ActiveCursor.offset.x, Screen.height - mousePos.y + ActiveCursor.offset.y, 
			                          ActiveCursor.cursorTexture.width, ActiveCursor.cursorTexture.height);
			GUI.DrawTexture (drawRect, ActiveCursor.cursorTexture);
		}
	}
}
