using UnityEngine;
using System.Collections;

public class BoltSkill : BaseSkill {

	// Bolt skills are the most basic spell, ranged, single target, damage of a school type
	// These are generated at run time for whatever school you want.

	public BoltSkill(BaseSchool school)
		:base(){
		this.Power = 20;
		this.Cooldown = 1;
		this.TargetEnemy = true;
		this.Ranged = true;
		this.Ethereal = true;
		this.School = school;
		this.Information = BuildInformation (school);
	}

	private BaseObjectInformation BuildInformation (BaseSchool school){
		string name = school.Information.Name + " Bolt";
		string description = "A ranged ethereal spell that deals "+ school.Information.Name +" damage";
		return new BaseObjectInformation (name, description);
	}	
}
