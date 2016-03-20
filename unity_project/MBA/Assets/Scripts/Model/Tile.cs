using UnityEngine;
using System.Collections;

public enum TileTypes
{
	Empty,
	Floor,
	Wall
}

//Base class for each tile of the map
//This can be used for any basic tile type:
//1-Create art and add to Assets/Sprites/Tiles/tile_<your_type>.png
//2-Create an empty gameobject, add sprite renderer with your sprite, call it Tile<your_type>
//3-Turn it into a prefab @ Assets/Resources/Tiles/
//4-Add tile type
//5-Go to TileFactory constructor and add your type's move speed to the switch(type)
//6-Use it
//
//If you need custom getMovementSpeed/isWalkable behavior, you have to inherit from this
public class Tile {

	//Tile coordinates
	public int x { get; protected set; }
	public int y { get; protected set; }
	public Position position { get { return new Position (x, y); } }

	public TileTypes type { get; private set; }

	//MOVEMENT SPEED - INT 0-4
	//if 0 	- not walkable
	//if 4	- Full speed
	//1 - 25%
	//2 - 50%
	//3 - 75%
	//4 - 100%
	private int movementSpeed;

	protected int MovementSpeed
	{
		get { return movementSpeed; }
		set 
		{
			if (value < 0 || value > 4) {
				Debug.LogError ("Trying to set out of bounds movementSpeed on tile at (" + x + "," + y + ")");
				return;
			}

			movementSpeed = value;
		}
	}

	public virtual int getMovementSpeed()
	{
		return MovementSpeed;
	}

	public virtual bool isWalkable() { return this.MovementSpeed > 0; }

	//Visual representation of this tile on the scene
	private GameObject visualTile;

	private GameObject VisualTile
	{
		get { return visualTile; }
		set
		{
			visualTile = value;
			visualTile.name = "Tile_" + x + "_" + y;
			visualTile.transform.position = position.vector3;
			visualTile.transform.parent = TileMapController.tileMapController.transform;
		}
	}

	//Std constructor
	public Tile(int x, int y, int movementSpeed, TileTypes type, GameObject visualPrefab)
	{
		this.x = x;
		this.y = y;
		this.movementSpeed = movementSpeed;
		this.type = type;
		this.VisualTile = GameObject.Instantiate (visualPrefab);
	}

	//Don't use this...
	protected Tile() { }

	public void destroy()
	{
		GameObject.Destroy (visualTile);
	}
}
