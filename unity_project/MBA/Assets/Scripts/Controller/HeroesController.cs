using UnityEngine;

public class HeroesController : MonoBehaviour {

	private Hero[] heroes;

	private int[,] heroMap;
	private int width, height;

	public void CreateHeroMap()
	{
		this.width = MapController.mapController.width;
		this.height = MapController.mapController.height;

		heroMap = new int[width, height];
	
		initializeHeroes ();
		buildHeroMap ();
	}

	private void initializeHeroes()
	{
		heroes = new Hero[transform.childCount];

		for (int i = 0; i < heroes.Length; i++) {
			
			heroes [i] = transform.GetChild (i).GetComponent<Hero>();
			heroes [i].Initialize ();
		}
	}

	private void buildHeroMap()
	{
		//Filling map with -1's
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				heroMap [x,y] = -1;
			}
		}

		Hero h;

		//Adding the heroes
		for (int i = 0; i < heroes.Length; i++) {
			h = heroes [i];

			heroMap [h.x, h.y] = i;
		}
	}

	public Hero getHeroAtPosition(Position p)
	{
		int heroIndex;

		heroIndex = heroMap [p.x, p.y];

		if (heroIndex < 0) return null;

		return heroes [heroIndex];
	}
}
