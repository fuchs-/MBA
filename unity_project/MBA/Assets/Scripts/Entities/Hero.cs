﻿using UnityEngine;

public enum Teams
{
	None,
	Red,
	Blue
}

public class Hero : Entity {

	public Teams team;

	public override EntityTypes getEntityType ()
	{
		return EntityTypes.Hero;
	}

	private HUDData hudData;
	private Sprite charImg;

	//More stuff here: titles, effects

	//--------------------------------------------------Hero stats

	//Basics
	private int level;
	private int experience;
	public bool isMage; //Mages are heroes whose auto-attack is magic


	public int Level { get { return level; } }

	public override int maxHP {
		get {
			return baseHP + (strength * 50);
		}
	}

	private int HPRegen { get { return strength * 10; } }

	public override int maxMP {
		get {
			return baseMP + (intelligence * 50);
		}
	}

	private int MPRegen { get { return intelligence * 10; } }



	//------------------------
	//Primary attributes
	public int strength;
	public int strGain;
	public int dexterity;
	public int dexGain;
	public int intelligence;
	public int intGain;

	public int mainAttribute
	{
		get
		{
			if (isMage)
				return intelligence;
			if (isRanged)
				return dexterity;

			return strength;
		}
	}

	//------------------------
	//Secondary attributes

	protected override int attackDamage {
		get {
			return baseAttack + (mainAttribute * 5);
		}
	}

	public int defense { get { return strength; } }

	public int magicResistance;

	public int dodge { get { return dexterity; } }

	//--------------------------------------------------End of Hero stats

	public override void Initialize()
	{
		base.Initialize ();

		//Getting map coordinates based on position
		this.x = Mathf.FloorToInt(this.transform.position.x);
		this.y = Mathf.FloorToInt(this.transform.position.y);

		GameObject teamHighlight = null;

		if (this.team == Teams.Blue)
			teamHighlight = (GameObject)Instantiate (Resources.Load ("UI/BlueTeamHighlight"), this.transform.position, Quaternion.identity);
		if (this.team == Teams.Red)
			teamHighlight = (GameObject)Instantiate (Resources.Load ("UI/RedTeamHighlight"), this.transform.position, Quaternion.identity);

		if (teamHighlight) {
			teamHighlight.name = "TeamHighlight";
			teamHighlight.transform.parent = this.transform;
		}


		//HUD STUFF
		charImg = Resources.Load<Sprite>("Heroes/" + gameObject.name + "/char");

		hudData = new HUDData (gameObject.name, team);

		updateHUDData ();
	}
		
	private void levelUp()
	{
		level++;

		strength += strGain;
		dexterity += dexGain;
		intelligence += intGain;

		//TODO: make skill levelup available
	}

	public void giveExperience(int exp)
	{
		experience += exp;

		//TODO: verify if hero leveled up
		//I guess I'm gonna have a static dictionary where the key is hero level and the value is the necessary experience
	}

	public override void takeDamage (Damage d)
	{
		//TODO: apply dodge and damage reduction stuff

		base.takeDamage (d);

		updateHUDData ();
	}

	public override void moveTo (Position p, int cost)
	{
		base.moveTo (p, cost);

		updateHUDData ();

	}

	private void updateHUDData()
	{
		hudData.HP = this.HP;
		hudData.maxHP = this.maxHP;
		hudData.MP = this.MP;
		hudData.maxMP = this.maxMP;
		hudData.charImg = this.charImg;
		hudData.currentMS = this.currentMoveSpeed;
		hudData.MS = this.moveSpeed;

		//Just in case this hero is currently selected
		UIController.UI.refreshHUDValues ();
	}

	public HUDData getHUDData()
	{
		return hudData;
	}
}
