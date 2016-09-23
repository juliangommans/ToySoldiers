using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class BaseCharacter : MonoBehaviour {

	// Character skills/abilities etc.
	private string characterName;
	private List<BaseSkill> skills;
	private BaseSkill selectedSkill;

	// Character base stats
	private int health;
	private int baseHealth;
	private int maxHealth;
	private int baseMaxHealth;
	private int attack;
	private int baseAttack;
	private int defense;
	private int baseDefense;
	private int energy;
	private int baseEnergy; // (special attack)
	private int resistance;
	private int baseResistance ;// (special defence)
	private int speed;
	private int baseSpeed;

	// Character advanced stats
	private int shield; // (absorbs damage, resets each round)
	private int baseShield;
	private int retaliations; // (defaults to one retaliation a round)
	private int baseRetaliations;
	private int retaliationDamage; // (defaults to 50%)
	private int baseRetaliationDamage;
	private int comboPoints; // (starts at zero, once it reaches max, 4, attacks do extra things)
	private int baseComboPoints;
	private int maxComboPoints;
	private int baseMaxComboPoints;
	private int actionPoints; // (used on abilities, start with 4 max)
	private int baseActionPoints;
	private int maxActionPoints;
	private int baseMaxActionPoints;
	private int gloabalCooldownReduction;
	private int baseGlobalCooldownReduction;

	// Character passive modifiers - might need to impliment/learn about c# lists
	public List<GameObject> buffs;
	public List<GameObject> deBuffs;

	// Interactive variables
	private bool madeSelection;
	private bool turnComplete;
	private bool yourTurn;
	private GameObject currentTarget;

	// Experience and OOC stats
	private int level;
	private int experience;

	// Getters and Setters
	public string CharacterName {get; set;}
	public List<BaseSkill> Skills {get; set;}
	public BaseSkill SelectedSkill { get; set; }

	public bool MadeSelection { get; set; }
	public bool TurnComplete { get; set; }
	public bool YourTurn { get; set; }
	public GameObject CurrentTarget { get; set; }

	public int Health {get; set;}
	public int BaseHealth { get; set; }
	public int MaxHealth {get; set;}
	public int BaseMaxHealth { get; set; }
	public int Attack {get; set;}
	public int BaseAttack {get; set;}
	public int Defense {get; set;}
	public int BaseDefense {get; set;}
	public int Energy {get; set;}
	public int BaseEnergy {get; set;}
	public int Resistance {get; set;}
	public int BaseResistance {get; set;}
	public int Speed {get; set;}
	public int BaseSpeed {get; set;}

	public int Shield {get; set;}
	public int BaseShield { get; set; }
	public int Resilience {get; set;}
	public int BaseResilience {get; set;}
	public int Retaliations {get; set;}
	public int BaseRetaliations {get; set;}
	public int RetaliationDamage {get; set;}
	public int BaseRetaliationDamage {get; set;}
	public int ComboPoints {get; set;}
	public int BaseComboPoints {get; set;}
	public int MaxComboPoints {get; set;}
	public int BaseMaxComboPoints {get; set;}
	public int ActionPoints {get; set;}
	public int BaseActionPoints {get; set;}
	public int MaxActionPoints {get; set;}
	public int BaseMaxActionPoints {get; set;}
	public int GlobalCooldownReduction {get; set;}
	public int BaseGlobalCooldownReduction {get; set;}

	public int Level {get; set;}
	public int Experience {get; set;}

	public BaseCharacter (){
		CharacterName = "Character";
		Skills = new List<BaseSkill>();
		MadeSelection = false;

		BaseHealth = 300;
		BaseMaxHealth = 300;
		BaseAttack = 30;
		BaseDefense = 30;
		BaseEnergy = 30;
		BaseResistance = 30;
		BaseSpeed = 30;

		BaseShield = 5;
		BaseResilience = 0;
		BaseRetaliations = 1;
		BaseRetaliationDamage = 5;
		BaseComboPoints = 0;
		BaseMaxComboPoints = 3;
		BaseActionPoints = 0;
		BaseMaxActionPoints = 4;
		BaseGlobalCooldownReduction = 0;

		Level = 0;
		Experience = 0;
	}

	// This should be called whenever there is a change to anyones stats (maybe once per update)
	// i.e. keeps the % increase relevent to the current state of the game
	public void ImplimentTraits () { 
		ResetStats();
	}

	// This should be called by characters when adding a skill
	public void AddSkill (BaseSkill skill) {
		skill.Owner = this.gameObject;
		skills.Add (skill);
	}

	void UpdateStats (string stat, float percent){
		switch (stat) {
		// Base stats
		case "Health":
			Health += (int)(BaseHealth * percent);
			break;
		case "MaxHealth":
			MaxHealth += (int)(BaseMaxHealth * percent);
			break;
		case "Attack":
			Attack += (int)(BaseAttack * percent);
			break;
		case "Defense":
			Defense += (int)(BaseDefense * percent);
			break;
		case "Energy":
			Energy += (int)(BaseEnergy * percent);
			break;
		case "Resistance":
			Resistance += (int)(BaseResistance * percent);
			break;
		case "Speed":
			Speed += (int)(BaseSpeed * percent);
			break;
			// Advanced stats
		case "Shield":
			Shield += (int)(BaseShield * percent);
			break;
		case "Resilience":
			Resilience += (int)(BaseResilience * percent);
			break;
		case "Retaliations":
			Retaliations += (int)(BaseRetaliations * percent);
			break;
		case "RetaliationDamage":
			RetaliationDamage += (int)(BaseRetaliationDamage * percent);
			break;
		case "ComboPoints":
			ComboPoints += (int)(BaseComboPoints * percent);
			break;
		case "MaxComboPoints":
			MaxComboPoints += (int)(BaseMaxComboPoints * percent);
			break;
		case "ActionPoints":
			ActionPoints += (int)(BaseActionPoints * percent);
			break;
		case "MaxActionPoints":
			MaxActionPoints += (int)(BaseMaxActionPoints * percent);
			break;
		case "GlobalCooldownReduction":
			GlobalCooldownReduction += (int)(BaseGlobalCooldownReduction * percent);
			break;
		}
	}

	public void UpdateBaseStats (string stat, int change){
		switch (stat) {
		// Base stats
		case "Health":
			BaseHealth += change;
			break;
		case "MaxHealth":
			BaseMaxHealth += change;
			break;
		case "Attack":
			BaseAttack += change;
			break;
		case "Defense":
			BaseDefense += change;
			break;
		case "Energy":
			BaseEnergy += change;
			break;
		case "Resistance":
			BaseResistance += change;
			break;
		case "Speed":
			BaseSpeed += change;
			break;
			// Advanced stats
		case "Shield":
			BaseShield += change;
			break;
		case "Resilience":
			BaseResilience += change;
			break;
		case "Retaliations":
			BaseRetaliations += change;
			break;
		case "RetaliationDamage":
			BaseRetaliationDamage += change;
			break;
		case "ComboPoints":
			BaseComboPoints += change;
			break;
		case "MaxComboPoints":
			BaseMaxComboPoints += change;
			break;
		case "ActionPoints":
			BaseActionPoints += change;
			break;
		case "MaxActionPoints":
			BaseMaxActionPoints += change;
			break;
		case "GlobalCooldownReduction":
			BaseGlobalCooldownReduction += change;
			break;
		}
	}

	public void ResetStats (){
		Health = BaseHealth;
		MaxHealth = BaseMaxHealth;
		Attack = BaseAttack;
		Defense = BaseDefense;
		Energy = BaseEnergy;
		Resistance = BaseResistance;
		Speed = BaseSpeed;

		Shield = BaseShield;
		Resilience = BaseResilience;
		Retaliations = BaseRetaliations;
		RetaliationDamage = BaseRetaliationDamage;
		GlobalCooldownReduction = BaseGlobalCooldownReduction;
	}
}
