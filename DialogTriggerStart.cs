using UnityEngine;
using System.Collections;

public class DialogTriggerStart : MonoBehaviour {

    public float Delay = 0.5f;
    public string DialogName = "intro";
    public DialogGUI Gui;

    float startDelay = 1.0f;

    void Start()
    {
        startDelay = Delay;
        if (Gui != null)
            return;

        Gui = GameObject.Find("DialogGUI").GetComponent<DialogGUI>();
    }

	void Update () {
        Delay -= Time.deltaTime;
        if (Delay <= 0)
        {
            if (DialogName == "intro_akt2")
            {
                if (Globals.FladderKilled)
                    DialogName = "slum_fladder_död";
                else if (Globals.FladderArested)
                    DialogName = "slum_fladder_lever";
                else
                    return; //Fel håll

            }

            Gui.StartDialog(DialogName);
            Delay = startDelay;
            enabled = false;
        }
	}
}
