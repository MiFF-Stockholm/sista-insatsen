using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Boss {
	public static Dictionary<string, Boss> bosses = new Dictionary<string, Boss>();

	static Boss() {
		bosses.Add("Fladder", new Boss("Fladder", Resources.Load<Sprite>("bosses/Fladder"), "FladderAi"));
		bosses.Add("Rand", new Boss("Mäster Rand", Resources.LoadAll<Sprite>("bosses/Rand")[1], "FladderAi"));
		bosses.Add("CyberDragon", new Boss("Von Fjäll", Resources.LoadAll<Sprite>("bosses/cyberDragon")[0], "FladderAi"));
		bosses.Add("Raven", new Boss("Räven", Resources.LoadAll<Sprite>("bosses/raven")[1], "FladderAi"));
	}

	public string name;
	public Sprite sprite;
	public String ai;

	public Boss(string name, Sprite sprite, String ai) {
		this.name = name;
		this.sprite = sprite;
		this.ai = ai;
	}
}

