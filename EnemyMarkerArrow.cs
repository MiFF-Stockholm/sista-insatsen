using UnityEngine;
using System.Collections;

public class EnemyMarkerArrow : MonoBehaviour {

	public GameObject target;

	private Vector3 startPos;
	public GameObject[] enemies;
	private int index;
	private int maxIndex;
	public bool selectionActive = false;

	void Start () {
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		maxIndex = enemies.Length;
		float x = enemies[index].transform.position.x - 1.1f;
		float y = enemies[index].transform.position.y - 0.10f;
		transform.position = new Vector3 (x, y);
		index = 0;
	}

	int getIndexOfFirstLivingEnemy () {
		int ret = 0;
		for (int i = 0; i < maxIndex; i++) {
			if (!((EnemyStats)enemies[i].GetComponent("EnemyStats")).isDead) {
				ret = i;
				break;
			}
		}
		return ret;
	}
	
	private void moveArrowToNextEnemy () {
		GameObject enemy = enemies[index];
		transform.position = new Vector3 (enemy.transform.position.x - 1.1f, 
		                                  enemy.transform.position.y - 0.10f, 
		                                  enemy.transform.position.z);
	}

	public void moveUp() {
		if (index + 1 >= maxIndex) {
			index = 0;
		} else {
			index++;
		}
		if (!((EnemyStats)enemies[index].GetComponent("EnemyStats")).isDead) {
			moveArrowToNextEnemy ();
		} else {
			moveUp();
		}
	}
	
	public void moveDown() {
		if (index - 1 < 0) {
			index = maxIndex-1;
		} else {
			index--;
		}
		if (!((EnemyStats)enemies[index].GetComponent("EnemyStats")).isDead) {
			moveArrowToNextEnemy ();
		} else {
			moveDown();
		}
	}

	public void refreshMarker() {
		index = getIndexOfFirstLivingEnemy ();
		moveArrowToNextEnemy ();
	}
	
	public void select() {
		target = enemies[index];
		selectionActive = false;
		renderer.enabled = false;
		//trigger chosen action on target
		
		//show action marker
		BattleLogic logic = (BattleLogic) GameObject.Find ("Main Camera").GetComponent("BattleLogic");
		EnemyStats enemy = (EnemyStats)enemies [index].GetComponent("EnemyStats");

		if (logic.isAbilityAttack) {
			AbilityMarkerArrow ability = GameObject.Find ("abilityArrowMarker").GetComponent<AbilityMarkerArrow>();
			AbilityStats stats = ability.getSelectedAbility();
			enemy.health -= stats.damage;
			logic.isAbilityAttack = false;
		} else {
			enemy.health -= Random.Range (logic.activePlayer.damage/2, logic.activePlayer.damage);
			Globals.Play(Resources.Load<AudioClip>("sounds/music/Combat03_Attack hit2"));
		}

		Debug.Log ("Player " + logic.activePlayer.index + " attacked enemy " + index);
	}
	
	void Update () {

	}
}
