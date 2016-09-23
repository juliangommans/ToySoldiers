using UnityEngine;
using System.Collections;

public class BaseObjectInformation {

	private string name;
	private string description;
	private Sprite icon;

	public string Name { get { return name; } }
	public string Description { get { return description; } }
	public Sprite Icon { get { return icon; } }

	public BaseObjectInformation(string newName){
		name = newName;
	}
	public BaseObjectInformation(string newName, string newDescription){
		name = newName;
		description = newDescription;
	}
	public BaseObjectInformation(string newName, string newDescription, Sprite newIcon){
		name = newName;
		description = newDescription;
		icon = newIcon;
	}
}
