using UnityEngine;

public class Skill {

	/// <summary>
	/// The "Parent" hero.
	/// </summary>
	private GameObject hero;
	public GameObject Hero { get { return hero; } }

	private string name;
	public string Name 
	{
		get { return name; }
		protected set { name = value; }
	}


	/// <summary>
	/// Current skill level
	/// </summary>
	private int level;
	public int Level { get { return level; } }

	/// <summary>
	/// Maximum level of this skill
	/// </summary>
	private int maxLevel;
	public int MaxLevel { get { return maxLevel; } }



	protected Skill(GameObject parentHero, string name, int maxLevel)
	{
		this.hero = parentHero;
		this.name = name;
		this.level = 0;
		this.maxLevel = maxLevel;
	}

	//Don't use this
	protected Skill() : this(null, "", 0) { }


	public virtual bool canBeLeveledUp()
	{
		return level < maxLevel;
	}

	/// <summary>
	/// Levels this skill up if possible
	/// </summary>
	/// <returns><c>true</c>, if leveled up, <c>false</c> otherwise.</returns>
	public virtual bool LevelUp()
	{
		if (this.canBeLeveledUp ()) {
			level++;
			return true;
		}

		return false;
	}

	/// <summary>
	/// Executes skill if possible
	/// </summary>
	/// <returns><c>true</c> if it was executed, <c>false</c> otherwise</returns>
	public virtual bool Execute()
	{
		Debug.LogError ("Skill '" + this.name + "' from hero '" + hero.name + "' not implemented");
		return false;
	}
}
