using UnityEngine;
using System.Collections;

public class HexCell : MonoBehaviour {

	// Currently public in order to visualise where things are
	public int xCoord;
	public int yCoord;

	// LOC is probably not necessary, but it's here for reference atm
	public float xLoc;
	public float yLoc;

	//Public bools for other scripts to interact with.
	public bool playerSpawn;
	public bool enemySpawn;
	public bool occupied;
	public GameObject occupant;

	public void Setup(int x, int y, float xL, float yL){
		xCoord = x;
		yCoord = y;
		xLoc = xL;
		yLoc = yL;
	}

	public string Coords(){
		return "X: " + xCoord + "| Y: " + yCoord;
	}

	public int Distance (int targetX, int targetY){
		Debug.Log ("XCharLoc = " + targetX + " |YCharLoc = " + targetY);
		Debug.Log ("XDestLoc = " + xCoord + " |YDestLoc = " + yCoord);

		int x = xCoord - targetX;
		int y = yCoord - targetY;
		if (x < 0){
			x = x * -1;
		}
		if (y < 0){
			y = y * -1;
		}
		Debug.Log ("XDistance = " + x + " |YDistance = " + y);
//		int total = x + y;
//		if (total < 0) {
//			total = total * -1;
//		}
//		return total;
		return x + y;
	}
}
