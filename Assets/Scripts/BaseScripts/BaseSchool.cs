using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BaseSchool {
	
	private string name;
	private string strongAgainst;
	private float strongPercent;
	private string weakAgainst;
	private float weakPercent;
	public List<BaseSkill> skills;

	public string Name { get; set; }
	public string StrongAgainst { get; set; }
	public float StrongPercent { get; set; }
	public string WeakAgainst { get; set; }
	public float WeakPercent { get; set; }

	public BaseSchool(){
		Name = "School";
		StrongAgainst = "Stat";
		StrongPercent = 1.25f;
		WeakAgainst = "Stat";
		WeakPercent = 0.75f;
	}
}
