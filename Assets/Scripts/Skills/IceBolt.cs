using UnityEngine;
using System.Collections;

public class IceBolt : BaseSkill {

	private const string name = "Ice Bolt";
	private const string description = "A ranged ethereal spell that deals cold damage";

	public IceBolt()
		:base(){
		this.TargetEnemy = true;
		this.Ranged = true;
		this.Ethereal = true;
		this.School = new IceSchool ();
		this.Information = new BaseObjectInformation (name, description);
	}

}
