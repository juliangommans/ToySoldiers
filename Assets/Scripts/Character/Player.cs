using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class Player : BaseCharacter {

	private const string name = "Player"; // input can effect this later

//	public Player ()
//		:base (){
//
//	}
	public void BuildPlayer() {
		Debug.Log (this.gameObject);
		this.CharacterName = name;
//		this.Skills.Add (new TouchOfFlame (this.gameObject));
	}
		
}
