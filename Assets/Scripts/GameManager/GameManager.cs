using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public static GameManager Instance;

	public enum GameState
	{
		Paused,
		World,
		SetupBattlefield,
		Combat,
		Town
	}
		
//	private GameObject player;
	private GameState gameState;
	private CombatManager combatManager;
	private bool gridSetup;

//	public GameObject Player { get {return player;} }
	public string Test = "testing";

	void Awake() {
		Instance = this;
//		player = (GameObject)Instantiate(Resources.Load("prefabs/Player"));
		gameState = GameState.Paused;
		// ATM this is for testing - TODO change to when someone enters a zone.

	}

	void Update () {

		switch(gameState) 
		{
		case GameState.Paused:
			if (true) { // Testing purposes - SHOULD give options here.
//				player.GetComponent<Player>().BuildPlayer();
				combatManager = new CombatManager();
				combatManager.StartCombat();
				gameState = GameState.SetupBattlefield;
			}
			break;
		case GameState.SetupBattlefield:
			if (!gridSetup) {
				this.GetComponent<GridManager> ().SetupScene ("HexTiles/GrassTiles");
				gridSetup = false;
			}
			gameState = GameState.Combat;
			break;
		case GameState.Combat:
			combatManager.CheckCombat ();
			break;

		}

	}


}
