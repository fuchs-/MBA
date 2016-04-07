using UnityEngine;

public class Position {

	public int x, y;

	public Position(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public Position(Vector3 pos) : this(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y)) { }

	public Vector3 vector3
	{
		get { return new Vector3 (x, y); }
	}

	public static int Distance(Position p1, Position p2)
	{
		return Mathf.Abs (p1.x - p2.x) + Mathf.Abs (p1.y - p2.y);
	}

	public override string ToString ()
	{
		return string.Format ("Position = [{0},{1}]", x, y);
	}

	public static bool operator ==(Position p1, Position p2)
	{
		return (p1.x == p2.x) && (p1.y == p2.y);
	}

	public static bool operator !=(Position p1, Position p2)
	{
		return !(p1 == p2);
	}
}
