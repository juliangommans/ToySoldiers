using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour {

	public enum TurnState {
		BeginTurn, // player/opponentes choose actions
		ManageOutcome, // actions based on player/opponent actions
		EndTurn, // report outcomes, list new totals
		EndOfRound // something that is always checked, when no further actions are available
	}

	private TurnState turnState;
	private List<GameObject> characters;

	private List<GameObject> players;
	private List<GameObject> enemies;

	// bools
	public bool finishedRound;
	private bool playerSelectedAction;
	private bool turnActionsComplete;
	private bool inAction;


	public void StartTurn (List<GameObject> p, List<GameObject> e){
		finishedRound = false;
		turnState = TurnState.BeginTurn;
		players = p;
		enemies = e;
		CreateCharactersList ();
		playerSelectedAction = false;
		turnActionsComplete = false;
		inAction = false;
	}
		
	public void CheckTurn () {
		switch(turnState)
		{
		case TurnState.BeginTurn:
			// wait for player to choose his action
//			if (player.GetComponent<Player>().MadeSelection) {
				// auto select opponents actions

//				turnState = TurnState.ManageOutcome;
			SpeedCalculation ();
//			}

			break;
		case TurnState.ManageOutcome:
			// do actions, animations, calculations etc.
			for (int i = 0; i < characters.Count; i++) {
				StartCoroutine (EachCharactersTurn(characters [i]));
			}
			if (turnActionsComplete) {
				turnState = TurnState.EndTurn;
			}
			break;
		case TurnState.EndTurn:
			// actions have occured, we have the outcome, now begin turn again if we have AP left over
			break;
		case TurnState.EndOfRound:
			finishedRound = true;
			break;
		}
	}

	void SpeedCalculation (){
		Debug.Log ("before " + characters[0].GetComponent<BaseCharacter>().CharacterName);
		characters.Sort ((x, y) => x.GetComponent<BaseCharacter>().Speed.CompareTo (y.GetComponent<BaseCharacter>().Speed));
		Debug.Log ("after " + characters[0].GetComponent<BaseCharacter>().CharacterName);
	}

	void CreateCharactersList (){
		characters = enemies;
		for (int i = 0; i < players.Count; i++) {
			characters.Add (players [i]);
		}
	}

	private IEnumerator EachCharactersTurn(GameObject character){
		while(!character.GetComponent<BaseCharacter>().TurnComplete){
			character.GetComponent<BaseCharacter> ().YourTurn = true;
			yield return null;
		}
		character.GetComponent<BaseCharacter> ().YourTurn = false;
		// start waitforXseconds until all actions have been taken? Maybe don't need to.
	}
}
