using UnityEngine;
using System;

public enum EntityTypes
{
	Entity,
	Hero
}

/// <summary>
/// Any Entity in the battlefield
/// Hero inherits from this
/// </summary>
public class Entity : MonoBehaviour {
	
	//Entity coordinates
	public int x { get; protected set; }
	public int y { get; protected set; }
	public Position position { get { return new Position (x, y); } }

	Action<Entity> moving;

	public virtual EntityTypes getEntityType() { return EntityTypes.Entity; }

	//--------------------------------------------------Entity stats

	//Basics

	private int hp;
	private int mp;

	public bool canAttack { get { return attackRange > 0; } }
	public bool isMelee { get { return attackRange == 1; } }
	public bool isRanged { get { return attackRange > 1; } }

	//------------------------

	public int HP { get { return hp; } }
	public virtual int maxHP { get { return  baseHP; } }

	public int MP { get { return mp; } }
	public virtual int maxMP { get { return baseMP; } }


	//------------------------

	//Base attributes
	public int attackRange;
	public int baseAttack;
	public int baseHP;
	public int baseMP;


	//Secondary attributes
	protected virtual int attackDamage { get { return baseAttack; } }

	//--------------------------------------------------End of Entity stats

	public virtual void Initialize()
	{
		hp = maxHP;
		mp = maxMP;
	}

	public virtual void takeDamage(Damage d)
	{
		hp -= d.value;

		if (hp <= 0)
			this.diedFrom (d);
	}

	public void attack(Entity e)
	{
		//Checking if e is in the attack range
		if (!isInAttackRange(e))
			return;

		e.takeDamage (this.makeDamageForEntity (e));
	}

	public void moveTo (Position p)
	{
		Vector3 newPosition = p.vector3;

		newPosition.x += .5f;
		newPosition.y += .5f;

		this.transform.position = newPosition;
		x = p.x;
		y = p.y;

		moving (this);
	}

	protected virtual Damage makeDamageForEntity(Entity e)
	{
		return new Damage (attackDamage, DamageTypes.Physical, this);
	}

	public virtual void diedFrom(Damage d)
	{
		//TODO: do things here

		//Im guessing we're gonna have an event system later
		//So we'll probably send info about this death right here
	}

	public bool isInAttackRange(Entity e)
	{
		return (Position.Distance (this.position, e.position) <= this.attackRange);
	}

	public void registerMovingCallback(Action<Entity> cb)
	{
		moving += cb;
	}
}
