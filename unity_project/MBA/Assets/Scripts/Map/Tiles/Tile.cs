using UnityEngine;

public enum TileTypes
{
	Empty,
	Floor,
	Wall,
	Water
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

	//MOVEMENT COST - INT 0-4
	//if 4 	- not walkable
	//if 0	- Full speed
	//0 - 100%
	//1 - 75%
	//2 - 50%
	//3 - 25%
	//4 - 0%
	private int movementCost;

	protected int MovementCost
	{
		get { return movementCost; }
		set 
		{
			if (value < 0 || value > 4) {
				Debug.LogError ("Trying to set out of bounds movementCost on tile at (" + x + "," + y + ")");
				return;
			}

			movementCost = value;
		}
	}

	public virtual int getMovementCost()
	{
		return MovementCost;
	}

	public virtual bool isWalkable() { return this.MovementCost < 4; }

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
			visualTile.transform.parent = MapController.mapController.tileMap.transform;
		}
	}

	//Std constructor
	public Tile(int x, int y, int movementSpeed, TileTypes type, GameObject visualPrefab)
	{
		this.x = x;
		this.y = y;
		this.movementCost = movementSpeed;
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
