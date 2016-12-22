using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseSkill {

	// private stats
	private DmgAndHealManager dmgAndHealManager;

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
	public bool TargetGround{ get; set; }
	public bool Melee { get; set; }
	public bool Ranged { get; set; }
	public bool Corporeal { get; set; } // "physical" - based off attack/defense
	public bool Ethereal { get; set; } // "magical" - based of power/resistance
	public bool SingleTarget { get; set; }

	// Classifiers
	public List<BaseSkillEffects> Effects { get; set; }
	public BaseSchool School { get; set; }
	public int Level;

	public BaseSkill () {
		Information = new BaseObjectInformation("skill", "description");
		// defaults
		Effects = new List<BaseSkillEffects>();
		Power = 10;
		Cost = 2;
		Cooldown = 2;
		BonusMultiplier = 1.5f;
		CooldownRemaining = 0;
		Level = 1;

		TargetEnemy = false;
		TargetFriend = false;
		TargetGround = false;
		Melee = false;
		Ranged = false;
		Corporeal = false;
		Ethereal = false;
		OnCooldown = false;

		dmgAndHealManager = new DmgAndHealManager(this);
	}

	public string Targets(){
		string targets = "";
		if (TargetEnemy) {
			targets += " Enemy";
		}		
		if (TargetFriend) {
			targets += " Friend";
		}
		if (TargetGround) {
			targets += " Ground";
		}
		if (targets.Length > 0) {
			targets.Substring (1);
		}
		return targets;
	}

	public bool AllowedTarget (GameObject t, GameObject c){
		if (TargetGround) {
			if (t.tag == "HexCell") {
				return true;
			} else {
				// if aoe and target another character, we allow it to return true
				if (TargetCharacter (t, c)) {
					return true;
				} else {
					return false;
				}
			}
		}
		if (TargetCharacter (t, c)) {
			return true;
		} else {
			return false;
		}
	}

	private bool TargetCharacter(GameObject tar, GameObject character){
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
				dmgAndHealManager.HealLogic ();
			}
		} else if (TargetEnemy) {
			if (Power == 0) {
				DebuffLogic ();
			} else {
				dmgAndHealManager.DamageLogic();
			}
		} else if (TargetGround) {
			// move skills, utility skills and aoe skills
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

	private void ResolveSkill (){
		this.Owner.GetComponent<BaseCharacter> ().Stats.ActionPoints -= Cost;
		OnCooldown = true;
		CooldownRemaining = Cooldown;
	}

}
