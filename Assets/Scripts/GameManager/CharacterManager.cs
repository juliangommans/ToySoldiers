using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CharacterManager : MonoBehaviour {

	private List<GameObject> characters;
	private GameObject[] characterArray;

	public List<GameObject> FetchCharacters(string resourceLocation, string type){
		characters = new List<GameObject> ();
		InstantiateCharacters (resourceLocation, type);
		return characters;
	}
		
	private void InstantiateCharacters(string resourceLocation, string type){
// in future we need logic to add the types of enemies we want to the list
		characterArray = Resources.LoadAll<GameObject>("Prefabs/" + resourceLocation);
//		Debug.Log ("array = " + characterArray + "| Length: " + characterArray.Length);
//		Debug.Log ("location? " + resourceLocation);
			
		if (characterArray != null) {
			for(int i = 0; i < characterArray.Length; i++){
				GameObject instance = (GameObject)Instantiate(characterArray[i]);
				GridManager grid = FindObjectOfType<GridManager> ();
				if (type == "enemy") {
					for(int x=0;x<grid.enemySpawnPoints.Count;x++){
						if (!grid.enemySpawnPoints[i].GetComponent<HexCell>().occupied){
							SetupCharacter (instance, grid.enemySpawnPoints [i], "computer");
						}
					}
				} else {
					for(int x=0;x<grid.playerSpawnPoints.Count;x++){
						if (!grid.playerSpawnPoints[i].GetComponent<HexCell>().occupied){
							SetupCharacter (instance, grid.playerSpawnPoints [i], "player");
						}
					}
				}
				instance.GetComponent<BaseCharacter> ().Stats.ResetStats();
				characters.Add (instance);
			}
		} else {
			Debug.Log("Couldn't find/return prefab array: Prefabs/" + resourceLocation);
		}
	}

	private void SetupCharacter(GameObject character, GameObject spawnPoint, string controller){
		character.transform.position = spawnPoint.transform.position;
		character.GetComponent<BaseCharacter> ().Location = spawnPoint;
		character.GetComponent<BaseCharacter> ().Controller = controller;
		spawnPoint.GetComponent<HexCell> ().occupied = true;
		spawnPoint.GetComponent<HexCell> ().occupant = character;
	}

}
