 using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {

	Tile[,] tileMap;

	//size of the map in tiles
	public int width, height;

	void Start () {

		tileMap = new Tile[width, height];

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				if (x == 0 || y == 0 || x == width-1 || y == height-1)	
					changeTileAtPosition (x, y, TileTypes.Empty);
				else 
					changeTileAtPosition (x, y, TileTypes.Floor);
			}
		}

		Debug.Log ("Map created: " + width + "x" + height);
		Debug.Log (width * height + " total tiles");
	}

	public Tile getTile(int x, int y)
	{
		if (x < 0 || x >= width || y < 0 || y >= height) {
			Debug.LogError ("Trying to reach out of range tile: (" + x + "," + y + ")");
			return null;
		} else {
			return tileMap [x, y];
		}
	}

	public void changeTileAtPosition(int x, int y, TileTypes type)
	{
		Tile t = tileMap [x, y];
		if (t != null)
			t.destroy ();

		switch (type) {
		case TileTypes.Empty:
			t = new Tile (x, y);
			break;
		case TileTypes.Floor:
			t = new TileFloor (x, y);
			break;
		default:
			Debug.LogError ("Tile type: " + type + " not implemented in method MapController.changeTileAtPosition()");
			return;
		}

		tileMap [x, y] = t;
	}

	public void RandomizeTiles()
	{
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				if (Random.Range (0, 2) == 1)
					changeTileAtPosition (x, y, TileTypes.Floor);
				else
					changeTileAtPosition (x, y, TileTypes.Empty);
			}
		}
	}
}
