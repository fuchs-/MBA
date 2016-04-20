using UnityEngine;

/// <summary>
/// This class represents the data for the HUD to display from a Hero
/// </summary>
public class HUDData {

	public string name { get; private set; }

	public Sprite charImg;

	public Teams team { get; private set; }

	public int HP;
	public int maxHP;
	public int MP;
	public int maxMP;

	public bool isEmpty { get; private set; }

	public HUDData(string name, Teams team)
	{
		this.name = name;
		this.team = team;

		isEmpty = false;
	}

	public HUDData() : this("No Hero Selected", Teams.None) 
	{
		isEmpty = true;
	}
}
