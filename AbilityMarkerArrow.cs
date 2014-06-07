using UnityEngine;
using System.Collections;

public class AbilityMarkerArrow : MonoBehaviour {

	public bool selectionActive = false;
	public int currentItemPosition = 0;

	private Vector3 startPos;
	private const float moveDistance = 0.5f;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void hideActionPopup () {
		GameObject obj = GameObject.Find ("abilityPopup");
		Component[] comps = obj.transform.GetComponentsInChildren<Renderer> ();
		foreach (Component c in comps) {
			c.renderer.enabled = false;
		}
		selectionActive = false;
	}
	
	public void showActionPopup () {
		GameObject obj = GameObject.Find ("abilityPopup");
		Component[] comps = obj.transform.GetComponentsInChildren<Renderer> ();
		foreach (Component c in comps) {
			c.renderer.enabled = true;
		}
		selectionActive = true;
	}

	public void moveUp() {
		Vector3 newPos = transform.position;
		if (newPos.y == startPos.y) {
			newPos = new Vector3(newPos.x, startPos.y - 1.5f, newPos.z);
			currentItemPosition = 3;
		} else {
			newPos = new Vector3(newPos.x, newPos.y + 0.5f, newPos.z);
			currentItemPosition--;
		}
		transform.position = newPos;
	}
	
	public void moveDown() {
		Vector3 newPos = transform.position;
		if (newPos.y == startPos.y - 1.5f) {
			newPos = new Vector3(newPos.x, startPos.y, newPos.z);
			currentItemPosition = 0;
		} else {
			newPos = new Vector3(newPos.x, newPos.y - 0.5f, newPos.z);
			currentItemPosition++;
		}
		transform.position = newPos;
	}

	public void select() {
		GameObject marker = GameObject.Find("enemyArrowMarker");
		EnemyMarkerArrow enemyMarker = (EnemyMarkerArrow) marker.GetComponent("EnemyMarkerArrow");
		enemyMarker.selectionActive = true;
		selectionActive = false;
		BattleLogic logic = (BattleLogic) GameObject.Find ("Main Camera").GetComponent("BattleLogic");
		logic.isAbilityAttack = true;
		enemyMarker.refreshMarker();
		marker.renderer.enabled = true;
	}

	public AbilityStats getSelectedAbility() {
		return new AbilityStats (15, "");
	}
}
