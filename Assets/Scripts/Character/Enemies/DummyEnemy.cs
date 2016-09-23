using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class DummyEnemy : BaseCharacter {

	private const string name = "Dummy Enemy"; // input can effect this later

	public DummyEnemy ()
		:base (){

		this.BaseHealth = 100;
		this.BaseMaxHealth = 100;
		this.BaseAttack = 10;
		this.BaseDefense = 10;
		this.BaseEnergy = 10;
		this.BaseResistance = 10;
		this.BaseSpeed = 10;

		this.CharacterName = name;
//		this.Skills.Add (new TouchOfFlame (this.gameObject));
	}
}

