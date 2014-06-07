using UnityEngine;
using System.Collections;

public class BattleLogic : MonoBehaviour {

	public ActionMarkerArrow actionMarker;
	public EnemyMarkerArrow enemyMarker;
	public PlayerStats activePlayer;
	public bool isAbilityAttack = false;

	private GameObject[] playerObjects;
	private GameObject[] enemyObjects;
	private const float updateInterval = 0.1f;
	private float passedTime;
	private const float progressBarMaxLength = 0.2524079f;

	void Start () {
		playerObjects = GameObject.FindGameObjectsWithTag("Player");
		enemyObjects = GameObject.FindGameObjectsWithTag ("Enemy");
		actionMarker = (ActionMarkerArrow) GameObject.Find ("popupArrowMarker").GetComponent("ActionMarkerArrow");
		enemyMarker = (EnemyMarkerArrow) GameObject.Find ("enemyArrowMarker").GetComponent("EnemyMarkerArrow");

		if (Globals.boss != null) {
			// Disable all mobs
			foreach(GameObject obj in enemyObjects) {
				obj.transform.renderer.enabled = false;
				obj.GetComponent<EnemyStats>().isDead = true;

				GameObject.Find(obj.name + "GUI").GetComponentInChildren<TextMesh>().text = "";
			}

			// Init boss!!! YEAH!
			GameObject bossObj = enemyObjects[0];
			bossObj.transform.renderer.enabled = true;
			EnemyStats bossStats = bossObj.GetComponent<EnemyStats>();
			bossStats.isDead = false;

			bossObj.GetComponent<SpriteRenderer>().sprite = Globals.boss.sprite;
			
			// Set enemy name:
			GameObject.Find("enemy1GUI").GetComponentInChildren<TextMesh>().text = Globals.boss.name;

			//TODO adjust difficulty!
			bossStats.health = Random.Range((int)(Globals.difficultyFactor * 0.5), Globals.difficultyFactor);
			bossStats.damage = Random.Range((int)(Globals.difficultyFactor * 0.5), Globals.difficultyFactor);

			// TODO create new Ai for Bosses
			bossObj.AddComponent(Globals.boss.ai);
			
			//TODO: Center boss better
			bossObj.transform.position = new Vector3(bossObj.transform.position.x, bossObj.transform.position.y - 4, bossObj.transform.position.y);
			//bossObj.GetComponent<EnemyStats>().attackStart = bossObj.GetComponent<EnemyStats>().transform.position;
			//bossObj.transform.position = new Vector3(bossObj.transform.position.x, bossObj.transform.position.y - 4, bossObj.transform.position.y);

			// Set boss music
			AudioSource audioSource = GameObject.Find ("Main Camera").GetComponent<AudioSource>();
			audioSource.clip = Resources.Load<AudioClip>("sounds/Music/Battle4_Boss Battle");
		} else {
			createEnemies ();
		}
		foreach (PlayerStats player in PlayerContainer.players) {
			player.battleProgress = Random.Range(0, (int)((100 - Globals.difficultyFactor)*0.7));
			//GameObject.Find("player" + (player.index + 1)).AddComponent(player);
		}
	}

	private void createEnemies () {
		//Add GFX
		Sprite[] objs = Resources.LoadAll<Sprite>("enemies");
		if (objs.Length > 0) {
			Debug.Log ("enemies: " + objs.Length);
			foreach (GameObject gObj in enemyObjects) {
				Sprite sprite = objs [Random.Range (0, objs.Length)];
				gObj.GetComponent<SpriteRenderer>().sprite = sprite;

				// Set enemy name:
				TextMesh textMesh = GameObject.Find(gObj.name + "GUI").GetComponentInChildren<TextMesh>();
				textMesh.text = char.ToUpper(sprite.name[0]) + sprite.name.Substring(1);

			}
		} else {
			Debug.Log("No enemies textures found!!");
		}

		foreach(GameObject eObj in enemyObjects) {
			EnemyStats enemy = eObj.GetComponent<EnemyStats>();
			enemy.health = Random.Range((int)(Globals.difficultyFactor * 0.5), Globals.difficultyFactor);
			enemy.damage = Random.Range((int)(Globals.difficultyFactor * 0.5), Globals.difficultyFactor);
		}

		//SFX

		foreach (GameObject en in enemyObjects) {
			en.AddComponent(Ai.getRandomAi());
            Debug.Log(en);
		}
	}

	bool noFlagsActive () {
		return !(actionMarker.selectionActive || enemyMarker.selectionActive);
	}

	void increaseProgress (GameObject obj) {
		const float startX = -0.7164783f;
		PlayerStats player = null;
		GameObject bar = null;
		if (obj.name.Equals("player1")) {
			player = PlayerContainer.players[0];
			bar = GameObject.Find("innerBar1");
		}
		if (obj.name.Equals("player2")) {
			player = PlayerContainer.players[1];
			bar = GameObject.Find("innerBar2");
		}
		if (obj.name.Equals("player3")) {
			player = PlayerContainer.players[2];
			bar = GameObject.Find("innerBar3");
		}
		if (obj.name.Equals("player4")) {
			player = PlayerContainer.players[3];
			bar = GameObject.Find("innerBar4");
		}
		if (player.battleProgress >= 100) {
			if (noFlagsActive ()) {
				actionMarker.showActionPopup ();
				player.battleProgress = 0;
				activePlayer = player;
			}
		} else if(player.healthPoints > 0){
			player.battleProgress += 1;
		} 
		bar.transform.localScale = new Vector3(progressBarMaxLength * player.battleProgress / 100f, 
		                                       bar.transform.localScale.y, 
		                                       bar.transform.localScale.z);
		bar.transform.position = new Vector3(startX + 4.6f, 
		                                     bar.transform.position.y, 
		                                     bar.transform.position.z);

	}

	void Update () {
		int livingEnemiesCount = 0;
		foreach (GameObject enemy in enemyMarker.enemies) {
			EnemyStats enemyStats = (EnemyStats)enemy.GetComponent("EnemyStats");
			if (!enemyStats.isDead) { 
				livingEnemiesCount++;
			}
		}
		if (livingEnemiesCount == 0) {
			//Debug.Log ("Changing level to " + Globals.PreBattleLevelName);
			//Application.LoadLevel(Globals.PreBattleLevelName);
			Globals.battleCompletedDelegate();
		}

		int livingPlayers = 0;
		foreach (PlayerStats player in PlayerContainer.players) {
			if(player.healthPoints > 0) {
				livingPlayers++;
			}
		}
		if (livingPlayers == 0) {
			Globals.gameOverDelegate();
		}


		passedTime += Time.deltaTime;
		if (passedTime > updateInterval) {
			foreach (GameObject obj in playerObjects) {
				increaseProgress(obj);
			}

			foreach(GameObject obj in enemyObjects) {
				increaseEnemyProgress(obj);
			}

			passedTime = 0;
		}
	}

	private void increaseEnemyProgress(GameObject enemy) {
		EnemyStats stats = (EnemyStats)enemy.GetComponent("EnemyStats");

		if (stats.battleProgress >= 100 && !stats.isDead) {
			Ai ai = (Ai)enemy.GetComponent ("Ai");
			ai.attack (stats);
			stats.battleProgress = 0 - Random.Range(0, 50);
		} else {
			stats.battleProgress += 1;
		}

	}
}
