using UnityEngine;

public class TileMapController : MonoBehaviour {

	//public static TileMapController tileMapController;

	public Tile[,] tiles;
	private TileFactory tileFactory;

	private int width, height;

	void Start() { }

	public void CreateTileMap()
	{
		this.width = MapController.mapController.width;
		this.height = MapController.mapController.height;

		tiles = new Tile[width,height];
		tileFactory = new TileFactory ();

		//Temporary
		setExampleStartingPosition();

		Debug.Log ("Map created: " + width + "x" + height);
		Debug.Log (width * height + " total tiles");
	}


	/// <summary>
	/// Changes the tile at position x and y to the TileType "type"
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="type">New type</param>
	public void changeTileAtPosition(int x, int y, TileTypes type)
	{
		Tile t = tiles [x, y];

		if (t != null) {
			if (t.type == type)	return;

			t.destroy ();
		}

		t = tileFactory.MakeTile (x, y, type);


		tiles [x, y] = t;
	}

	public Tile getTile(int x, int y)
	{
		if (x < 0 || x >= width || y < 0 || y >= height) {
			Debug.LogError ("Trying to reach out of range tile: (" + x + "," + y + ")");
			return null;
		} else {
			return tiles [x, y];
		}
	}

	public Tile getTile(Position p) { return getTile (p.x, p.y); }


	//Movement stuff
	public void populateMovementArray(int[,] array)
	{
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				array [x, y] = tiles [x, y].getMovementSpeed ();
			}
		}
	}


	//THIS IS COMPLETELY TEMPORARY
	public void setExampleStartingPosition()
	{
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				if (x == 0 || y == 0 || x == width-1 || y == height-1)	
					changeTileAtPosition (x, y, TileTypes.Wall);
				else 
					changeTileAtPosition (x, y, TileTypes.Floor);
			}
		}
	}

}
