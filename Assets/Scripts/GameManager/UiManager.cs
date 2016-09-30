using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiManager : MonoBehaviour {

	public GameObject userCharacter;
	public GameObject target;
	public GameObject userUi;
	public GameObject targetUi;

	void Awake(){
		targetUi = GameObject.Find ("TargetUiPanel");
		userUi =  GameObject.Find("UserUiPanel");
	}

	public void ChangeTargetUi(GameObject t){
		target = t;
		ChangeUiDisplayDetails (targetUi, t);
	}

	public void ChangeUserUi(GameObject u){
		userCharacter = u;
		ChangeUiDisplayDetails (userUi, u);
	}

	private void ChangeUiDisplayDetails(GameObject ui, GameObject character){
		GameObject targetText = targetUi.transform.FindChild("CharacterInfo").transform.FindChild("CharacterName").gameObject;
		targetText.GetComponent<Text> ().text = character.GetComponent<BaseCharacter> ().CharacterName;
		// also update stats and icon here.
	}
}
