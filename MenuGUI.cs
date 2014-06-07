using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {

    public GUISkin Skin;
    public Texture[] Textures;
    public Rect[] TexturePositions;

    bool showOptions;
    string textFieldValue = "";
    string textFieldValue2 = "";

    Rect header;
    Rect startGame;
    Rect options;
    Rect optionLabel;
    Rect option;
    Rect optionLabel2;
    Rect option2;
    Rect credits;
    Rect quit;    

	void Start () {
        Globals.Reset();

	    header = new Rect(10, 10, Screen.width - 20, 60);
        startGame = new Rect(10, 80, 200, 40);
        options = new Rect(10, 130, 200, 40);
        optionLabel = new Rect(220, 125, Screen.width - 250, 40);
        option = new Rect(220, 145, 200, 25);
        optionLabel2 = new Rect(220, 165, Screen.width - 250, 40);
        option2 = new Rect(220, 185, 200, 25);
        credits = new Rect(10, 180, 200, 40);
        quit = new Rect(10, 230, 200, 40);

        textFieldValue = Globals.RandomEncounterGlobalFactor.ToString();
        textFieldValue2 = Globals.volume.ToString();
	}

    void OnGUI()
    {
        GUI.skin = Skin;
        for (int i = 0; i < Textures.Length && i < TexturePositions.Length; i++)
            GUI.DrawTexture(TexturePositions[i], Textures[i], ScaleMode.StretchToFill);

        GUI.Label(header, "<size=50><color=red>Sista Insatsen</color></size>");
        if (GUI.Button(startGame, "Start Game"))
        {
            float value = 0.5f;
            if (float.TryParse(textFieldValue, out value))
                Globals.RandomEncounterGlobalFactor = value;

            int value2 = 50;
            if (int.TryParse(textFieldValue2, out value2))
                Globals.volume = value2;

            Application.LoadLevel("hub");
        }
        if (GUI.Button(options, "Toggle Options"))
        {
            showOptions = !showOptions;
        }
        if (GUI.Button(credits, "Credits"))
        {
            Application.LoadLevel("Credits");
        }
        if (GUI.Button(quit, "Quit"))
        {
            Application.Quit();
        }

        if (showOptions)
        {
            GUI.Label(optionLabel, "<size=10>Random Encounter Frequency</size>");
            textFieldValue = GUI.TextField(option, textFieldValue);

            GUI.Label(optionLabel2, "<size=10>Sound Volume</size>");
            textFieldValue2 = GUI.TextField(option2, textFieldValue2);
        }
    }
}
