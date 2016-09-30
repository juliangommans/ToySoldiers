using UnityEngine;
using System.Collections;

public class BaseStats : MonoBehaviour {

	// Character base stats
	public int Health {get; set;}
	public int BaseHealth { get; set; }
	public int MaxHealth {get; set;}
	public int BaseMaxHealth { get; set; }
	public int Attack {get; set;}
	public int BaseAttack {get; set;}
	public int Defense {get; set;}
	public int BaseDefense {get; set;}
	public int Power {get; set;} // (special attack)
	public int BasePower {get; set;}
	public int Resistance {get; set;} // (special defence)
	public int BaseResistance {get; set;}
	public int Speed {get; set;}
	public int BaseSpeed {get; set;}

	// Character advanced stats
	public int Shield {get; set;} // (absorbs damage, resets each round)
	public int BaseShield { get; set; }
	public int Resilience {get; set;}
	public int BaseResilience {get; set;}
	public int Retaliations {get; set;} // (defaults to one retaliation a round)
	public int BaseRetaliations {get; set;}
	public int RetaliationDamage {get; set;} // (defaults to 50%)
	public int BaseRetaliationDamage {get; set;}
	public int ComboPoints {get; set;} // (starts at zero, once it reaches max, 4, attacks do extra things)
	public int BaseComboPoints {get; set;}
	public int MaxComboPoints {get; set;}
	public int BaseMaxComboPoints {get; set;}
	public int ActionPoints {get; set;} // (used on abilities, start with 4 max)
	public int BaseActionPoints {get; set;}
	public int MaxActionPoints {get; set;}
	public int BaseMaxActionPoints {get; set;}
	public int GlobalCooldownReduction {get; set;}
	public int BaseGlobalCooldownReduction {get; set;}

	void UpdateStats (string stat, float percent){
		switch (stat) {
		// Base stats * modifiers
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
		case "Power":
			Power += (int)(BasePower * percent);
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
		// Base stats +/- flat amount
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
		case "Power":
			BasePower += change;
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
		Power = BasePower;
		Resistance = BaseResistance;
		Speed = BaseSpeed;

		ActionPoints = BaseActionPoints;

		Shield = BaseShield;
		Resilience = BaseResilience;
		Retaliations = BaseRetaliations;
		RetaliationDamage = BaseRetaliationDamage;
		GlobalCooldownReduction = BaseGlobalCooldownReduction;
	}

}
