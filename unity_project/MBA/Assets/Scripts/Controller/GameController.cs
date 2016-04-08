﻿using UnityEngine;

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

	public Hero selectedHero
	;
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

	public void HeroClicked(Hero h)
	{
		if (gameState == GameStates.Attacking) {
			selectedHero.attack (h);
			gameState = GameStates.StandBy;
		} 
		else {
			selectedHero = h;

			Debug.Log (h.gameObject.name + " was selected");
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
