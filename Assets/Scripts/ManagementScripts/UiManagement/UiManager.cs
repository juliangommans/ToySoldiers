using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiManager : MonoBehaviour {

	public GameObject userCharacter;
	public GameObject target;
	public GameObject userUi;
	public GameObject targetUi;
	public GameObject buttonPrefab;
	private BaseSkill currentSkill;

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
		BuildSkillButtons ();
	}
		
	private void ChangeUiDisplayDetails(GameObject ui, GameObject character){
		GameObject targetInfo = ui.transform.FindChild("CharacterInfo").gameObject;
		GameObject targetNameText = targetInfo.transform.FindChild ("CharacterName").gameObject;
		targetNameText.GetComponent<Text> ().text = character.GetComponent<BaseCharacter> ().CharacterName;
		// also update icon here.
		GameObject stats = targetInfo.transform.FindChild ("Stats").gameObject;
		FetchAllStats (stats, character);
	}

	private void FetchAllStats (GameObject statsUi, GameObject character){
		BaseStats charStats = character.GetComponent<BaseCharacter> ().Stats;
		BuildStatString (statsUi, "Shield", charStats.Shield, charStats.BaseShield);
		BuildStatString (statsUi, "Health", charStats.Health, charStats.MaxHealth);
		BuildStatString (statsUi, "Attack", charStats.Attack, charStats.BaseAttack);
		BuildStatString (statsUi, "Defense", charStats.Defense, charStats.BaseDefense);
		BuildStatString (statsUi, "Power", charStats.Power, charStats.BasePower);
		BuildStatString (statsUi, "Resistance", charStats.Resistance, charStats.BaseResistance);
		BuildStatString (statsUi, "Speed", charStats.Speed, charStats.BaseSpeed);
	}

	private void BuildStatString(GameObject statsUi, string stat, int currentStat, int maxStat){
		GameObject newStat = statsUi.transform.FindChild (stat).gameObject;
		string color;
		string str = "<b>" + stat + ":</b> ";
		if (currentStat >= maxStat) {
			color = "#00ff00ff";
		} else {
			color = "#ff0000ff";
		}
		str += "<color=" + color + ">" + currentStat + "</color>/";
		str += maxStat;
		newStat.GetComponent<Text> ().text = str;

	}

	private void BuildSkillButtons(){
		// builds butotns at run time
		int buttonCount = userCharacter.GetComponent<BaseCharacter> ().Skills.Count;
		GameObject parentPanel = GameObject.Find("SkillPopoutPanel");
			for (int i = 0; i < buttonCount; i++) {
			GameObject skillButton = (GameObject)Instantiate (buttonPrefab);
			GameObject buttonSlot = GameObject.Find("SkillSlot" + i);
			BaseSkill currentSkill = userCharacter.GetComponent<BaseCharacter> ().Skills [i];
			skillButton.transform.SetParent(parentPanel.transform, false);
			skillButton.transform.position = buttonSlot.transform.position;
			skillButton.transform.GetChild (0).GetComponent<Text> ().text = currentSkill.Information.Name;
			skillButton.GetComponent<SkillManager> ().SkillSetup (userCharacter, currentSkill);
		}
	}
}
