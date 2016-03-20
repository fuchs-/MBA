using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {

	/// <summary>
	/// THERE CAN BE ONLY ONE
	/// </summary>
	public static MapController mapController;

	//size of the map in units/positions
	public int width, height;


	//Tile Highlighting
	private GameObject tileHighlightPrefab;
	private GameObject tileHighlight;

	void Start () {

		//THERE CAN BE ONLY ONE
		if (mapController != null && mapController != this) {
			Destroy (this.gameObject);
			return;
		}

		mapController = this;

		TileMapController.tileMapController.CreateTileMap ();

		tileHighlightPrefab = (GameObject) Resources.Load ("TileHighlight");
	}


	//Tile Highlighting
	public void highlightPosition(Vector3 position)
	{
		if (position.x < 0 || position.x >= this.width) 	//Mouse is to the left/right of the map
			return;
		if (position.y < 0 || position.y >= this.height)	//Mouse is bellow/above the map
			return;
		
		Destroy (tileHighlight);

		tileHighlight = (GameObject)Instantiate (tileHighlightPrefab, position, Quaternion.identity);
	}
}
