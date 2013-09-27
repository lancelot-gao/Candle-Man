using UnityEngine;
using System.Collections;

public class LightControlEnableTrigger : BasePlayerTrigger
{
	public bool enableLightControl = true;
	#region implemented abstract members of BasePlayerTrigger
	protected override void OnPlayerEnter (Player player)
	{
		player.LightController.interactive = enableLightControl;
		if (!enableLightControl)
			player.LightController.LightOn = false;
		Destroy (gameObject);
	}

	protected override void OnPlayerExit (Player player)
	{
	}
	#endregion
}
