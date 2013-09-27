using UnityEngine;
using System.Collections;

public class FinishTrigger : BasePlayerTrigger
{

	#region implemented abstract members of BasePlayerTrigger
	protected override void OnPlayerEnter (Player player)
	{
		Messenger.Broadcast (MessageTypes.GameEnded, true);
	}

	protected override void OnPlayerExit (Player player)
	{
	}
	#endregion
}
