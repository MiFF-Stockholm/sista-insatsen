using UnityEngine;
using System.Collections;

public class Priest : MonoBehaviour {
    public bool Standing = true;
	void Start () {
        if (Standing)
        {
            gameObject.SetActive(!Globals.PriestOut);
        }
        else
        {
            renderer.enabled = Globals.PriestOut;
        }
	}
}
