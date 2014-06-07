using UnityEngine;
using System.Collections;

public static class PlayerContainer : object {

	public static PlayerStats[] players;

	static PlayerContainer () {
		players = new PlayerStats[4];
		players [0] = new PlayerStats(0);
		players [1] = new PlayerStats(1);
		players [2] = new PlayerStats(2);
		players [3] = new PlayerStats(3);
	}

}
