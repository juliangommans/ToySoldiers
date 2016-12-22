using UnityEngine;
using System.Collections;

public class Freeze : BaseSkillEffects {

	private const string name = "Freeze";
	private const string description = "Freeze's effected target(s) lowering their maximum action points and reducing speed";

	public Freeze (BaseSkill s)
		:base(new BaseObjectInformation(name,description)){
		this.PrimaryStat = "speed";
		this.PrimaryAmount = 0.8f;
		this.Skill = s;
	}

	public override void PerformEffect(){
		BaseCharacter ph = Skill.Target.GetComponent<BaseCharacter> ();
		ph.DeBuffs.Add (this);
		ph.Stats.UpdateStatsWithMultiplier (PrimaryStat, PrimaryAmount);
		ph.Stats.MaxActionPoints -= 1;
	}

	public override void RemoveEffects(BaseCharacter ph){
		ph.DeBuffs.Remove (this);
		ph.Stats.UpdateStatsWithMultiplier (PrimaryStat, (2.0f - PrimaryAmount));
		ph.Stats.MaxActionPoints += 1;
	}
}
