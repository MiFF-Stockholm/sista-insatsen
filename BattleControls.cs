using UnityEngine;
using System.Collections;

public class BattleControls : MonoBehaviour {
	ActionMarkerArrow actionMarker;
	PlayerMarkerArrow playerMarker;
	EnemyMarkerArrow enemyMarker;
	AbilityMarkerArrow abilityMarker;

	void Start () {
		GameObject obj = GameObject.Find ("popupArrowMarker");
		actionMarker = (ActionMarkerArrow) obj.GetComponent ("ActionMarkerArrow");
		obj = GameObject.Find ("playerArrowMarker");
		playerMarker = (PlayerMarkerArrow) obj.GetComponent ("PlayerMarkerArrow");
		obj = GameObject.Find ("enemyArrowMarker");
		enemyMarker = (EnemyMarkerArrow) obj.GetComponent ("EnemyMarkerArrow");
		obj = GameObject.Find ("abilityArrowMarker");
		abilityMarker = (AbilityMarkerArrow)obj.GetComponent ("AbilityMarkerArrow");

		actionMarker.selectionActive = false;
		actionMarker.hideActionPopup (); //debug
	}

	void Update () {
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			if (actionMarker.selectionActive) {
				actionMarker.moveUp();
			} else if (playerMarker.selectionActive) {
				playerMarker.moveUp();
			} else if (enemyMarker.selectionActive) {
				enemyMarker.moveUp();
			} else if (abilityMarker.selectionActive) {
			}
		} else if (Input.GetKeyUp (KeyCode.DownArrow)) {
			if (actionMarker.selectionActive) {
				actionMarker.moveDown();
			} else if (playerMarker.selectionActive) {
				playerMarker.moveDown();
			} else if (enemyMarker.selectionActive) {
				enemyMarker.moveDown();
			}
		} else if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			//noop
		} else if (Input.GetKeyUp (KeyCode.RightArrow)) {
			//noop
		} else if (Input.GetKeyUp(KeyCode.Return)) {
			if (actionMarker.selectionActive) {
				actionMarker.select();
				if (actionMarker.currentItemPosition == 0) {
					enemyMarker.selectionActive = true;
					actionMarker.hideActionPopup();
				}
			} else if (playerMarker.selectionActive) {
				playerMarker.select();
			} else if (enemyMarker.selectionActive) {
				enemyMarker.select();
			}
		}
	}
}
