using UnityEngine;
using System.Collections;

public class DialogNPCTrigger : MonoBehaviour {

    public DialogGUI Gui;
    public string DialogName = "intro";

    void Start()
    {
        if (Gui != null)
            return;

        Gui = GameObject.Find("DialogGUI").GetComponent<DialogGUI>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
		Tags t = other.GetComponent<Tags>();
	    if (t == null || !t.PlayerCharacter)
	        return;

		Gui.StartDialog(DialogName);
		collider2D.enabled = false;
        enabled = false;
    }
}
