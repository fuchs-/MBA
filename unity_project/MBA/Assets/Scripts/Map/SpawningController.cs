using UnityEngine;
using System.Collections.Generic;

public class SpawningPoolItem
{
	public Hero hero { get; private set; }
	private int countdown;

	public SpawningPoolItem (Hero h, int countdown)
	{
		this.hero = h;
		this.countdown = countdown;
	}

	public bool shouldSpawnThisTurn()
	{
		return --countdown == 0;
	}
}

public class SpawningController 
{
	//Rounds it takes for a dead hero to respawn
	private const int SPAWNING_TIME = 3;

	//Spawning points
	private List<Position> redSpawningPoints;
	private List<Position> blueSpawningPoints;

	//Hero to be spawned -> turns remaining until spawned
	private List<SpawningPoolItem> spawningPool;


	//The spawning points are currently the bottommost for the red team and the topmost for the blue team
	//If you'd like to change that, change this constructor
	public SpawningController(int mapWidth, int mapHeight)
	{
		redSpawningPoints = new List<Position>();
		blueSpawningPoints = new List<Position>();

		for (int x = 1; x < mapWidth - 1; x++) 
		{
			redSpawningPoints.Add (new Position (x, 1));
			blueSpawningPoints.Add (new Position (x, mapHeight - 2));
		}

		spawningPool = new List<SpawningPoolItem>();

		GameController.gameController.registerTurnChangeCallback (passingTurn);
	}

	private void spawnHero(Hero h)
	{
		Position spawningPoint = getValidSpawningPoint (h.team);

		h.spawnAt (spawningPoint);
	}

	private Position getValidSpawningPoint(Teams team)
	{
		Position[] array;
		Position ret = null;


		if (team == Teams.Blue)
			array = blueSpawningPoints.ToArray();
		else if (team == Teams.Red)
			array = redSpawningPoints.ToArray();
		else {
			Debug.LogError ("Trying to get spawning point for a weird team");
			return null;
		}

		for (int i = 0; i < array.Length; i++) {
			ret = array [i];

			if (MapController.mapController.isPositionEmpty (ret))
				break;
		}

		return ret;
	}

	//turn changing callback
	public void passingTurn()
	{
		if (spawningPool.Count == 0)
			return;

		foreach (SpawningPoolItem i in spawningPool.ToArray()) {
			if (i.shouldSpawnThisTurn ()) {
				spawningPool.Remove (i);
				spawnHero (i.hero);
			}
		}
	}

	//hero dieing callback
	public void entityDied(Entity e, Damage d)
	{
		if (e.getEntityType () == EntityTypes.Entity)
			return;

		Hero h = (Hero)e;

		spawningPool.Add (new SpawningPoolItem (h, SPAWNING_TIME));
	}
}
