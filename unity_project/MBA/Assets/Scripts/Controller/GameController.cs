using UnityEngine;

public enum GameStates
{
	StandBy,
	Attacking
}

public class GameController : MonoBehaviour {

	Hero selected;
	GameStates gameState;

	void Start()
	{
		gameState = GameStates.StandBy;
	}

	public void HeroClicked(Hero h)
	{
		if (gameState == GameStates.Attacking) 
		{
			selected.attack (h);
			gameState = GameStates.StandBy;
		}
		else
			selected = h;
	}

	public void AttackButtonPressed()
	{
		if (gameState == GameStates.Attacking)
			gameState = GameStates.StandBy;
		else
			gameState = GameStates.StandBy;
	}
}
