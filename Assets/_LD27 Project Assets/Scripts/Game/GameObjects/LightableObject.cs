using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class LightableObject : BasePlayerTrigger
{
	public Light targetLight;
	public ParticleSystem particle;
	public float time = 10;

	void Awake ()
	{
		targetLight.enabled = false;
	}

	#region implemented abstract members of BasePlayerTrigger
	protected override void OnPlayerEnter (Player player)
	{
		GameContext.Player.LightController.LightStateChanged += HandleLightStateChanged;
	}

	protected override void OnPlayerExit (Player player)
	{
		GameContext.Player.LightController.LightStateChanged -= HandleLightStateChanged;
	}
	#endregion
	
	void HandleLightStateChanged (bool lightOn)
	{
		if (lightOn) {
			GameContext.Player.LightController.LightStateChanged -= HandleLightStateChanged;
			
			targetLight.enabled = true;
			particle.Play ();
			
			collider.enabled = false;
			enabled = false;
			
			GameHintTextController.Instance.ClearHintText ();
			Destroy (GetComponent<HintTrigger> ());
			
			Invoke ("DisableLight", time);
		}
	}
	
	void DisableLight ()
	{
		particle.Stop ();
		targetLight.enabled = false;
	}
}
