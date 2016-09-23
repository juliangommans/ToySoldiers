using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ObjectPlacer : MonoBehaviour {

	public bool legalPlacement = true;
	public GameObject road;
	public GameObject camp;
	public GameObject pa;
	public GameObject trade;
	public GameObject currentStructure;
	public StructureState currentState;

	GameObject currentPlayer;
	GameObject structureImage;

	Text kauriText;
	Text kumaraText;
	Text flaxText;
	Text moaText;
	Text pounamuText;

//	PaLogic paLogic;
//	RoadLogic roadLogic;
//	CampLogic campLogic;

	int startingCamps;
	int startingRoads;

	void Start (){
		currentState = StructureState.EMPTY;
		structureImage = GameObject.Find ("StructureImage");

		kumaraText = GameObject.Find ("KumaraDeduction").GetComponent<Text> ();
		flaxText = GameObject.Find ("FlaxDeduction").GetComponent<Text> ();
		kauriText = GameObject.Find ("KauriDeduction").GetComponent<Text> ();
		moaText = GameObject.Find ("MoaDeduction").GetComponent<Text> ();
		pounamuText = GameObject.Find ("PounamuDeduction").GetComponent<Text> ();

//		paLogic = GameObject.Find ("BuildPa").GetComponent<PaLogic> ();
//		roadLogic = GameObject.Find ("BuildRoad").GetComponent<RoadLogic> ();
//		campLogic = GameObject.Find ("BuildCamp").GetComponent<CampLogic> ();
	}

	void Update () {
//		currentPlayer = GetComponent<RoundManager> ().currentPlayer;
//		Debug.Log ("current player is ==> " + currentPlayer + " ROUND  " + GetComponent<RoundManager> ().roundCount);
		// Confirms the starting resources
		if (currentPlayer != null)
			CheckPlayerResources ();
		// Check what state we're in
		CallStateMachine ();
		// Listen for a click/placement
		if (currentStructure != null)
			PlaceObject ();

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

	void PlaceObject(){
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (hit.collider != null && Input.GetMouseButtonDown(0) && !IsPointerOverUIObject()) {
			
			if (hit.transform.gameObject.layer == 8) {
//				if (currentPlayer.GetComponent<NewPlayer>().StartingPhase()) {
//					if (currentPlayer.GetComponent<NewPlayer>().data.StartingCamps > 0 ) {

//						hit.transform.GetComponent<GroundLogic> ().CampPlacement (currentStructure);
//					} else if (currentPlayer.GetComponent<NewPlayer>().data.StartingRoads > 0){
						
//						hit.transform.GetComponent<GroundLogic> ().ObjectPlacementLogic (currentStructure);
//					}
//				} else {
//					hit.transform.GetComponent<SpriteRenderer> ().color = Color.black;
//					hit.transform.GetComponent<GroundLogic> ().ObjectPlacementLogic (currentStructure);
//				}
			}
		}
	}
		
	void CallStateMachine(){
//		switch (currentState) {
//		case (StructureState.EMPTY):
//			currentStructure = null;
//			EmptyCostsUI ();
//			structureImage.GetComponent<StructureImage> ().SetEmpty (currentPlayer);
//			break;
//		case (StructureState.CAMP):
//			currentStructure = camp;
//			if (!currentPlayer.GetComponent<NewPlayer>().StartingPhase()) {
//				CampCostsUI ();
//			}
//			structureImage.GetComponent<StructureImage> ().SetCamp (currentPlayer);
//			break;
//		case (StructureState.ROAD):
//			currentStructure = road;
//			if (!currentPlayer.GetComponent<NewPlayer>().StartingPhase()) {
//				RoadCostsUI ();
//			}
//			structureImage.GetComponent<StructureImage> ().SetRoad (currentPlayer);
//			break;
//		case (StructureState.PA):
//			currentStructure = pa;
//			if (!currentPlayer.GetComponent<NewPlayer>().StartingPhase()) {
//				PaCostsUI ();
//			}
//			structureImage.GetComponent<StructureImage> ().SetPa (currentPlayer);
//			break;
//		case (StructureState.TRADE):
//			currentStructure = trade;
//			EmptyCostsUI ();
//			break;
//		}
	}
		
	void CheckPlayerResources (){
//		if(currentPlayer.GetComponent<NewPlayer>().data.StartingCamps > 0)
//			currentState = StructureState.CAMP;
//		else if (currentPlayer.GetComponent<NewPlayer>().data.StartingRoads > 0)
//			currentState = StructureState.ROAD;
//		else if (currentPlayer.GetComponent<NewPlayer>().StartingPhase())
//			currentState = StructureState.EMPTY;
	}

	void EmptyCostsUI (){
		kumaraText.text = " ";
		flaxText.text = " ";
		kauriText.text = " ";
		moaText.text = " ";
		pounamuText.text = " ";

	}

	void CampCostsUI (){
//		kumaraText.text = BlankOrNumber (campLogic.cost.KumaraCost);
//		kumaraText.color = CanAffordOrNot (currentPlayer.GetComponent<NewPlayer>().data.Kumara >= campLogic.cost.KumaraCost);
//		flaxText.text = BlankOrNumber (campLogic.cost.FlaxCost);
//		flaxText.color = CanAffordOrNot (currentPlayer.GetComponent<NewPlayer>().data.Flax >= campLogic.cost.FlaxCost);
//		kauriText.text = BlankOrNumber (campLogic.cost.KauriCost);
//		kauriText.color = CanAffordOrNot (currentPlayer.GetComponent<NewPlayer>().data.Kauri >= campLogic.cost.KauriCost);
//		moaText.text = BlankOrNumber (campLogic.cost.MoaCost);
//		moaText.color = CanAffordOrNot (currentPlayer.GetComponent<NewPlayer>().data.Moa >= campLogic.cost.MoaCost);
//		pounamuText.text = BlankOrNumber (campLogic.cost.PounamuCost);
//		pounamuText.color = CanAffordOrNot (currentPlayer.GetComponent<NewPlayer>().data.Pounamu >= campLogic.cost.PounamuCost);

	}

	void RoadCostsUI (){

//		kumaraText.text = BlankOrNumber (roadLogic.cost.KumaraCost);
//		kumaraText.color = CanAffordOrNot (currentPlayer.GetComponent<NewPlayer>().data.Kumara >= roadLogic.cost.KumaraCost);
//		flaxText.text = BlankOrNumber (roadLogic.cost.FlaxCost);
//		flaxText.color = CanAffordOrNot (currentPlayer.GetComponent<NewPlayer>().data.Flax >= roadLogic.cost.FlaxCost);
//		kauriText.text = BlankOrNumber (roadLogic.cost.KauriCost);
//		kauriText.color = CanAffordOrNot (currentPlayer.GetComponent<NewPlayer>().data.Kauri >= roadLogic.cost.KauriCost);
//		moaText.text = BlankOrNumber (roadLogic.cost.MoaCost);
//		moaText.color = CanAffordOrNot (currentPlayer.GetComponent<NewPlayer>().data.Moa >= roadLogic.cost.MoaCost);
//		pounamuText.text = BlankOrNumber (roadLogic.cost.PounamuCost);
//		pounamuText.color = CanAffordOrNot (currentPlayer.GetComponent<NewPlayer>().data.Pounamu >= roadLogic.cost.PounamuCost);

	}

	void PaCostsUI (){
//		kumaraText.text = BlankOrNumber (paLogic.cost.KumaraCost);
//		kumaraText.color = CanAffordOrNot (currentPlayer.GetComponent<NewPlayer>().data.Kumara >= paLogic.cost.KumaraCost);
//		flaxText.text = BlankOrNumber (paLogic.cost.FlaxCost);
//		flaxText.color = CanAffordOrNot (currentPlayer.GetComponent<NewPlayer>().data.Flax >= paLogic.cost.FlaxCost);
//		kauriText.text = BlankOrNumber (paLogic.cost.KauriCost);
//		kauriText.color = CanAffordOrNot (currentPlayer.GetComponent<NewPlayer>().data.Kauri >= paLogic.cost.KauriCost);
//		moaText.text = BlankOrNumber (paLogic.cost.MoaCost);
//		moaText.color = CanAffordOrNot (currentPlayer.GetComponent<NewPlayer>().data.Moa >= paLogic.cost.MoaCost);
//		pounamuText.text = BlankOrNumber (paLogic.cost.PounamuCost);
//		pounamuText.color = CanAffordOrNot (currentPlayer.GetComponent<NewPlayer>().data.Pounamu >= paLogic.cost.PounamuCost);
	}
		
	string BlankOrNumber (int amount){
		if (amount == 0) {
			return " ";
		}else {
			return "-" + amount;
		}
	}

	Color CanAffordOrNot(bool canAfford){
		if (canAfford)
			return Color.green;
		else
			return Color.red;
	}

	public enum StructureState{
		EMPTY,
		ROAD,
		CAMP,
		PA,
		TRADE
	}
}
