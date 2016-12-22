using UnityEngine;
using System.Collections;

public class BaseSkillEffects {

	private BaseObjectInformation information;
	public BaseSkill Skill;

	public string PrimaryStat;
	public string SecondaryStat;
	public float PrimaryAmount;
	public float SecondaryAmount;
	public int Duration;
	public int RemainingDuration;

	public BaseSkillEffects(BaseObjectInformation setInfo ) {
		information = setInfo;
		this.Duration = 3;
		this.RemainingDuration = 3;
	}
			
	public virtual void PerformEffect(){
		Debug.LogWarning ("NO SKILL EFFECT ADDED " + this.information.Name);
	}

	public virtual void RemoveEffects(BaseCharacter ph){
		Debug.LogWarning ("NO EFFECTS TO REMOVE " + this.information.Name);
	}

	public BaseObjectInformation Information{ get { return information; } }

}
