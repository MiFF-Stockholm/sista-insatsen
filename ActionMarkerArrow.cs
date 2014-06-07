using UnityEngine;
using System.Collections;

public class ActionMarkerArrow : MonoBehaviour {

	private Vector3 startPos;
	public int currentItemPosition = 0;
	public bool selectionActive = false;

	void Start () {
		startPos = transform.position;
	}

	void Update () {
		
	}

	public void hideActionPopup () {
		GameObject obj = GameObject.Find ("charPopup");
		Component[] comps = obj.transform.GetComponentsInChildren<Renderer> ();
		foreach (Component c in comps) {
			c.renderer.enabled = false;
		}
		selectionActive = false;
	}

	public void showActionPopup () {
		GameObject obj = GameObject.Find ("charPopup");
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
		if (currentItemPosition == 0) {
			GameObject marker = GameObject.Find("enemyArrowMarker");
			EnemyMarkerArrow enemyMarker =  marker.GetComponent<EnemyMarkerArrow>();
			enemyMarker.selectionActive = true;
			selectionActive = false;
			enemyMarker.refreshMarker();
			marker.renderer.enabled = true;
		} else if (currentItemPosition == 1) {
			GameObject marker = GameObject.Find("abilityArrowMarker");
			AbilityMarkerArrow abilityMarker = marker.GetComponent<AbilityMarkerArrow>();
			abilityMarker.selectionActive = true;
			selectionActive = false;
			abilityMarker.showActionPopup();
		}
	}

}
