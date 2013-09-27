using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Translate iPhone touch event to mouse event.
[RequireComponent(typeof(Camera))]
public class TouchToMouseEventTranslator : MonoBehaviour
{
	private List<GameObject> touchedGos;

	public List<GameObject> TouchedGos {
		get {
			if (touchedGos == null)
				touchedGos = new List<GameObject> ();
			return touchedGos;
		}
	}
	
	void AssignTouchedGo (GameObject go, int touchID)
	{
		if (go == null) {
			Debug.LogError ("go cannot be null.");
			return;
		}
		
		if (TouchedGos.Count <= touchID) {
			int numNewData = touchID - TouchedGos.Count + 1;
			for (int i = 0; i < numNewData; i++)
				TouchedGos.Add (null);
		}
		
		TouchedGos [touchID] = go;
	}
	
	void RemoveTouchedGo (int touchId)
	{
		if (TouchedGos.Count <= touchId) {
			Debug.Log ("Nothing to remove.");
			return;
		}
		
		TouchedGos [touchId] = null;
	}
	
	GameObject GetTouchedGoByTouchId (int touchId)
	{
		if (touchId >= TouchedGos.Count)
			return null;
		return TouchedGos [touchId];
	}
	
	void Update ()
	{
		RaycastHit hit = new RaycastHit ();
		for (int i = 0; i < Input.touchCount; ++i) {
			Touch touch = Input.GetTouch (i);
			TouchPhase phase = touch.phase;
			GameObject prevTouchedGo = GetTouchedGoByTouchId (i);
			
			Ray ray = camera.ScreenPointToRay (touch.position);
			if (Physics.Raycast (ray, out hit)) {
				GameObject touchedGo = hit.transform.gameObject;
				
				if (phase.Equals (TouchPhase.Began)) {
					AssignTouchedGo (touchedGo, i);
					prevTouchedGo = touchedGo;
					SimulateMouseMessages (touchedGo, "OnMouseDown");
				} else if (phase.Equals (TouchPhase.Ended) || phase.Equals (TouchPhase.Canceled)) {
					SimulateMouseMessages (touchedGo, "OnMouseUp");
					if (prevTouchedGo == touchedGo)
						SimulateMouseMessages (touchedGo, "OnMouseUpAsButton");
					SimulateMouseMessages (touchedGo, "OnMouseExit");
					RemoveTouchedGo (i);
				} else {
					if (touchedGo != prevTouchedGo) {
						AssignTouchedGo (touchedGo, i);
						if (prevTouchedGo != null)
							SimulateMouseMessages (prevTouchedGo, "OnMouseExit");
						SimulateMouseMessages (touchedGo, "OnMouseEnter");
						SimulateMouseMessages (touchedGo, "OnMouseDown");
					} 
				}
			} else {
				if (prevTouchedGo != null) {
					RemoveTouchedGo (i);
					SimulateMouseMessages (prevTouchedGo, "OnMouseExit");
				}
			}
		}
	}
	
	private void SimulateMouseMessages (GameObject target, string message)
	{
		if (Input.touchCount > 1)// If touchCount = 1, system will simulate mouse events automaticly.
			target.SendMessage (message, SendMessageOptions.DontRequireReceiver);
	}
}
