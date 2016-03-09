using UnityEngine;
using System.Collections;

public enum TileTypes
{
	Empty,
	Floor,
	Water
}

//Model class for each tile of the map
//ATM if you instantiate this, you will get an empty tile
//To get other tile types you have to instantiate one of the child classes (THIS COULD CHANGE)
public class Tile {

	//Tile coordinates
	public int x { get; protected set; }
	public int y { get; protected set; }

	public TileTypes type { get; private set; }

	//MOVEMENT SPEED - INT 0-4
	//if 0 	- not walkable
	//if 4	- Full speed
	//1 - 25%
	//2 - 50%
	//3 - 75%
	//4 - 100%
	public virtual int movementSpeed()
	{
		return 0;
	}

	public bool isWalkable() { return movementSpeed () != 0; }

	//Visual representation of this tile on the scene
	private GameObject visualTile;

	protected GameObject VisualTile
	{
		get { return visualTile; }
		set
		{
			visualTile = value;
			visualTile.name = "Tile_" + x + "_" + y;
			visualTile.transform.position = new Vector3 (x, y);
		}
	}

	//Call this when making a different tile type
	protected Tile(int x, int y, TileTypes type)
	{
		this.x = x;
		this.y = y;
		this.type = type;
	}

	//Call this to instantiate an empty tile
	public Tile(int x, int y) : this(x, y, TileTypes.Empty)
	{
		VisualTile = (GameObject) GameObject.Instantiate (Resources.Load ("Tiles/EmptyTile"));
		visualTile.transform.parent = GameObject.FindGameObjectWithTag("Map").transform;
	}

	//Don't use this...
	protected Tile() { }

	public void destroy()
	{
		GameObject.Destroy (visualTile);
	}
}
