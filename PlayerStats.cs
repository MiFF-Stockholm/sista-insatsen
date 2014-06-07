using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public int healthPoints = 100;
	public int battleProgress = 50;
	public int index;
	public int damage = 10;

	// Use this for initialization
	public PlayerStats (int index) {
		this.index = index;
	}
}
