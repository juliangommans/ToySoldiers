using UnityEngine;
using System.Collections;

public class DmgAndHealManager : MonoBehaviour {

	private BaseSkill skill;
	private int powerStat; // player.attack or player.power etc.
	private int defenseStat;

	public DmgAndHealManager (BaseSkill s){
		skill = s;
	}

	public void HealLogic(){
		HealStats ();
		skill.Target.GetComponent<BaseCharacter> ().Stats.ChangeHealth (PowerCalculation());
	}

	private void HealStats(){
		if (skill.Corporeal){
			powerStat = skill.Owner.GetComponent<BaseCharacter> ().Stats.Attack;

		} else{
			powerStat = skill.Owner.GetComponent<BaseCharacter> ().Stats.Power;
		}
		defenseStat = skill.Target.GetComponent<BaseCharacter> ().Stats.Defense + skill.Target.GetComponent<BaseCharacter> ().Stats.Resistance;
	}

	public void DamageLogic(){
		DamageStats ();
		DeductDamage (PowerCalculation ());
	}

	private void DamageStats(){
		if (skill.Corporeal){
			powerStat = skill.Owner.GetComponent<BaseCharacter> ().Stats.Attack;
			defenseStat = skill.Target.GetComponent<BaseCharacter> ().Stats.Defense;
		} else{
			powerStat = skill.Owner.GetComponent<BaseCharacter> ().Stats.Power;
			defenseStat = skill.Target.GetComponent<BaseCharacter> ().Stats.Resistance;
		}
	}

	private int PowerCalculation (){
		int schoolModifiedPower = skill.School.SchoolEffectsForSkill (skill);
		float damage = schoolModifiedPower *  powerStat / defenseStat;
		int rounded = (int)Mathf.Floor (damage / 2); // globabl constant in order to prevent huge damage spikes.
		return rounded;
	}

	private void DeductDamage (int damage){
		skill.Target.GetComponent<BaseCharacter> ().Stats.TakeDamage (damage);
		Debug.Log ("" + skill.Target.name + " should be taking " + damage + " damage from health"); 
	}

}
