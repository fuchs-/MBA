using UnityEngine;
using System;

public enum GameStates
{
	StandBy,
	Attacking,
	Moving
}

public class GameController : MonoBehaviour {

	/// <summary>
	/// THERE CAN BE ONLY ONE
	/// </summary>
	public static GameController gameController;

	public Hero selectedHero;
	GameStates gameState;
	public Teams turn { get; private set; }



	Action passingTurn;
	Action<Hero> selectedHeroChanging; //Hero is the new selected hero, bool is true if Hero is from the team which is currently playing

	void Start()
	{
		//THERE CAN BE ONLY ONE
		if (gameController != null && gameController != this) {
			Destroy (this.gameObject);
			return;
		}
		gameController = this;

		gameState = GameStates.StandBy;

		turn = Teams.Red;
		UIController.UI.Initialize ();
	}


	public void MouseClickedAtPosition(Position position)
	{
		if (!MapController.mapController.isInsideBounds(position)) {
			//Mouse was clicked outside of the map
			return;
		}

		MapPositionData posData = MapController.mapController.getMapPositionData (position);

		if (posData.isEmpty ()) {

			if (gameState == GameStates.Moving) {
				MapController.mapController.moveHeroToPosition (selectedHero, position);
			}
			else {
				selectedHero = null;
				selectedHeroChanging (selectedHero);
			}

			if (gameState != GameStates.StandBy) gameState = GameStates.StandBy;

		} else 
		{
			//TODO: check for effects

			if (gameState == GameStates.Attacking) 
			{
				//TODO: check if its an Entity
				selectedHero.attack (posData.hero);
				gameState = GameStates.StandBy;
			} else if (gameState == GameStates.StandBy) 
			{
				selectedHero = posData.hero;
				selectedHeroChanging (selectedHero);
			} 
			else if(gameState == GameStates.Moving) 
			{
				Debug.Log ("Can't move here");
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

	public void MoveButtonPressed()
	{
		gameState = GameStates.Moving;

		MapController.mapController.rebuildMovementGraph (selectedHero.position);
	}

	public void NextTurn()
	{
		if (turn == Teams.Red)
			turn = Teams.Blue;
		else
			turn = Teams.Red;

		Debug.Log (turn.ToString () + " team's turn");

		passingTurn();
	}

	//returns true if selectedHero's team is the team currently playing
	public bool isHeroOnTurn() 
	{
		if (selectedHero == null)
			return false;
		
		return selectedHero.team == turn;
	}



	//Callbacks registering

	public void registerTurnChangeCallback(Action cb)
	{
		passingTurn += cb;
	}

	public void registerHeroChangeCallback(Action<Hero> cb)
	{
		selectedHeroChanging += cb;
	}
}
