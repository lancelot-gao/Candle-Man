using UnityEngine;
using System.Collections;

public class GameSceneController : MonoBehaviour
{
	private int loadLevelIndex = 0;
	// Use this for initialization
	IEnumerator Start ()
	{
		Time.timeScale = 1;
		Messenger.AddListener (MessageTypes.GameEnded, OnPlayerDead);
		
		GlobalBlackScreenTransition.Instance.TransitionIn (true);
		GlobalBlackScreenTransition.Instance.TransitionOut ();
		yield return new WaitForSeconds(GlobalBlackScreenTransition.Instance.TotalTransitionInTime);
	}
	
	private void OnPlayerDead (object data)
	{
		GameContext.Player.GetComponent<ThirdPersonController> ().enabled = false;
		GameContext.Player.LightController.interactive = false;
		
		bool win = (bool)data;
		if (win) {
			GameContext.Player.GetComponentInChildren<Animation> ().Play ("win");
			GameContext.Player.LightController.LightOn = true;
			GameContext.Player.LightController.RefillLightLeftTime ();
		} else {
			GameContext.Player.GetComponentInChildren<Animation> ().Stop ();
			GameContext.Player.LightController.LightOn = false;
			RenderSettings.ambientLight = Color.red;
		}
		
		StartCoroutine (DelayAndLoadLevel (win));
	}
	
	private IEnumerator DelayAndLoadLevel (bool win)
	{
		yield return new WaitForSeconds(3);
		
		GlobalBlackScreenTransition.Instance.TransitionIn ();
		yield return new WaitForSeconds(GlobalBlackScreenTransition.Instance.TotalTransitionInTime);
		
		loadLevelIndex = win ? Application.loadedLevel + 1 : Application.loadedLevel;
		Application.LoadLevel (loadLevelIndex);
	}
}
