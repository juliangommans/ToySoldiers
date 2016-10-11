using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;


public class SkillUiManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public bool longPress;
	private float pressDuration = 0.5f;

	public void OnPointerDown(PointerEventData eventData){
		Invoke ("LongPress", pressDuration);
	}

	public void OnPointerUp(PointerEventData eventData){
		ShortPress ();
	}

	public void OnPointerExit(PointerEventData eventData){
		ShortPress ();
	}

	private void ShortPress(){
		if (longPress) {
			longPress = false;
		} else {
			CancelInvoke ("LongPress");
			Debug.Log ("shortPress");
			this.GetComponent<SkillManager> ().SelectSkill ();
		}
	}

	private void LongPress(){
		longPress = true;
		Debug.Log ("we have a long press");
		this.gameObject.GetComponent<SkillManager> ().DisplayInfo ();
	}

}
