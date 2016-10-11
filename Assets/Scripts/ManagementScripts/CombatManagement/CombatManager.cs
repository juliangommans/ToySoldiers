using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatManager {

	public enum CombatState {
		BeforeCombat, 
		BeginRound, 
		TurnManager, 
		EndRound, 
		AfterCombat
	}

	public CombatManager(GameManager g){
		gm = g;
	}

	public GameManager gm;
	public TurnManager turnManager;
//	public GameObject currentCharacter;
//	private GameObject player;
	private List<GameObject> playerCharacters;
	private List<GameObject> enemyCharacters;
	private CombatState combatState;
	private int round;
//	private int frameCounter;
	private GameObject enemy;
	private CharacterManager characterManager;

	// TEST
	private bool testBool;

	public void StartCombat () {
//		player = p;
		characterManager = new CharacterManager();
		turnManager = new TurnManager (gm, this);
		combatState = CombatState.BeforeCombat;
		round = 0;
		testBool = false;
	}

	public void CheckCombat () {
		switch (combatState) 
		{
		case CombatState.BeforeCombat:
			FetchCharacters ();
			combatState = CombatState.BeginRound; // Testing purposes - SHOULD give options here.
			break;
		case CombatState.BeginRound:
			if (!testBool) {
				turnManager.StartTurn (playerCharacters, enemyCharacters);
				testBool = true;
			}
			round += 1;
			combatState = CombatState.TurnManager;
			break;
		case CombatState.TurnManager:
			turnManager.CheckTurn ();
//			currentCharacter = turnManager.currentCharacter;
			if (turnManager.finishedRound == true) {
				combatState = CombatState.EndRound;
			}
			break;
		case CombatState.EndRound:
			// check if we have people alive on both sides, if yes, begin round, if no, calculate outcomes
			break;
		case CombatState.AfterCombat:
			break;
		}
	}

	void FetchCharacters(){
		enemyCharacters = characterManager.FetchCharacters("Enemies", "enemy");
		// Hacky temporary thing
		playerCharacters = characterManager.FetchCharacters("PlayerCharacters", "player");
	}
}
