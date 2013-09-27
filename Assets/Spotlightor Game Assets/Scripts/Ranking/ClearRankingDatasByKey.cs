using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPrefsRankingModel))]
public class ClearRankingDatasByKey : MonoBehaviour
{
	private const float ConfirmationMaxWaitTime = 5;
	public KeyCode[] keyCombination;
	private bool doingConfirmation = false;
	private float confirmationTimeElapsed = 0;
	
	// Update is called once per frame
	void Update ()
	{
		bool keyCombinationPressed = true;
		for (int i = 0; i < keyCombination.Length-1; i++) {
			if (Input.GetKey (keyCombination [i]) == false) {
				keyCombinationPressed = false;
				break;
			}
		}
		if (Input.GetKeyDown (keyCombination [keyCombination.Length - 1]) == false)
			keyCombinationPressed = false;
		
		if (keyCombinationPressed) {
			if (doingConfirmation) {
				GetComponent<PlayerPrefsRankingModel> ().ClearRankingDatas ();
				doingConfirmation = false;
				confirmationTimeElapsed = 0;
				Debug.Log ("Ranking datas cleared.");
			} else {
				doingConfirmation = true;
				confirmationTimeElapsed = 0;
			}
		} else {
			if (doingConfirmation) {
				confirmationTimeElapsed += Time.deltaTime;
				if (confirmationTimeElapsed >= ConfirmationMaxWaitTime) {
					doingConfirmation = false;
					confirmationTimeElapsed = 0;
				}
			}
		}
	}
	
	void OnGUI ()
	{
		if (doingConfirmation) {
			Rect confirmationBoxRect = new Rect ();
			confirmationBoxRect.width = 320;
			confirmationBoxRect.height = 60;
			confirmationBoxRect.center = new Vector2 (Screen.width * 0.5f, Screen.height * 0.5f);
			GUI.Box (confirmationBoxRect, "Try clearing ranking datas!\nPress the key combination again to continue.\nTime left: " +
				string.Format ("{0:0.0}", ConfirmationMaxWaitTime - confirmationTimeElapsed));
		}
	}
}
