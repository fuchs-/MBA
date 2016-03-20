using UnityEngine;
using System.Collections;

public class TileCreationData {

	public TileTypes type { get; private set; }
	public int movementSpeed { get; private set; }
	public GameObject tilePrefab { get; private set; }

	public TileCreationData(TileTypes type, int movementSpeed, GameObject prefab)
	{
		this.type = type;
		this.movementSpeed = movementSpeed;
		this.tilePrefab = prefab;
	}

}
