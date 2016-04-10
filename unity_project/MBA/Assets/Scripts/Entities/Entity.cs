using UnityEngine;

/// <summary>
/// Any Entity in the battlefield
/// Hero inherits from this
/// </summary>
public class Entity : MonoBehaviour {
	
	//Entity coordinates
	public int x { get; protected set; }
	public int y { get; protected set; }
	public Position position { get { return new Position (x, y); } }


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
}
