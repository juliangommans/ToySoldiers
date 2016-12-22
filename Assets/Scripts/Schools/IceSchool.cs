using UnityEngine;
using System.Collections;

public class IceSchool : BaseSchool {

	private const string name = "Ice";
	private const string description = "Ice is a element most associated with cold temperatures, liquids and the source of life.";

	public IceSchool()
		:base(new BaseObjectInformation(name, description)){
		this.StrongAgainst = "Lightning";
		this.WeakAgainst = "Fire";
	}
}
