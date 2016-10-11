using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class CombatControlsManager : MonoBehaviour {

	private int moveDistance;

	private bool targetSelected;
	// what the user has clicked on
	private GameObject target;
	// the character target (if one is selected or on a selected cell)
	private GameObject selectedCharacter;
	private GameObject previousSelectedCharacter;
	// the selected cell, irrespective of occupants
	private GameObject selectedCell;
	private GameObject previousCell;

	// Tile Selection
	public GameObject hexSelectorMask;
	private Color friendlyColor = new Color (0f,1f,1f,1f);
	private Color enemyColor = new Color (1f,0f,1f,1f);
	private Color neutralColor = new Color (0.5f,1f,0.5f,1f);

	private GameManager gm;
	private GameObject currentCharacter;

	void Awake () {
		gm = this.GetComponent<GameManager> ();
	}

	void Update () {
		if (targetSelected) {
//			GetComponent<GameManager> ().Player.GetComponent<BaseCharacter> ().CurrentTarget = target;
			targetSelected = false;
		}
		SelectTarget ();
		GetCurrentCharacter ();
	}

	void SelectTarget (){
		if (Input.GetMouseButtonDown (0)) {
			target = CheckForTarget ();
			if (target != null) {
				targetSelected = true;
				EvaluateTarget ();
			}
		}
	}

	private bool IsPointerOverUIObject() {
		// Referencing this code for GraphicRaycaster https://gist.github.com/stramit/ead7ca1f432f3c0f181f
		// the ray cast appears to require only eventData.position.
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}

	private GameObject CheckForTarget(){
		//Converting Mouse Pos to 2D (vector2) World Pos
		Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		RaycastHit2D hit=Physics2D.Raycast(rayPos, Vector2.zero, 0f);

		if (hit && !IsPointerOverUIObject())
		{
//			Debug.Log("YOU SELECTED " + hit.transform.name + " at coord: " + hit.transform.GetComponent<HexCell>().Coords());
			return hit.transform.gameObject;
		}
		else return null;
	}

	private void EvaluateTarget(){
		hexSelectorMask.SetActive (true);
		switch (target.tag){
		case "HexCell":
			// Checks for occupation
			if (!target.GetComponent<HexCell> ().occupied) {
				// If unoccupied sets selected cell as this target and selector color as lightblue
				hexSelectorMask.GetComponent<SpriteRenderer> ().color = neutralColor;
				selectedCell = target;
				if (previousCell == null || selectedCell.GetComponent<HexCell> ().Coords () != previousCell.GetComponent<HexCell> ().Coords ()) {
					// If previous cell is not the same as this one, moves marker and changes previous target
					previousCell = target;
					hexSelectorMask.transform.position = selectedCell.transform.position;
				} else {
					// If its the same cell as before will attempt to move character
					if (currentCharacter != null && currentCharacter.GetComponent<BaseCharacter> ().Controller == "player") {
						moveDistance = selectedCell.GetComponent<HexCell> ().Distance (currentCharacter.GetComponent<BaseCharacter> ().Location.GetComponent<HexCell> ().xCoord, currentCharacter.GetComponent<BaseCharacter> ().Location.GetComponent<HexCell> ().yCoord);
						if (currentCharacter.GetComponent<BaseCharacter> ().IsLegalMove (moveDistance)) {
							currentCharacter.GetComponent<BaseCharacter> ().MoveCharacter (selectedCell);
						} else {
							Debug.Log ("can't move sorry - not enough AP");
						}
					} else {
						Debug.Log ("Player is not current character " + currentCharacter);
					}
				}
			} else {
				// check for cells occupants.
				if (target.GetComponent<HexCell> ().occupant.tag == "Enemy" || target.GetComponent<HexCell> ().occupant.tag == "Friendly") {
					SetTargetCharacter (target.GetComponent<HexCell> ().occupant);		
				}
			}
			break;
		case "Enemy":
			SetTargetCharacter (target);
			// Do some logic with selecting a target now.
			break;
		case "Friendly":
			SetTargetCharacter (target);
			break;
		}
	}

	void SetTargetCharacter (GameObject o){
		if (o != null) {
			// Handles the hex mask changes
			selectedCell = o.GetComponent<BaseCharacter>().Location;
			previousCell = selectedCell;
			hexSelectorMask.transform.position = selectedCell.transform.position;
			if (o.tag == "Enemy"){
				hexSelectorMask.GetComponent<SpriteRenderer> ().color = enemyColor;
			} else{
				hexSelectorMask.GetComponent<SpriteRenderer> ().color = friendlyColor;
			}
			// Handles the character changes
			selectedCharacter = o;
			if (previousSelectedCharacter == null || o.GetComponent<BaseCharacter>().Location.GetComponent<HexCell> ().Coords () != previousSelectedCharacter.GetComponent<BaseCharacter>().Location.GetComponent<HexCell> ().Coords ()) {
				previousSelectedCharacter = o;
				currentCharacter.GetComponent<BaseCharacter> ().CurrentTarget = o;
				gm.combatManager.turnManager.currentTarget = o;
				gm.ui.ChangeTargetUi (o);
				Debug.Log ("target = " + o.name);
			} else {
				Debug.Log ("you clicked the same fullah twice bro");
			}
		} else {
			Debug.Log ("you've selected an enemy tile, but for some reason it's null...");
		}
	}
		
	void GetCurrentCharacter (){
		if (gm.gameState == GameManager.GameState.Combat) {
			currentCharacter = gm.combatManager.turnManager.currentCharacter;
		}
	}
}
