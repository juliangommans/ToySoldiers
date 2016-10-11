using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

// Becoming far too bloated, maybe move all stats stuff out into a stats script/class?

public class BaseCharacter : MonoBehaviour {

	// Background info
	private GameObject gameManager;

	// Character skills/abilities etc.
	public string CharacterName {get; set;}
	public List<BaseSkill> Skills {get; set;}
	public BaseSkill SelectedSkill { get; set; }
	public BaseStats Stats { get; set; }

	// Character passive modifiers - might need to impliment/learn about c# lists
	public List<GameObject> buffs;
	public List<GameObject> deBuffs;

	// Interactive variables
	public bool MadeSelection { get; set; }
	public bool TurnComplete { get; set; }
	public bool YourTurn { get; set; }
	public GameObject CurrentTarget { get; set; }
	public float MoveSpeed { get; set; }
	public GameObject Location { get; set; }
	public string Controller { get; set; } // Player - Computer

	//Movement
	private bool moving;
	private GameObject destination;
	private float moveSpeed; //Howmany hexs you can move per action point (0.5,1 or 1.5)
	private float moveAnimationSpeed;
	private GameObject location;

	// Experience and OOC stats
	public int Level {get; set;}
	public int Experience {get; set;}

	void Awake(){
		gameManager = GameObject.Find ("GameManager");
	}

	public BaseCharacter (){
		CharacterName = "Character";
		Stats = new BaseStats ();
		Skills = new List<BaseSkill>();

		Stats.BaseHealth = 300;
		Stats.BaseMaxHealth = 300;
		Stats.BaseAttack = 30;
		Stats.BaseDefense = 30;
		Stats.BasePower = 30;
		Stats.BaseResistance = 30;
		Stats.BaseSpeed = 30;

		Stats.BaseShield = 5;
		Stats.BaseResilience = 0;
		Stats.BaseRetaliations = 1;
		Stats.BaseRetaliationDamage = 5;
		Stats.BaseComboPoints = 0;
		Stats.BaseMaxComboPoints = 3;
		Stats.BaseActionPoints = 4;
		Stats.BaseMaxActionPoints = 4;
		Stats.BaseGlobalCooldownReduction = 0;

		MoveSpeed = 0.5f;

		Level = 0;
		Experience = 0;
		moveAnimationSpeed = 1.5f;
	}

	// This should be called whenever there is a change to anyones stats (maybe once per update)
	// i.e. keeps the % increase relevent to the current state of the game
	public void ImplimentTraits () { 
		Stats.ResetStats();
	}

	// This should be called by characters when adding a skill
	public void AddSkill (BaseSkill skill) {
		skill.Owner = this.gameObject;
		Skills.Add (skill);
		//instantiate a butotn and it's scripts
	}

	public void ReadyForTurn(){
		YourTurn = true;
		MadeSelection = false;
		TurnComplete = false;
		this.GetComponent<HighlightBlack> ().triggerColor = true;
	}

// Movement Logic
	public bool IsLegalMove(int distance){
		int cost = (int)Mathf.Ceil (distance * MoveSpeed);
		Debug.Log("Cost: " + cost + "|Points: " + Stats.ActionPoints + "|Distance: " + distance);
		if (cost <= Stats.ActionPoints) {
			Stats.ActionPoints -= cost;
			return true;
		} else {
			return false;
		}
	}

	public void MoveCharacter(GameObject newLocation){
		destination = newLocation;
		moving = true;
		ChangeLocation ();
	}

	private void ChangeLocation(){
		Location.GetComponent<HexCell>().occupant = null;
		Location.GetComponent<HexCell>().occupied = false;
		Location = destination;
		Location.GetComponent<HexCell> ().occupied = true;
		Location.GetComponent<HexCell>().occupant = this.gameObject;
	}

	public void SelectSkill (BaseSkill s) {
		MadeSelection = true;
		SelectedSkill = s;
	}

// End turn - called whenever any AP spending action is performed
	private void EndTurn(){
		MadeSelection = true;
		TurnComplete = true;
		moving = false;
	}

	void Update (){
		if (moving) {
			transform.position = Vector3.Lerp(transform.position, destination.transform.position, (moveAnimationSpeed * gameManager.GetComponent<GameSettingsManager>().gameSpeed) * Time.deltaTime);
		}
		if (moving && !MadeSelection && destination != null && this.transform.position == destination.transform.position) {
			EndTurn ();
		}
	}
		
}
