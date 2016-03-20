using UnityEngine;
using System.Collections;

public class Position {

	public int x, y;

	public Position(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public Position(Vector3 pos) : this((int)pos.x, (int)pos.y) { }

	public Vector3 vector3
	{
		get { return new Vector3 (x, y); }
	}
}
