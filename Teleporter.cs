using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {
	public Vector3 target;
	public string levelToLoad = "none";

	void OnTriggerEnter(Collider other) {
	}

	void OnTriggerStay(Collider other) {
		if(other.name == "Player") {
			if(levelToLoad != "none" ) {
				Globals.initialPosition = target;
				Globals.translateOnStartup = true;
				Globals.PreBattleLevelName = Application.loadedLevelName;
				Application.LoadLevel(levelToLoad);
			} else {
				other.transform.position = target;
			}
		}
	}



	void OnTriggerExit(Collider other) {
	}
}
