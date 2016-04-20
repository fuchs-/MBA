using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	/// <summary>
	/// THERE CAN BE ONLY ONE!
	/// </summary>
	public static UIController UI;

	//Bottom HUD Elements
	public Text selectedHeroText;
	public Image charImg;

	public Text hpValue;
	public Text mpValue;

	public Button attackButton;
	public Button nextTurnButton;

	public Color blueTeamColor;
	public Color redTeamColor;

	private HUDData currentHeroData;

	void Start()
	{
		//THERE CAN BE ONLY ONE!
		if (UI != null && UI != this) {
			Destroy (this);
		}

		UI = this;

		updateHeroData (new HUDData ());
	}

	void Update () {

	}

	public void updateHeroData(HUDData hudData)
	{
		currentHeroData = hudData;

		checkAttackButton ();
		

		selectedHeroText.text = currentHeroData.name;
		
		this.charImg.sprite = hudData.charImg;

		this.hpValue.text = string.Format("{0}", hudData.HP);
		this.mpValue.text = string.Format("{0}", hudData.MP);
	}

	public void passingTurn()
	{
		if (GameController.gameController.turn == Teams.Blue)
			nextTurnButton.GetComponent<Image> ().color = blueTeamColor;
		else
			nextTurnButton.GetComponent<Image> ().color = redTeamColor;

		checkAttackButton ();
	}

	//this checks if the attack button should be active or not
	private void checkAttackButton()
	{
		if (currentHeroData.isEmpty || currentHeroData.team != GameController.gameController.turn)
			attackButton.interactable = false;
		else
			attackButton.interactable = true;
	}
}
