using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManager {

	public enum TurnState {
		BeginTurn, // player/opponentes choose actions
		ManageOutcome, // actions based on player/opponent actions
		NextCharacter,
		EndTurn, // report outcomes, list new totals
		EndOfRound // something that is always checked, when no further actions are available
	}

	private TurnState turnState;
	private List<GameObject> characters;
	public GameObject currentCharacter;
	public GameObject currentTarget;

	private List<GameObject> players;
	private List<GameObject> enemies;

	// bools
	public bool finishedRound;
	private bool playerSelectedAction;
	private bool inAction;

	private int currentCharacterIndex;

	public void StartTurn (List<GameObject> p, List<GameObject> e){
		finishedRound = false;
		turnState = TurnState.BeginTurn;
		players = p;
		enemies = e;
		CreateCharactersList ();
		currentCharacterIndex = 0;
		playerSelectedAction = false;
		inAction = false;
	}
		
	public void CheckTurn () {
		switch(turnState)
		{
		case TurnState.BeginTurn:
			SpeedCalculation ();
			currentCharacterIndex = 0;
			turnState = TurnState.ManageOutcome;
			break;
		case TurnState.ManageOutcome:
			if (currentCharacterIndex >= characters.Count) {
				turnState = TurnState.EndTurn;
			} else {
				currentCharacter = characters [currentCharacterIndex];
				currentCharacter.GetComponent<BaseCharacter> ().ReadyForTurn();
				currentCharacterIndex++;
				turnState = TurnState.NextCharacter;
			}
			break;
		case TurnState.NextCharacter:
			if (currentCharacter.GetComponent<BaseCharacter> ().Controller == "computer") {
				Debug.Log ("computer passes");
				turnState = TurnState.ManageOutcome;
			}
			if (currentCharacter.GetComponent<BaseCharacter> ().TurnComplete) {
				turnState = TurnState.ManageOutcome;
			}
			break;
		case TurnState.EndTurn:
			Debug.Log ("TURN ENDED");
			FilterFinishedCharacters ();
			if (characters.Count > 0) {
				turnState = TurnState.BeginTurn;
			} else {
				turnState = TurnState.EndOfRound;
			}

			// actions have occured, we have the outcome, now begin turn again if we have AP left over
			break;
		case TurnState.EndOfRound:
			finishedRound = true;
			break;
		}
	}

	void FilterFinishedCharacters(){
//		Debug.Log ("Before: Characters count = " + characters.Count);
		List<GameObject> tempList = new List<GameObject>();
		for (int i = 0; i < characters.Count; i++) {
			if (characters [i].GetComponent<BaseCharacter> ().Stats.ActionPoints > 0) {
				tempList.Add (characters[i]);
			}
		}
		characters = tempList;
//		Debug.Log ("After: Characters count = " + characters.Count);
	}

	void SpeedCalculation (){
//		Debug.Log ("before " + characters[0].GetComponent<BaseCharacter>().CharacterName);
		characters.Sort ((x, y) => y.GetComponent<BaseCharacter>().Stats.Speed.CompareTo (x.GetComponent<BaseCharacter>().Stats.Speed));
//		Debug.Log ("after " + characters[0].GetComponent<BaseCharacter>().CharacterName);
	}

	void CreateCharactersList (){
		characters = enemies;
		characters.AddRange (players);
	}
}
