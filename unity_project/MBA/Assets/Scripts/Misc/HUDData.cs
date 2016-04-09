using UnityEngine;

/// <summary>
/// This class represents the data for the HUD to display from a Hero
/// </summary>
public class HUDData {

	private string name;
	public string Name { get { return name; } }

	public Sprite charImg;

	public int HP;
	public int maxHP;
	public int MP;
	public int maxMP;

	public bool isEmpty { get; private set; }

	public HUDData(string name)
	{
		this.name = name;

		isEmpty = false;
	}

	public HUDData() : this("No Hero Selected") 
	{
		isEmpty = true;
	}
}
