using UnityEngine;
using System.Collections;

public class CombatControlsManager : MonoBehaviour {

	private bool targetSelected;
	private GameObject target;
	private GameObject previousTarget;
	public GameObject hexSelectorMask;
//	private GameObject user;
//
//	vo

	void Update () {
		if (targetSelected) {
//			GetComponent<GameManager> ().Player.GetComponent<BaseCharacter> ().CurrentTarget = target;
			targetSelected = false;
		}
		SelectTarget ();
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

	private GameObject CheckForTarget(){
		//Converting Mouse Pos to 2D (vector2) World Pos
		Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		RaycastHit2D hit=Physics2D.Raycast(rayPos, Vector2.zero, 0f);

		if (hit)
		{
			Debug.Log("YOU SELECTED " + hit.transform.name + " at coord: " + hit.transform.GetComponent<HexCell>().Coords());
			return hit.transform.gameObject;
		}
		else return null;
	}

	private void EvaluateTarget(){
		switch (target.tag){
		case "HexCell":
			if (true) {
				hexSelectorMask.SetActive (true);
				if (target.GetComponent<HexCell> ().Coords () == previousTarget.GetComponent<HexCell> ().Coords ()) {
					
				} else {
					previousTarget = target;
					hexSelectorMask.transform.position = target.transform.position;
				}
			} else {
				
			}
			// Do some other things, like calculate ability to move there
			break;
		}
	}
}
