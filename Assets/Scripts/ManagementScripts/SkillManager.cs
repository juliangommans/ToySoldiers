using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour {

	private BaseSkill skill;
	private GameObject character;
	private GameObject target;
	private bool selected;
	private bool allowedTarget;
	private UiManager uiManager;

	public GameObject Character { get; set; }
	public BaseSkill Skill {get; set; }
	public bool Selected { get; set; }
	public GameObject Target { get; set; }

	void Awake(){
		GameObject gm = GameObject.Find ("GameManager");
		uiManager = gm.GetComponent<UiManager> ();
	}

	public void SkillSetup (GameObject c, BaseSkill s){
		// should be called on a characters instantiation;
		character = c;
		skill = s; 
	}

	public void SelectSkill(){
		if (skill.OnCooldown) {
			Debug.Log ("skills on cooldown brah");
		} else {
			selected = false;
			target = character.GetComponent<BaseCharacter> ().CurrentTarget;
			if (target != null) {
				SelectTarget ();
				if (skill.Target != null) {
					character.GetComponent<BaseCharacter> ().MadeSelection = true;
					selected = true;
					character.GetComponent<BaseCharacter> ().SelectSkill (skill);
					Debug.Log (character.GetComponent<BaseCharacter> ().name + ": Has selected - " + skill.Information.Name);
					UseSkill ();
				}
			} else {
				Debug.Log ("target is nill");
			}
		}
	}

	public void SelectTarget(){
		allowedTarget = skill.AllowedTarget (target, character);
		if (allowedTarget) {
			skill.Target = target;
			Debug.Log ("tar: " + target + " from: " + skill.Owner.GetComponent<BaseCharacter>().CharacterName);
		} else {
			Debug.Log ("you have selected an invalid target");
		}
	}

	public void DisplayInfo(){
		Debug.Log (transform.parent.transform.GetChild (1).name);
		transform.parent.transform.GetChild (1).gameObject.SetActive (true);
	}

	public void UseSkill(){
		skill.UseSkill ();
		if (skill.Owner.GetComponent<BaseCharacter> ().Controller == "player") {
			uiManager.ChangeTargetUi (skill.Target);
			uiManager.ChangeUserUi (skill.Owner);
		}else{
			uiManager.ChangeTargetUi (skill.Owner);
			uiManager.ChangeUserUi (skill.Target);
		}
	}
}
