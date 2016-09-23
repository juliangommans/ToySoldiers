using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GroundLogic : MonoBehaviour {

	public string resource;
	public int multiplier;

	public float xCoord;
	public float yCoord;
	float angleBetween;

	public GameObject tradePanel;
	public GameObject owner;
	public bool occupied = false;
	public bool hasPa = false;
	bool oneStructure = false;
	bool extendedRoad = false;
	float midX;
	float midY;
	bool popOver;

	public GameObject[] roads = new GameObject[6];
	GameObject currentStructure;
	GameObject currentPlayer;

	GameObject gameManager;
	GameObject roadButton;
	GameObject campButton;
	GameObject paButton;

	public GameObject placedStructure;

	Text playerMessage;


	int[][] evenSurroundingGrids = {
		new int[]{ -1, 1 },
		new int[]{ 0, 1 },
		new int[]{ -1, 0 },
		new int[]{ 1, 0 },
		new int[]{ -1, -1 },
		new int[]{ 0, -1 }
	};

	int[][] oddSurroundingGrids = {
		new int[]{ 1, 1 },
		new int[]{ 0, 1 },
		new int[]{ -1, 0 },
		new int[]{ 1, 0 },
		new int[]{ 1, -1 },
		new int[]{ 0, -1 }
	};

	GameObject[] allGrids;

	void Start(){
		gameManager = GameObject.Find ("GameManager");
		roadButton = GameObject.Find ("BuildRoad");
		campButton = GameObject.Find ("BuildCamp");
		paButton = GameObject.Find ("BuildPa");
		playerMessage = GameObject.Find ("PlayerMessage").GetComponent<Text> ();
	}

	public void SetCoords(float x, float y){
		xCoord = x;
		yCoord = y;
	}

	void OnMouseOver(){
//		if (this.occupied && !currentPlayer.GetComponent<NewPlayer>().StartingPhase()){
//			playerMessage.text = "This tile generates " + this.multiplier + " times " + this.resource + " and Kumara a turn";
//			playerMessage.color = this.owner.GetComponent<NewPlayer> ().data.PlayerColor;
//			popOver = true;
//		}
	}

	void OnMouseExit(){
		if (popOver) {
			playerMessage.text = "";
			playerMessage.color = Color.white;
			popOver = false;
		}

	}

	void Update(){
		allGrids = GameObject.FindGameObjectsWithTag ("Ground");
//		currentPlayer = gameManager.GetComponent<RoundManager> ().currentPlayer;
	}

	public void ObjectPlacementLogic(GameObject currentStructure){
		if (currentStructure.name == "TradeObject") {
			// Trade logic
			int roadCount = 0;
			foreach (GameObject road in roads) {
				if (road != null) {
//					if (road.GetComponent<Structure> ().Owner == currentPlayer)
//						roadCount += 1;
				}
			}
			if (roadCount > 0) {
//				GameObject.Find ("TradeButton").GetComponent<TradeLogic> ().SetupTrade (this.resource, roadCount);
			}

	} else {
			// Placing structure logic
			FindAllTheGrids(currentStructure);
		}
	}

	public void FindAllTheGrids(GameObject currentStructure){
		foreach (GameObject grid in allGrids){
			if (grid.GetComponent<GroundLogic> ().yCoord % 2 == 0) {
				foreach (int[] loc in evenSurroundingGrids) {
					OccupiedLogic (grid, loc, currentStructure);
				}
			} else {
				foreach (int[] loc in oddSurroundingGrids) {
					OccupiedLogic (grid, loc, currentStructure);
				}
			}
		}
		oneStructure = false;
	}

	void OccupiedLogic(GameObject grid, int[] loc, GameObject currentStructure){
		// This is ignoring ground objects not touching this tile (ground tile clicked on)
		if ((xCoord + loc [0] == grid.GetComponent<GroundLogic> ().xCoord) && (yCoord + loc [1] == grid.GetComponent<GroundLogic> ().yCoord)) {
			if (currentStructure.tag == "Road") {
				RoadPlacementLogic (grid, currentStructure);
			} else if (currentStructure.tag == "Camp") {
				CampPlacementLogic (grid, currentStructure);
			} else if (currentStructure.tag == "Pa") {
				PaPlacementLogic (currentStructure);
			}
		}
	}


	void CampPlacementLogic(GameObject grid, GameObject currentStructure){
		// Grid = current grid from surrounding grids

//			if (currentPlayer.GetComponent<NewPlayer>().data.StartingCamps > 0) {
//				CampPlacement (currentStructure);
//			} else {
//				// Checks for a road to build a camp off of
//				foreach (GameObject road in roads) {
//					if (road != null){
//						if (road.GetComponent<Structure> ().Owner == currentPlayer) {
//							CampPlacement (currentStructure);
//							oneStructure = true;
//							break;
//						}
//					}
//				}
//			}

	}

	void RoadPlacementLogic (GameObject grid, GameObject currentStructure){

		foreach (GameObject road in grid.GetComponent<GroundLogic> ().roads) {
			if (road != null){
//				if (road.GetComponent<Structure> ().Owner == currentPlayer) {
//					extendedRoad = true;
//				}
			}
		}

		// This bit of logic is checking for overlapping roads, and skipping placement if one exists at midX,midY
		midX = (grid.transform.position.x + this.transform.position.x) / 2;
		midY = (grid.transform.position.y + this.transform.position.y) / 2;
		RaycastHit2D hit = Physics2D.Raycast (new Vector2(midX,midY),Vector2.zero);

		if (grid.GetComponent<GroundLogic> ().owner == currentPlayer && !this.occupied && hit.collider == null) {
			RoadPlacementStepTwo (grid, currentStructure);
		} else if (extendedRoad && hit.collider == null && !this.occupied) {
			RoadPlacementStepTwo (grid, currentStructure);
		}
		extendedRoad = false;
	}

	void RoadPlacementStepTwo(GameObject grid, GameObject currentStructure){
		RoadPlacement (grid, currentStructure);
		oneStructure = true;
		return;
	}

	void PaPlacementLogic (GameObject currentStructure){
		if (!oneStructure) {
			if (this.owner == currentPlayer && !this.hasPa) {
//				if (paButton.GetComponent<PaLogic> ().cost.CanAfford (currentPlayer)) {
//					PaPlacement (currentStructure);
//					oneStructure = true;
//				} else {
//					playerMessage.text = "I'm sorry but you can't afford this Pa!";
//					playerMessage.color = Color.red;
//				}
			} else {
				playerMessage.text = "You cannot place a Pa here, you need to place on a camp you own.";
				playerMessage.color = Color.red;
			}
		}
	}

	public void RoadPlacement (GameObject grid, GameObject currentStructure){
		
		if (!oneStructure) {

			if (grid.transform.position.y > this.transform.position.y || grid.transform.position.y < this.transform.position.y) {
				angleBetween = 90f;
			} else {
				angleBetween = 0f;
			}

//			if (currentPlayer.GetComponent<NewPlayer>().data.StartingRoads > 0 || roadButton.GetComponent<RoadLogic> ().cost.CanAfford (currentPlayer)) {
//				GameObject roadClone = Instantiate (currentStructure, transform.position, transform.rotation) as GameObject;
//				gameManager.GetComponent<GameManager> ().SetupUndo (roadClone, currentPlayer, this.gameObject);
//
//				roadClone.gameObject.transform.position = new Vector3 (midX, midY, 0);
//				roadClone.gameObject.transform.Rotate (new Vector3 (0, 0, angleBetween));
//				currentPlayer.GetComponent<NewPlayer> ().data.RoadPoints += 1;
//				if (currentPlayer.GetComponent<NewPlayer>().data.StartingRoads > 0) {
//					currentPlayer.GetComponent<NewPlayer>().data.StartingRoads -= 1;
//				} else {
//					// Takes Away Resources
//					roadButton.GetComponent<RoadLogic> ().cost.DeductResources (currentPlayer);
//				}
//
//				// Logic for adding a road to an array
//				int count = 0;
//				foreach (GameObject road in roads) {
//					if (road == null) {
//						roads [count] = roadClone;
//						break;
//					}
//					count++;
//				}
		} else {
				playerMessage.text = "I'm sorry but you can't afford this Road!";
				playerMessage.color = Color.red;
//			}
		}
	}

	public void CampPlacement(GameObject currentStructure){

		if (!this.occupied) {
			if (!oneStructure) {
//				if (currentPlayer.GetComponent<NewPlayer>().data.StartingCamps > 0 || campButton.GetComponent<CampLogic> ().cost.CanAfford (currentPlayer)) {
//					GameObject campClone = Instantiate (currentStructure, this.transform.position, this.transform.rotation) as GameObject;
//					gameManager.GetComponent<GameManager> ().SetupUndo (campClone, currentPlayer, this.gameObject);
//
//					this.occupied = true;
//					this.owner = currentPlayer;
//					this.multiplier = campButton.GetComponent<CampLogic> ().cost.Multiplier;
//					currentPlayer.GetComponent<NewPlayer> ().data.LandPoints += campButton.GetComponent<CampLogic> ().cost.Multiplier;
//					this.placedStructure = campClone;
//					if (currentPlayer.GetComponent<NewPlayer>().data.StartingCamps > 0) {
//						currentPlayer.GetComponent<NewPlayer>().data.StartingCamps -= 1;
//					} else {
//						campButton.GetComponent<CampLogic> ().cost.DeductResources (currentPlayer);
//					}
//				} else {
//					playerMessage.text = "I'm sorry but you can't afford this Camp!";
//					playerMessage.color = Color.red;
//				}
			}
		}
	}

	public void PaPlacement(GameObject currentStructure){
		GameObject paClone = Instantiate (currentStructure, this.transform.position, this.transform.rotation) as GameObject;
//		gameManager.GetComponent<GameManager> ().SetupUndo (paClone, currentPlayer, this.gameObject);

		Destroy (this.placedStructure);
		this.hasPa = true;
		this.placedStructure = paClone;
//		this.multiplier = paButton.GetComponent<PaLogic> ().cost.Multiplier;
//		currentPlayer.GetComponent<NewPlayer>().data.LandPoints += paButton.GetComponent<PaLogic> ().cost.Multiplier - campButton.GetComponent<CampLogic> ().cost.Multiplier;
//		paButton.GetComponent<PaLogic> ().cost.DeductResources (currentPlayer);
//

	}


		
}
