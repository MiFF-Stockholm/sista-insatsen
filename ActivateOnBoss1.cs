using UnityEngine;
using System.Collections;

public class ActivateOnBoss1 : MonoBehaviour {

    public GameObject Target;

	// Use this for initialization
	void Start () {
        Target.SetActive(Globals.boss1Defeated);
	}
}
