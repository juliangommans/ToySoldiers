using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseSkill {

	// Information
	private BaseObjectInformation information;
	private GameObject owner;

	// Combat Stats
	private int power;
	private int cost; // number of AP it costs to use
	private int cooldown; // how many turns it takes before you can use it again (1 is every turn)
	private float bonusMultiplier;
	private BaseBonus bonus;
	private GameObject target;

	// Bool Checks
	private bool targetOther;
	private bool targetSelf;
	private bool melee;
	private bool ranged;
	private bool corporeal; // "physical" - based off attack/defense
	private bool ethereal; // "magical" - based of etheral damage/resistance
	private bool singleTarget;

	// Groups
	private List<BaseSkillEffects> effects;
	private BaseSchool school;

	// Getters
	public BaseObjectInformation Information{ get{ return information; } }
	public GameObject Owner { get; set; }
	public int Power{ get; set; }
	public int Cost{ get; set; }
	public int Cooldown{ get; set; }
	public bool TargetOther{ get; set; }
	public bool TargetSelf{ get; set; }
	public bool Melee { get; set; }
	public bool Ranged { get; set; }
	public bool Corporeal { get; set; }
	public bool Ethereal { get; set; }
	public bool SingleTarget { get; set; }
	public float BonusMultiplier { get; set; }
	public GameObject Target { get; set; }
	public BaseBonus Bonus { get; set; }
	public List<BaseSkillEffects> Effects { get; set; }
	public BaseSchool School { get; set; }

	public BaseSkill (BaseObjectInformation setInfo) {
		information = setInfo;
		// defaults
		effects = new List<BaseSkillEffects>();
		power = 10;
		cost = 2;
		cooldown = 2;
		bonusMultiplier = 1.5f;

		targetOther = false;
		targetSelf = false;
		melee = false;
		ranged = false;
		corporeal = false;
		ethereal = false;
	}

	public BaseSkill (BaseObjectInformation setInfo, GameObject o) {
		information = setInfo;
		this.Owner = o;
		// defaults
		effects = new List<BaseSkillEffects>();
		power = 10;
		cost = 2;
		cooldown = 2;
		bonusMultiplier = 1.5f;

		targetOther = false;
		targetSelf = false;
		melee = false;
		ranged = false;
		corporeal = false;
		ethereal = false;
	}

	public bool AllowedTarget (GameObject tar, GameObject character){
		if (TargetOther) {
			if (tar.GetInstanceID() == character.GetInstanceID()){
				return false;
			}else{
				return true;
			}
		}
		if (TargetSelf) {
			if (tar.GetInstanceID() == character.GetInstanceID()){
				return true;
			}else{
				return false;
			}
		}
		Debug.LogWarning ("Your skill has both target other and self as false");
		return false;
	}

	public virtual void UseSkill() {
		Debug.LogWarning ("NO SKILL BEHAVIOUR ADDED");
	}


}
