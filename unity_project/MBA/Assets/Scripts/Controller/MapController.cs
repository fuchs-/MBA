using UnityEngine;
using System.Collections.Generic;

public class MapController : MonoBehaviour {

	/// <summary>
	/// THERE CAN BE ONLY ONE
	/// </summary>
	public static MapController mapController;

	//size of the map in units/positions
	public int width, height;

	public TileMapController tileMap;
	public HeroesController heroes;


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

		GameObject tileMap_go = transform.FindChild ("TileMap").gameObject;
		if (tileMap_go) {
			tileMap = tileMap_go.GetComponent<TileMapController> ();
		} else {
			Debug.LogError ("TileMap not found");
		}

		GameObject heroes_go = GameObject.Find ("Heroes");
		if (heroes_go) {
			heroes_go.transform.SetParent (this.transform);
			heroes = heroes_go.GetComponent<HeroesController> ();
		} else {
			Debug.LogError ("Heroes GameObject not found");
		}

		tileMap.CreateTileMap ();
		heroes.CreateHeroMap ();

		tileHighlightPrefab = (GameObject) Resources.Load ("TileHighlight");
	}

	public void MouseClickedAtPosition(Position p)
	{
		Hero h;
		h = heroes.getHeroAtPosition (p);

		//There's a hero at this position, I should tell the GameController
		if (h) {
			GameController.gameController.HeroClicked (h);
		} else {
			//TODO: check for other stuff like movement or other entities
			//For now, I'll just unselect the hero


			GameController.gameController.selectedHero = null; // This should PROOOOBABLY not be here (Probably should be done in GameController)
			UIController.UI.updateHeroData(new HUDData());
		}
	}


	//Tile Highlighting
	public void highlightPosition(Position position)
	{
		if (position.x < 0 || position.x >= this.width) 	//Mouse is to the left/right of the map
			return;
		if (position.y < 0 || position.y >= this.height)	//Mouse is bellow/above the map
			return;
		
		if (!tileHighlight)
			tileHighlight = (GameObject)Instantiate (tileHighlightPrefab, position.vector3, Quaternion.identity);
		else
			tileHighlight.transform.position = position.vector3;
	}
}
