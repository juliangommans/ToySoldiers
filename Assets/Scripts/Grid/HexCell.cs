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

	public void Setup(int x, int y, float xL, float yL){
		xCoord = x;
		yCoord = y;
		xLoc = xL;
		yLoc = yL;
	}

	public string Coords(){
		return "X: " + xCoord + "| Y: " + yCoord;
	}


}
