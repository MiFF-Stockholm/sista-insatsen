using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Globals
{
    public static void Reset()
    {
        InDialog = false;
        FladderKilled = false;
        FladderArested = false;
        RandKilled = false;
        PriestOut = false;
        FjallKilled = false;
        FoxerKilled = false;

        initialPosition = Vector3.zero;
        translateOnStartup = false;
        PreBattleLevelName = "";
        difficultyFactor = 20;
        boss = null;

        gameOverDelegate = new GameOverDelegate(defaultGameOver);
        battleCompletedDelegate = new BattleCompletedDelegate(defaultBattleComplete);
        battleType = BattleType.FOREST_BATTLE;

        boss1Defeated = false;
	    boss2Defeated = false;
	    boss3Defeated = false;
	    boss4Defeated = false;

        Dialog.PlayedDialogs.Clear();

		foreach(PlayerStats p in PlayerContainer.players) {
			p.healthPoints = 100;
			p.battleProgress = 50;
			p.damage = 10;
		}
    }

    public static float RandomEncounterGlobalFactor = 0.5f;
    public static bool InDialog = false;

    public static bool FladderKilled = false;
    public static bool FladderArested = false;
    public static bool RandKilled = false;
    public static bool PriestOut = false;
    public static bool FjallKilled = false;
    public static bool FoxerKilled = false;

	//To be set on scene transitions. The script called "Startup" is responsible for changing the player pos on load.
	public static Vector3 initialPosition;
	public static bool translateOnStartup = false;
	public static string PreBattleLevelName = "";
	public static int volume = 50;

	// Max health, damage = factor
	// Min health, damage = factor/2
	public static int difficultyFactor = 20;

	public static GameOverDelegate gameOverDelegate = new GameOverDelegate(defaultGameOver);
	public static BattleCompletedDelegate battleCompletedDelegate = new BattleCompletedDelegate(defaultBattleComplete);

	public delegate void GameOverDelegate();
	public delegate void BattleCompletedDelegate();
	public static Boss boss;

	public enum BattleType {
		FOREST_BATTLE, FOREST_BOSS, FOREST_BATTLE_WEAK
	}

	public static BattleType battleType = BattleType.FOREST_BATTLE;

	public static bool boss1Defeated;
	public static bool boss2Defeated;
	public static bool boss3Defeated;
	public static bool boss4Defeated;

	public static void defaultGameOver() {
		Application.LoadLevel("Credits");
	}

	public static void defaultBattleComplete() {
		// Random bonus
		foreach (PlayerStats p in PlayerContainer.players) {
			p.damage += (int)Random.Range(difficultyFactor*0.2f, difficultyFactor*0.4f);
            p.healthPoints += (int)Random.Range(difficultyFactor * 0.2f, difficultyFactor * 0.4f);
		}
		AudioClip audio = Resources.Load<AudioClip>("sounds/Music/Combat09_Win Combat");
		Play (audio);				

		Application.LoadLevel (PreBattleLevelName);
	}

    public static void fladderBeaten()
    {
		bossDefeated ();
		boss1Defeated = true;
        Application.LoadLevel(PreBattleLevelName);
    }

    public static void randBeaten()
    {
		bossDefeated ();
		boss2Defeated = true;
        Application.LoadLevel(PreBattleLevelName);
    }
    
    public static void fjallBeaten()
    {
		bossDefeated ();
		boss3Defeated = true;
        Application.LoadLevel(PreBattleLevelName);
    }

    public static void foxerBeaten()
    {
		bossDefeated ();        
        boss4Defeated = true;
        Application.LoadLevel(PreBattleLevelName);
    }

	public static void lvlup() 
	{
		foreach (PlayerStats p in PlayerContainer.players) {
			p.damage += Random.Range(difficultyFactor/4, difficultyFactor/2);
			p.healthPoints += Random.Range(difficultyFactor/4, difficultyFactor/2);
		}
	}

	public static void bossDefeated() {
		Play(Resources.Load<AudioClip> ("sounds/music/Combat10_Win Combat Boss"));
		Globals.boss = null;
		Globals.battleCompletedDelegate = Globals.defaultBattleComplete;
		Globals.difficultyFactor += 20;
		lvlup ();
	}
	
	public static AudioSource Play(AudioClip clip)
	{
        if (clip == null)
            return null;

		//Create an empty game object
		GameObject go = new GameObject("Audio: " + clip.name);
		
		//Create the source
		AudioSource source = go.AddComponent<AudioSource>();
		source.clip = clip;
		source.volume = volume;
		source.Play();
		GameObject.Destroy(go, clip.length);
		return source;
	}
}