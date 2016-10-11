using UnityEngine;
using System.Collections;

public class SkillPopout : MonoBehaviour {

	private GameObject skillPanel;

	void Awake(){
		skillPanel = GameObject.Find("SkillPopoutPanel");
	}

	public void UpdatePanel(){
		skillPanel.SetActive (!skillPanel.active);
	}
}
