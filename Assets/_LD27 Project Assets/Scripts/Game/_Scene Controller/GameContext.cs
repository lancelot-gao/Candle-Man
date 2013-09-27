using UnityEngine;
using System.Collections;

public static class GameContext
{
	private static Player player;

	public static Player Player {
		get {
			if (player == null)
				player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
			return player;
		}
	}
}
