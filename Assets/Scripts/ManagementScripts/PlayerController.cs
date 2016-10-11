using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	// Basically a store for the players stuff
	public List<GameObject> characters;
	public GameObject currentCharacter;
	public GameObject currentTarget;
	public BaseSkill currentlySelectedSkill;

}
