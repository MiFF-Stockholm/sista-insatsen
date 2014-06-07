using UnityEngine;
using System.Collections;

public class EnemyGui : MonoBehaviour {
	
	void Update()
	{
		for (int i = 0; i < 4; i++) {
			TextMesh text = GameObject.Find("foe" + (i + 1) + "Text").GetComponent<TextMesh>();
			EnemyStats enemy = GameObject.Find("enemy" + (i + 1)).GetComponent<EnemyStats>();
			if(enemy.isDead) {
				text.color = Color.red;
			} else if(enemy.attacking) {
				text.color = Color.cyan;
			} else {
				text.color = Color.white;
			}
		}
	}
}