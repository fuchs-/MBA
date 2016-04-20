using UnityEngine;

public class HeroesController : MonoBehaviour {

	//List of all heroes in the match
	private Hero[] heroes;

	//Lists of heroes on each team
	//These could change from arrays to lists or maybe an encapsulating class "Team" or "HeroListController" or something like that
	private Hero[] redTeam;
	private Hero[] blueTeam;


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

		redTeam = new Hero[heroes.Length / 2];
		blueTeam = new Hero[heroes.Length / 2];

		Hero h;

		int r, b; //red and blue teams indexes
		r = 0;
		b = 0;

		for (int i = 0; i < heroes.Length; i++) {
			
			h = transform.GetChild (i).GetComponent<Hero>();
			h.Initialize ();

			heroes [i] = h;

			if (h.team == Teams.Red)
				redTeam [r++] = h;
			else
				blueTeam [b++] = h;
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
