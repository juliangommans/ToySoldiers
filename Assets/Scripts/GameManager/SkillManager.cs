using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour {

	private BaseSkill skill;
	private GameObject character;
	private GameObject target;
	private bool selected;
	private bool allowedTarget;

	public GameObject Character { get; set; }
	public BaseSkill Skill {get; set; }
	public bool Selected { get; set; }
	public GameObject Target { get; set; }

	public void SkillSetup (){
		// will need some logic eventually to determine skills modularly

//		character = GameObject.Find("GameScripts").GetComponent<GameManager> ().Player;
		skill = character.GetComponent<BaseCharacter> ().Skills [0]; // this needs to come from the players stat pool	
		selected = false;
	}

	public void SelectSkill(){
		SkillSetup ();
		target = character.GetComponent<BaseCharacter> ().CurrentTarget;
		if (target != null) {
			SelectTarget ();
			if (skill.Target != null) {
				character.GetComponent<BaseCharacter> ().MadeSelection = true;
				selected = true;
				character.GetComponent<BaseCharacter> ().SelectedSkill = skill;
				Debug.Log (character.GetComponent<BaseCharacter> ().name + ": Has selected - " + skill.Information.Name);
			}
		}
	}

	public void SelectTarget(){
		allowedTarget = skill.AllowedTarget (target, character);
		if (allowedTarget) {
			skill.Target = target;
			Debug.Log ("tar: " + target + " from: " + skill.Owner.name);
		} else {
			Debug.Log ("you have selected an invalid target");
		}
	}

	public void UseSkill(){
		skill.UseSkill ();
	}
}
