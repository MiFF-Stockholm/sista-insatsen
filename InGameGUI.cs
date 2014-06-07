using UnityEngine;
using System.Collections;

public class InGameGUI : MonoBehaviour {

    bool visible = false;
    Rect quit;

	// Use this for initialization
	void Start () {
        quit = new Rect(Screen.width * 0.5f - 100, Screen.height * 0.5f - 20, 200, 40);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            visible = !visible;
        }
	}

    void OnGUI()
    {
        if (!visible)
            return;

        if (GUI.Button(quit, "Quit"))
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
