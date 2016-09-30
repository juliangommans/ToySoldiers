using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class CombatControlsManager : MonoBehaviour {

	private int moveDistance;

	private bool targetSelected;

	private GameObject target;
	private GameObject previousTarget;
	public GameObject hexSelectorMask;

	private GameManager gm;
	private GameObject currentCharacter;
	private GameObject selectedCharacter;

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
		switch (target.tag){
		case "HexCell":
			if (!target.GetComponent<HexCell> ().occupied) {
				hexSelectorMask.SetActive (true);
				if (previousTarget == null || target.GetComponent<HexCell> ().Coords () != previousTarget.GetComponent<HexCell> ().Coords ()){
					previousTarget = target;
					hexSelectorMask.transform.position = target.transform.position;
				} else {
					if (currentCharacter != null && currentCharacter.GetComponent<BaseCharacter>().Controller == "player") {
						moveDistance = target.GetComponent<HexCell> ().Distance (currentCharacter.GetComponent<BaseCharacter>().Location.GetComponent<HexCell>().xCoord, currentCharacter.GetComponent<BaseCharacter>().Location.GetComponent<HexCell>().yCoord);
						if (currentCharacter.GetComponent<BaseCharacter> ().IsLegalMove (moveDistance)) {
							currentCharacter.GetComponent<BaseCharacter> ().MoveCharacter (target);
						} else {
							Debug.Log ("can't move sorry - not enough AP");
						}
					} else {
						Debug.Log ("Player is not current character " + currentCharacter);
					}
				}
			} else {
				SetTarget (target.GetComponent<HexCell> ().occupant);				
			}
			break;
		case "Enemy":
			SetTarget (target);
			// Do some logic with selecting a target now.
			break;
		}

	}

	void SetTarget (GameObject o){
		if (o != null) {
			gm.combatManager.turnManager.currentTarget = o;
			gm.ui.ChangeTargetUi (o);
			Debug.Log ("target = " + o.name);
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
