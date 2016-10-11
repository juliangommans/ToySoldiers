using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseSkill {

	// private stats
	private int  powerStat; // player.attack or player.power etc.
	private int defenseStat;

	// Objects
	public BaseObjectInformation Information{ get; set; }
	public GameObject Owner { get; set; }
	public GameObject Target { get; set; }

	// Combat Stats
	public int Power{ get; set; }
	public int Cost{ get; set; } // number of AP it costs to use
	public int Cooldown{ get; set; } // how many rounds it takes before you can use it again (1 is every round)
	public int CooldownRemaining { get; set; }
	public BaseBonus Bonus { get; set; }
	public float BonusMultiplier { get; set; }

	// Bool Checks
	public bool OnCooldown{ get; set; }
	public bool TargetEnemy{ get; set; }
	public bool TargetFriend{ get; set; }
	public bool Melee { get; set; }
	public bool Ranged { get; set; }
	public bool Corporeal { get; set; } // "physical" - based off attack/defense
	public bool Ethereal { get; set; } // "magical" - based of power/resistance
	public bool SingleTarget { get; set; }

	// Classifiers
	public List<BaseSkillEffects> Effects { get; set; }
	public BaseSchool School { get; set; }

	public BaseSkill (BaseObjectInformation setInfo) {
		Information = setInfo;
		// defaults
		Effects = new List<BaseSkillEffects>();
		Power = 10;
		Cost = 2;
		Cooldown = 2;
		BonusMultiplier = 1.5f;
		CooldownRemaining = 0;

		TargetEnemy = false;
		TargetFriend = false;
		Melee = false;
		Ranged = false;
		Corporeal = false;
		Ethereal = false;
		OnCooldown = false;
	}

	public BaseSkill (BaseObjectInformation setInfo, GameObject o) {
		Information = setInfo;
		this.Owner = o;
		// defaults
		Effects = new List<BaseSkillEffects>();
		Power = 10;
		Cost = 2;
		Cooldown = 2;
		BonusMultiplier = 1.5f;
		CooldownRemaining = 0;

		TargetEnemy = false;
		TargetFriend = false;
		Melee = false;
		Ranged = false;
		Corporeal = false;
		Ethereal = false;
		OnCooldown = false;
	}

	public bool AllowedTarget (GameObject tar, GameObject character){
		if (TargetEnemy) {
			if (tar.GetInstanceID() == character.GetInstanceID()){
				return false;
			}else{
				return true;
			}
		}
		if (TargetFriend) {
			if (tar.GetInstanceID() == character.GetInstanceID()){
				return true;
			}else{
				return false;
			}
		}
		Debug.LogWarning ("Your skill has both target other and self as false");
		return false;
	}

	public void UseSkill() {
		// Might want to add some sort of precurser "special effects" method here?
		ActivateSkillEffects();
		if (TargetFriend) {
			if (Power == 0) {
				BuffLogic ();
			} else {
				HealLogic ();
			}
		} else {
			if (Power == 0) {
				DebuffLogic ();
			} else {
				DamageLogic ();
			}
		}
		ResolveSkill ();
	}

	public virtual void ActivateSkillEffects(){
		Debug.LogWarning ("NO CUSTOM SKILL EFFECTS ADDED");
	}

	private void BuffLogic(){
		// Insert buff logic here
	}

	private void DebuffLogic(){
		//insert DEbuff logic here
	}

	private void HealLogic(){
		HealStats ();
		this.Target.GetComponent<BaseCharacter> ().Stats.ChangeHealth (PowerCalculation());
	}

	private void HealStats(){
		if (this.Corporeal){
			powerStat = this.Owner.GetComponent<BaseCharacter> ().Stats.Attack;

		} else{
			powerStat = this.Owner.GetComponent<BaseCharacter> ().Stats.Power;
		}
		defenseStat = this.Target.GetComponent<BaseCharacter> ().Stats.Defense + this.Target.GetComponent<BaseCharacter> ().Stats.Resistance;
	}

	private void DamageLogic(){
		DamageStats ();
		DeductDamage (PowerCalculation ());
	}

	private void DamageStats(){
		if (this.Corporeal){
			 powerStat = this.Owner.GetComponent<BaseCharacter> ().Stats.Attack;
			defenseStat = this.Target.GetComponent<BaseCharacter> ().Stats.Defense;
		} else{
			 powerStat = this.Owner.GetComponent<BaseCharacter> ().Stats.Power;
			defenseStat = this.Target.GetComponent<BaseCharacter> ().Stats.Resistance;
		}
	}

	private int PowerCalculation (){
		float damage = Power *  powerStat / defenseStat;
		int rounded = (int)Mathf.Floor (damage / 2); // globabl constant in order to prevent huge damage spikes.
		return rounded;
	}

	private void DeductDamage (int damage){
		this.Target.GetComponent<BaseCharacter> ().Stats.TakeDamage (damage);
		Debug.Log ("" + this.Target.name + " should be taking " + damage + " damage from health"); 
	}

	private void ResolveSkill (){
		this.Owner.GetComponent<BaseCharacter> ().Stats.ActionPoints -= Cost;
		OnCooldown = true;
		CooldownRemaining = Cooldown;
	}

}
