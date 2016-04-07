public enum DamageTypes
{
	Physical,
	Magical,
	Pure,
	Composite
}

public class Damage {

	public int value;

	private DamageTypes type;
	public DamageTypes Type {
		get {
			return type;
		}
	}

	private Entity source;
	public Entity Source {
		get {
			return source;
		}
	}

	public Damage(int value, DamageTypes type, Entity source)
	{
		this.value = value;
		this.type = type;
		this.source = source;
	}
}
