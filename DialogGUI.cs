using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogGUI : MonoBehaviour {

    public Texture BoxTextrue;
    public Texture BoxTextrueLeft;
    public Texture BoxTextrueRight;
    public Texture[] Faces;
    public string DialogName;
    public Dialog CurrentDialog;
    public GUISkin Skin;

    KeyCode[] indexedKeyCodes = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };
    Rect box;
    Rect boxR;
    Rect boxL;
    List<Rect> buttons = new List<Rect>(3);
    Rect label;
    Rect face;

    void Start()
    {
        box = new Rect(15, Screen.height - 200, Screen.width - 30, 200);
        boxR = new Rect(Screen.width - 15, Screen.height - 200, 15, 200);
        boxL = new Rect(0, Screen.height - 200, 15, 200);
        buttons.Add(new Rect(140, Screen.height - 60, 170, 30));
        buttons.Add(new Rect(140 + 180, Screen.height - 60, 170, 30));
        buttons.Add(new Rect(140 + 180 * 2, Screen.height - 60, 170, 30));
        label = new Rect(140, Screen.height - 170, Screen.width - 170, 95);
        face = new Rect(30, Screen.height - 170, 100, 100);

        if (Globals.boss1Defeated && !Globals.FladderKilled && !Globals.FladderArested)
        {
            StartDialog("fladder_efter");
        }

        if (Globals.boss2Defeated && !Globals.RandKilled)
        {
            StartDialog("rand_efter");
            Globals.RandKilled = true;
        }
        
        if (Globals.boss3Defeated && !Globals.FjallKilled)
        {
            StartDialog("fjall_efter");
            Globals.FjallKilled = true;
        }

        if (Globals.boss4Defeated && !Globals.FoxerKilled)
        {
            StartDialog("foxer_efter");
            Globals.FoxerKilled = true;
        }
    }

    public void StartDialog(string name)
    {
        if (Dialog.PlayedDialogs.Contains(name))
            return;

        DialogName = name;
        CurrentDialog = Dialog.GetDialog(DialogName);
        Globals.InDialog = true;
        Dialog.PlayedDialogs.Add(name);
    }

    void Update()
    {
        if (!Globals.InDialog)
            return;

        if (CurrentDialog == null)
        {
            Globals.InDialog = false;
            return;
        }

        if (CurrentDialog.Options.Count > 1)
        {
            for (int i = 0; i < CurrentDialog.Options.Count; i++)
            {
                if(Input.GetKeyUp(indexedKeyCodes[i]))
                {
                    DialogPath option = CurrentDialog.Options[i];
                    if (option.OnSelect != null)
                        SendMessage(option.OnSelect, SendMessageOptions.DontRequireReceiver);

                    CurrentDialog = option.NextDialog;
                    break;
                }
            }
            return;
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Alpha1))
        {
            DialogPath option = CurrentDialog.Options[0];
            if (option.OnSelect != null)
                SendMessage(option.OnSelect, SendMessageOptions.DontRequireReceiver);

            CurrentDialog = option.NextDialog;
        }
    }

    void OnGUI()
    {
        if (!Globals.InDialog)
            return;

        if (CurrentDialog == null)
        {
            Globals.InDialog = false;
            return;
        }

        GUI.skin = Skin;
        GUI.DrawTexture(box, BoxTextrue, ScaleMode.StretchToFill);
        GUI.DrawTexture(boxR, BoxTextrueRight, ScaleMode.StretchToFill);
        GUI.DrawTexture(boxL, BoxTextrueLeft, ScaleMode.StretchToFill);
        if(CurrentDialog.FaceIndex > 0)
            GUI.DrawTexture(face, Faces[CurrentDialog.FaceIndex], ScaleMode.StretchToFill);

        GUI.Label(label, CurrentDialog.Text);
        for(int i = 0; i < CurrentDialog.Options.Count; i++)
        {
            DialogPath option = CurrentDialog.Options[i];
            if (GUI.Button(buttons[i], option.Text))
            {
                if (option.OnSelect != null)
                    SendMessage(option.OnSelect, SendMessageOptions.DontRequireReceiver);

                CurrentDialog = option.NextDialog;
                break;
            }
        }
    }

    //Messages from dialogs
    void CombatFladder()
    {
		Globals.boss = Boss.bosses["Fladder"];
        Globals.difficultyFactor = 60; // 100 (max) - 10*4 (boss number)
        Globals.battleCompletedDelegate = new Globals.BattleCompletedDelegate(Globals.fladderBeaten);
        Globals.PreBattleLevelName = Application.loadedLevelName;
        Globals.initialPosition = new Vector3(3, 52, 0);
        Globals.translateOnStartup = true;
        Application.LoadLevel("scen-ooe_nik");
    }

    void CombatRand()
    {
		Globals.boss = Boss.bosses["Rand"];
        Globals.difficultyFactor = 70; // 100 (max) - 10*3 (boss number)
        Globals.battleCompletedDelegate = new Globals.BattleCompletedDelegate(Globals.randBeaten);
        Globals.PreBattleLevelName = Application.loadedLevelName;
        Globals.initialPosition = new Vector3(-36, 0, 0);
        Globals.translateOnStartup = true;
        Application.LoadLevel("scen-ooe_nik");
    }

    void CombatFjall()
    {
		Globals.boss = Boss.bosses["CyberDragon"];
		Globals.difficultyFactor = 80; // 100 (max) - 10*2 (boss number)
        Globals.battleCompletedDelegate = new Globals.BattleCompletedDelegate(Globals.fjallBeaten);
        Globals.PreBattleLevelName = Application.loadedLevelName;
        Globals.initialPosition = new Vector3(2, 82, 0);
        Globals.translateOnStartup = true;
        Application.LoadLevel("scen-ooe_nik");
    }

    void CombatFoxer()
    {
		Globals.boss = Boss.bosses["Raven"];
		Globals.difficultyFactor = 90; // 100 (max) - 10*1 (boss number)
        Globals.battleCompletedDelegate = new Globals.BattleCompletedDelegate(Globals.foxerBeaten);
        Globals.PreBattleLevelName = Application.loadedLevelName;
        Globals.initialPosition = new Vector3(-7, 40, 0);
        Globals.translateOnStartup = true;
        Application.LoadLevel("scen-ooe_nik");
    }

    void FladderKilled()
    {
        Globals.FladderKilled = true;
    }

    void FladderArested()
    {
        Globals.FladderArested = true;
    }

    void DropMap()
    {
        //TODO: -när Rand dör droppas en karta över templet med numrerade pelare typ-
    }

    void PriestOut()
    {
        Globals.PriestOut = true;
        GameObject p = GameObject.Find("priest_standing");
        if (p != null)
            p.SetActive(false);

        p = GameObject.Find ("priest_down");
        if (p != null)
            p.renderer.enabled = true;

    }

    void RunCredits()
    {
        Application.LoadLevel("Credits");
    }
}
