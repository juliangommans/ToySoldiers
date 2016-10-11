using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.


public class GridManager : MonoBehaviour {

	// Using Serializable allows us to embed a class with sub properties in the inspector.
	[System.Serializable]
	public class Count
	{
		public int minimum;             //Minimum value for our Count class.
		public int maximum;             //Maximum value for our Count class.


		//Assignment constructor.
		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}


	public float columns = 7;                                         //Number of columns in our game board.
	public float rows = 5;                                            //Number of rows in our game board.
	public float hDistance = 2.2f;
	public float vDistance = 1.85f;

//	public Count wallCount = new Count (5, 9);                      //Lower and upper limit for our random number of walls per level.

	public GameObject hexCell;
	public List <GameObject> hexGrid;
	public List <GameObject> playerSpawnPoints;
	public List <GameObject> enemySpawnPoints;

	public Sprite[] tileSprites;

//	private GameObject instance;
	private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
	private List <Vector3> gridPositions = new List <Vector3> ();   //A list of possible locations to place tiles.


	//Clears our list gridPositions and prepares it to generate a new board.
	void InitialiseList ()
	{
		//Clear our list gridPositions.
		gridPositions.Clear ();

		//Loop through x axis (columns).
		for(float y = 0; y < rows; y++)
		{
			//Within each column, loop through y axis (rows).
			for(float x = 0; x < columns; x++)
			{
				float xLoc = 0f;
				float yLoc = 0f;
				//At each index add a new Vector3 to our list with the x and y coordinates of that position.
				if (y % 2 == 0) {
					xLoc = x * hDistance - (hDistance * 0.5f);
				} else {
					xLoc = x * hDistance;
				}
				yLoc = y * vDistance;
				gridPositions.Add (new Vector3 (xLoc, yLoc, 0f));
			}
		}
	}


	//Sets up the outer walls and floor (background) of the game board.
	void BoardSetup ()
	{
		//Instantiate Board and set boardHolder to its transform.
		boardHolder = new GameObject ("Board").transform;

		//Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
		for(int y = 0, i = 0; y < rows; y++)
		{
			//Loop along y axis, starting from -1 to place floor or outerwall tiles.
			for(int x = 0; x < columns; x++, i++)
			{
//				//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
//				GameObject toInstantiate = hexCells[Random.Range (0,hexCells.Length)];
//
//				//Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
//				if(x == -1 || x == columns || y == -1 || y == rows)
//					toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];

				Vector3 pos = gridPositions[i];
				GameObject toInstantiate = hexCell;
				//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
				GameObject instance =
					Instantiate (toInstantiate, pos, Quaternion.identity) as GameObject;
				instance.transform.SetParent (boardHolder);
				instance.GetComponent<HexCell> ().Setup (x, y, pos.x, pos.y);
				hexGrid.Add(instance);

				if (x == 0 && y % 2 == 0) {
					instance.GetComponent<HexCell> ().playerSpawn = true;
					playerSpawnPoints.Add (instance);
				}
				if (x == columns - 1) {
					instance.GetComponent<HexCell> ().enemySpawn = true;
					enemySpawnPoints.Add (instance);
				}

				//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.

			}
		}
	}


//	//RandomPosition returns a random position from our list gridPositions.
//	Vector3 RandomPosition ()
//	{
//		//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
//		int randomIndex = Random.Range (0, gridPositions.Count);
//
//		//Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
//		Vector3 randomPosition = gridPositions[randomIndex];
//
//		//Remove the entry at randomIndex from the list so that it can't be re-used.
//		gridPositions.RemoveAt (randomIndex);
//
//		//Return the randomly selected Vector3 position.
//		return randomPosition;
//	}
//
//
//	//LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
//	void LayoutObjectAtRandom (GameObject resource, string name)
//	{
//		int count = 16;
//
//		for(int i = 0; i < count; i++)
//		{
//			Vector3 randomPosition = RandomPosition();
//
//			RaycastHit2D hit = Physics2D.Raycast (new Vector2(randomPosition.x,randomPosition.y),Vector2.zero);
//			if (hit.collider != null) {
//				GroundLogic tile = hit.collider.gameObject.GetComponent<GroundLogic>();
//				tile.resource = name;
//				Debug.Log ("should be 16 of these " + name);
//				Instantiate(resource, randomPosition, Quaternion.identity);
//			}
//
//
//		}
//	}

	Sprite RandomTile (){
		int randomIndex = Random.Range (0, tileSprites.Length-1);
//		Debug.Log ("Randy = " + randomIndex);
		return tileSprites [randomIndex];
	}

	private void LayoutRandomTiles () {
		for (int i = 0; i < hexGrid.Count; i++){
			hexGrid [i].GetComponent<SpriteRenderer> ().sprite = RandomTile ();
			hexGrid [i].AddComponent<PolygonCollider2D> ();
		}
	}

	private void CenterCamera (){
		int midPoint = (int)Mathf.Floor(hexGrid.Count / 2);
		HexCell a = hexGrid [midPoint].GetComponent<HexCell>();
		Debug.Log ("middle xLoc: " + a.xCoord + " yLoc: " + a.yCoord);
		Camera.main.transform.position = new Vector3 (hexGrid [midPoint].transform.position.x, hexGrid [midPoint].transform.position.y, Camera.main.transform.position.z);
	}

	public void SetupScene (string tileset)
	{
		tileSprites = Resources.LoadAll<Sprite>(tileset);
		//Set our list of gridpositions.
		InitialiseList ();
		//Build HexGrid
		BoardSetup ();
		//Load Sprites randomly into the grid.
		LayoutRandomTiles ();
		//Centers the Camera on the middle most tile.
		CenterCamera ();

//		LayoutObjectAtRandom (kauriLoc, "Kauri");

	}
}