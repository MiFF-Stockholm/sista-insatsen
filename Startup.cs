using UnityEngine;
using System.Collections;

public class Startup : MonoBehaviour {
	public GameObject player;
	void Start () {
		if(Globals.translateOnStartup) {
			Debug.Log("Starting up. Mocing player to " + Globals.initialPosition);
			player.transform.position = Globals.initialPosition;
			Globals.translateOnStartup = false;
			Globals.gameOverDelegate = new Globals.GameOverDelegate(GameOver);
		}
	}

	public static void GameOver() {
		// Enter Game over code here!
	}
}
