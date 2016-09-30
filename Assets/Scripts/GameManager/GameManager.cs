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
	public GameState gameState;
	public CombatManager combatManager;
	public UiManager ui;

	private bool gridSetup;

//	public GameObject Player { get {return player;} }
	void Awake() {
		Instance = this;
		gridSetup = false;
		ui = GetComponent<UiManager> ();
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
				gridSetup = true;
			}
			gameState = GameState.Combat;
			break;
		case GameState.Combat:
			combatManager.CheckCombat ();
			break;

		}

	}


}
