using UnityEngine;
using System.Collections;

//Standard floor
public class TileFloor : Tile {

	public TileFloor(int x, int y) : base(x, y, TileTypes.Floor)
	{
		this.VisualTile = (GameObject)GameObject.Instantiate (Resources.Load ("Tiles/FloorTile"));
		VisualTile.transform.parent = GameObject.FindGameObjectWithTag("Map").transform;
	}

	public override int movementSpeed()
	{
		return 4;
	}
}
