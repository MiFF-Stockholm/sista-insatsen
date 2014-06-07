using UnityEngine;
using System.Collections;

public class PlayerMarkerArrow : MonoBehaviour {
	
	private Vector3 startPos;
	public int currentItemPosition = 0;
	private float increment = 1.5f;
	public bool selectionActive = false;
	
	void Start () {
		startPos = transform.position;
	}

	void Update () {
		
	}

	public void moveUp() {
		Vector3 newPos = transform.position;
		if (newPos.y == startPos.y) {
			newPos = new Vector3 (newPos.x, startPos.y - (3 * increment), newPos.z);
			currentItemPosition = 3;
		} else {
			newPos = new Vector3 (newPos.x, newPos.y + increment, newPos.z);
			currentItemPosition--;
		}
		transform.position = newPos;
	}
	
	public void moveDown() {
		Vector3 newPos = transform.position;
		if (newPos.y == startPos.y - (3 * increment)) {
			newPos = new Vector3 (newPos.x, startPos.y, newPos.z);
			currentItemPosition = 0;
		} else {
			newPos = new Vector3 (newPos.x, newPos.y - increment, newPos.z);
			currentItemPosition++;
		}
		transform.position = newPos;
	}
	
	public void select() {

	}

}
