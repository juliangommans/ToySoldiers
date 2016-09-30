using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class DummyEnemy : BaseCharacter {

	private const string name = "Dummy Enemy"; // input can effect this later

	public DummyEnemy ()
		:base (){

		this.Stats.BaseHealth = 100;
		this.Stats.BaseMaxHealth = 100;
		this.Stats.BaseAttack = 10;
		this.Stats.BaseDefense = 10;
		this.Stats.BasePower = 10;
		this.Stats.BaseResistance = 10;
		this.Stats.BaseSpeed = 10;

		this.CharacterName = name;
//		this.Skills.Add (new TouchOfFlame (this.gameObject));
	}
}

