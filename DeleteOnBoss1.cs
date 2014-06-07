using UnityEngine;
using System.Collections;

public class DeleteOnBoss1 : MonoBehaviour {
	// Use this for initialization
	void Start () {
        gameObject.SetActive(!Globals.boss1Defeated);
	}
}
