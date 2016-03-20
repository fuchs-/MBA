using UnityEngine;
using System;
using System.Collections.Generic;

public class TileFactory {

	private static List<TileCreationData> tileData;

	public TileFactory()
	{
		tileData = new List<TileCreationData> ();
	
		TileCreationData tcd;
		int moveSpeed;
		GameObject prefab;

		foreach (TileTypes type in Enum.GetValues(typeof(TileTypes)))
		{
			switch (type) {
			case TileTypes.Empty:
				moveSpeed = 0;
				break;
			case TileTypes.Floor:
				moveSpeed = 4;
				break;
			case TileTypes.Wall:
				moveSpeed = 0;
				break;
			default:
				moveSpeed = 0;
				Debug.LogError ("Tile type: '" + type + "' not implemented on TileFactory.Initiate()");
				break;
			}

			prefab = (GameObject) Resources.Load("Tiles/Tile" + type);

			tcd = new TileCreationData (type, moveSpeed, prefab);
			tileData.Add (tcd);
		}
	}

	public Tile MakeTile(int x, int y, TileTypes type)
	{
		TileCreationData retData = null;
		
		foreach (TileCreationData tcd in tileData) {
			if (tcd.type == type) {
				retData = tcd;
				break;
			}
		}

		if (retData == null) {
			Debug.LogError ("Trying to create tile of type '" + type + "' but TileFactory has no data about it");
			return null;
		}

		return new Tile (x, y, retData.movementSpeed, type, retData.tilePrefab);
	}

	public Tile MakeTile(Position position, TileTypes type)
	{
		return this.MakeTile (position.x, position.y, type);
	}

}
