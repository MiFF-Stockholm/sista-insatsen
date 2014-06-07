using UnityEngine;
using System.Collections;

public class PlayerLifeGui : MonoBehaviour {

	void Update()
	{
		foreach(PlayerStats players in PlayerContainer.players)
		{
			GameObject hpObj = GameObject.Find("hp" + (players.index + 1) + "Text");
			hpObj.GetComponent<TextMesh>().text = players.healthPoints.ToString();

		}

		highlightPlayerTexts ();
	}

	private void highlightPlayerTexts ()
	{
		BattleLogic logic = (BattleLogic) GameObject.Find ("Main Camera").GetComponent("BattleLogic");

		for(int i = 0; i < 4; i++) {
			TextMesh text = GameObject.Find ("pc" + (i+ 1) + "Text").GetComponent<TextMesh> ();

			if(PlayerContainer.players[i].healthPoints <= 0) {
				text.color = Color.red;
			} else if(logic.activePlayer != null && logic.activePlayer.index == i) {
				text.color = Color.blue;
			} else {
				text.color = Color.white;
			}
		}
	}
}