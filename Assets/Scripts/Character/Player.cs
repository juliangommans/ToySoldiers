using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class Player : BaseCharacter {

	private const string name = "Hoolio"; // input can effect this later

	public Player ()
		:base (){
		this.CharacterName = name;
		this.AddSkill (new IceBolt());
	}

		
}
