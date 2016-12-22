using UnityEngine;
using System.Collections;

public class FireSchool : BaseSchool {

	private const string name = "Fire";
	private const string description = "Fire is a element most associated with warm temperatures, destruction and rebirth.";

	public FireSchool()
		:base(new BaseObjectInformation(name, description)){
		this.StrongAgainst = "Ice";
		this.WeakAgainst = "Lightning";
	}
}
