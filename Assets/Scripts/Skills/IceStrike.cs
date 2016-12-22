using UnityEngine;
using System.Collections;

public class IceStrike : BaseSkill {

	private const string name = "Ice Strike";
	private const string description = "A melee physical attack that deals cold damage";

	public IceStrike()
		:base(){
		this.TargetEnemy = true;
		this.Melee = true;
		this.Corporeal = true;
		this.School = new IceSchool();
		this.Information = new BaseObjectInformation (name, description);
	}
}
