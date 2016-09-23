using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CharacterManager : MonoBehaviour {

	private List<GameObject> characters = new List<GameObject> ();
	private GameObject[] characterArray;

	public List<GameObject> FetchCharacters(string resourceLocation, string type){
		InstantiateCharacters (resourceLocation, type);
		return characters;
	}
		
	private void InstantiateCharacters(string resourceLocation, string type){
// in future we need logic to add the types of enemies we want to the list
		characterArray = Resources.LoadAll<GameObject>("Prefabs/" + resourceLocation);
			
		if (characterArray != null) {
			for(int i = 0; i < characterArray.Length; i++){
				GameObject instance = (GameObject)Instantiate(characterArray[i]);
				GridManager grid = FindObjectOfType<GridManager> ();
				if (type == "enemy") {
					for(int x=0;x<grid.enemySpawnPoints.Count;x++){
						if (!grid.enemySpawnPoints[i].GetComponent<HexCell>().occupied){
							instance.transform.position = grid.enemySpawnPoints[i].transform.position;
						}
					}
				} else {
					for(int x=0;x<grid.playerSpawnPoints.Count;x++){
						if (!grid.playerSpawnPoints[i].GetComponent<HexCell>().occupied){
							instance.transform.position = grid.playerSpawnPoints[i].transform.position;
						}
					}
				}
				characters.Add (instance);
			}
		} else {
			Debug.Log("Prefabbing didn't work");
		}
	}


}
