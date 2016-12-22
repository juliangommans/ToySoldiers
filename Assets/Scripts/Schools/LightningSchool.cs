using UnityEngine;
using System.Collections;

public class LightningSchool : BaseSchool {

	private const string name = "Lightning";
	private const string description = "Lightning is a element most associated with high energy, high speed and something.";

	public LightningSchool()
		:base(new BaseObjectInformation(name, description)){
		this.StrongAgainst = "Fire";
		this.WeakAgainst = "Ice";
	}
}
