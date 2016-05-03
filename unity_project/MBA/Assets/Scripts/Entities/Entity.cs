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

	//Callbacks
	Action<Entity> moving;
	Action<Entity, Damage> dieing;

	public virtual EntityTypes getEntityType() { return EntityTypes.Entity; }

	//--------------------------------------------------Entity stats

	//Basics

	private int hp;
	private int mp;

	public bool canAttack { get { return attackRange > 0; } }
	public bool isMelee { get { return attackRange == 1; } }
	public bool isRanged { get { return attackRange > 1; } }

	public int moveSpeed;
	protected int currentMoveSpeed;

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
		currentMoveSpeed = moveSpeed;
	}

	public virtual void passingTurn()
	{
		currentMoveSpeed = moveSpeed;
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
		
	public virtual void moveTo (Position p, int cost)
	{
		if (cost > this.currentMoveSpeed) {
			Debug.Log ("Can't move to position = '" + p + "' because cost is " + cost + " and currentMoveSpeed is " + currentMoveSpeed);
		} else {
			moveTo (p);
			this.currentMoveSpeed -= cost;
		}
		
	}

	//Moves this hero to position p with no costs
	public void moveTo (Position p)
	{
		Vector3 newPosition = p.vector3;

		newPosition.x += .5f;
		newPosition.y += .5f;

		this.transform.position = newPosition;
		x = p.x;
		y = p.y;

		if(moving != null) moving (this);
	}

	protected virtual Damage makeDamageForEntity(Entity e)
	{
		return new Damage (attackDamage, DamageTypes.Physical, this);
	}

	public virtual void diedFrom(Damage d)
	{
		gameObject.SetActive (false);

		if(dieing != null) dieing (this, d);
	}

	public bool isInAttackRange(Entity e)
	{
		return (Position.Distance (this.position, e.position) <= this.attackRange);
	}

	public void registerMovingCallback(Action<Entity> cb)
	{
		moving += cb;
	}

	public void registerDieingCallback(Action<Entity, Damage> cb)
	{
		dieing += cb;
	}
}
