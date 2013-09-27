using UnityEngine;
using System.Collections;

public class HintTrigger : BasePlayerTrigger
{
	public LocalizedText hintText;
	#region implemented abstract members of BasePlayerTrigger
	protected override void OnPlayerEnter (Player player)
	{
		GameHintTextController.Instance.DisplayHintText (hintText);
	}

	protected override void OnPlayerExit (Player player)
	{
		GameHintTextController.Instance.ClearHintText ();
	}
	#endregion
	
}
