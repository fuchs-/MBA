using UnityEngine;

public enum GameStates
{
	StandBy,
	Attacking
}

public class GameController : MonoBehaviour {

	/// <summary>
	/// THERE CAN BE ONLY ONE
	/// </summary>
	public static GameController gameController;

	public Hero selectedHero;
	GameStates gameState;

	void Start()
	{
		//THERE CAN BE ONLY ONE
		if (gameController != null && gameController != this) {
			Destroy (this.gameObject);
			return;
		}
		gameController = this;

		gameState = GameStates.StandBy;
	}


	public void MouseClickedAtPosition(Position position)
	{
		MapPositionData posData = MapController.mapController.getMapPositionData (position);

		if (posData == null) {
			//Mouse was clicked outside of the map
			Debug.Log("Mouse clicked outside of the map");
			return;
		}

		if (posData.isEmpty ()) {
			selectedHero = null;
			UIController.UI.updateHeroData (new HUDData ());

			if (gameState == GameStates.Attacking)
				gameState = GameStates.StandBy;
		} else 
		{
			//TODO: check for effects

			if (gameState == GameStates.Attacking) {
				//TODO: check if its an Entity
				selectedHero.attack (posData.hero);
				gameState = GameStates.StandBy;
			} else 
			{
				selectedHero = posData.hero;
				UIController.UI.updateHeroData (posData.hero.getHUDData ());
			}
		}
	}

	public void AttackButtonPressed()
	{
		if (gameState == GameStates.Attacking)
			gameState = GameStates.StandBy;
		else
			gameState = GameStates.Attacking;
	}
}
