﻿using UnityEngine;
using System.Collections;

public class IceBolt : BaseSkill {

	private const string name = "Ice Bolt";
	private const string description = "A ranged ethereal spell that deals cold damage";

	public IceBolt()
		:base(new BaseObjectInformation(name, description)){
		this.TargetEnemy = true;
		this.Ranged = true;
		this.Ethereal = true;
	}

}
