using UnityEngine;
using System.Collections;

public class BaseSkillEffects {

	private BaseObjectInformation information;

	public BaseSkillEffects(BaseObjectInformation setInfo ) {
		information = setInfo;
	}
		
	public virtual void PerformEffect(){
		Debug.LogWarning ("NO SKILL EFFECT ADDED");
	}

	public BaseObjectInformation Information{ get{ return information; } }

}
