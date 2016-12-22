using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BaseSchool {
	
	public List<BaseSkill> skills;
	public BaseObjectInformation Information{ get; set; }
	public BaseBonus DefaultBonus { get; set; }
	public string StrongAgainst { get; set; }
	public float StrongPercent { get; set; }
	public string WeakAgainst { get; set; }
	public float WeakPercent { get; set; }

	public BaseSchool(BaseObjectInformation setInfo){
		Information = setInfo;
		StrongAgainst = "Stat";
		StrongPercent = 0.25f;
		WeakAgainst = "Stat";
		WeakPercent = -0.25f;
	}

	public string ToolTip(){
		string tt = Information.Description;
		tt += " " + Information.Name + " deals extra damage against " + StrongAgainst;
		tt += " and takes extra damage from " + WeakAgainst + ".";
		return tt;
	}

	public int SchoolEffectsForSkill(BaseSkill skill){
		float modifier = 1f;

		// Check for same school bonus and apply if there
		for (int i = 0; i < skill.Owner.GetComponent<BaseCharacter> ().Schools.Count; i++) {
			if (Information.Name == skill.Owner.GetComponent<BaseCharacter> ().Schools[i].Information.Name) {
				modifier += skill.Owner.GetComponent<BaseCharacter> ().Stats.SameSchoolBonus;
			}
		}

		// Check if there are boosts or negative boosts to be applies based on skill vs opponent
		for (int i = 0; i < skill.Target.GetComponent<BaseCharacter> ().Schools.Count; i++) {
			if (StrongAgainst == skill.Target.GetComponent<BaseCharacter> ().Schools [i].Information.Name) {
				modifier += StrongPercent;
			}
			if (WeakAgainst == skill.Target.GetComponent<BaseCharacter> ().Schools [i].Information.Name) {
				modifier += WeakPercent;
			}
		}
		int rounded = (int)Mathf.Floor (modifier * skill.Power);
		return rounded;
	}
}
