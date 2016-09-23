using UnityEngine;
using System.Collections;

public class IceStrike : BaseSkill {

	private const string name = "Ice Strike";
	private const string description = "A melee physical attack that deals cold damage";

	public IceStrike()
		:base(new BaseObjectInformation(name, description)){
		this.TargetOther = true;
		this.Melee = true;
		this.Corporeal = true;
	}
}
