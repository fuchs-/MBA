
public class MapPositionData {

	public Position position { get; private set; }

	public Tile tile { get; private set; }
	//public Effect effect { get; private set; } //Coming soon
	public Entity entity { get; private set; }
	public Hero hero { get; private set; }

	public MapPositionData (Position p, Tile t, Hero h, Entity e)
	{
		position = p;
		tile = t;
		hero = h;
		entity = e;
	}

	public MapPositionData(Position p, Tile t, Hero h) : this(p, t, h, null) { }

	public MapPositionData(Position p, Tile t) : this(p, t, null) { }

	public bool isEmpty()
	{
		return entity == null && hero == null;
	}
}
