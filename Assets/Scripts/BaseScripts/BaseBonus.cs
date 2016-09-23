using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseBonus : BaseSkillEffects {
	
	private float multiplier;

	public float Multiplier { get; set; }

	private List<BaseSkillEffects> effects;

	public List<BaseSkillEffects> Effects { get; set;}

	public BaseBonus(string name, string description)
		:base(new BaseObjectInformation(name,description)){
		multiplier = 1.5f;
		this.Effects = new List<BaseSkillEffects> ();
	}

	public string DescribeBonus() {
		int count = 0;
		string description = "When activated, this consumes all of your combo points and your skill be " + this.multiplier * 100 + "% more powerful.";
		string effects = " It will also have the following bonuses: (";
		foreach (BaseSkillEffects skillEffect in Effects) {
			count += 1;
			effects += skillEffect.Information.Name;
			if (count < Effects.Count) {
				effects += ", ";
			}
		}
		return description + effects + ")";
	}
		
}
