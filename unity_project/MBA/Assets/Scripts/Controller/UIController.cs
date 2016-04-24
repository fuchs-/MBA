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
	public Image charImgTeamHighlight;
	public Image charImg;

	public Text hpValue;
	public Text mpValue;

	public Button attackButton;
	public Button moveButton;
	public Button nextTurnButton;

	public Color blueTeamColor;
	public Color redTeamColor;
	public Color noTeamColor;

	private HUDData currentHeroData;

	void Start()
	{
		//THERE CAN BE ONLY ONE!
		if (UI != null && UI != this) {
			Destroy (this);
		}

		UI = this;
	}

	public void Initialize()
	{
		updateHeroData (new HUDData ());
		GameController.gameController.registerTurnChangeCallback (passingTurn);
		GameController.gameController.registerHeroChangeCallback (selectedHeroChanging);
		passingTurn ();
		selectedHeroChanging (null);
	}

	void Update () {

	}

	public void selectedHeroChanging(Hero h)
	{
		if (h == null) {
			updateHeroData (new HUDData ());
		} else {
			updateHeroData (h.getHUDData ());
		}
	}

	private void updateHeroData(HUDData hudData)
	{
		currentHeroData = hudData;
		

		selectedHeroText.text = currentHeroData.name;
		this.charImg.sprite = hudData.charImg;

		this.hpValue.text = string.Format("{0}", hudData.HP);
		this.mpValue.text = string.Format("{0}", hudData.MP);

		updateButtons ();
		checkTeamHighlight ();
	}

	public void passingTurn()
	{
		if (GameController.gameController.turn == Teams.Blue)
			nextTurnButton.GetComponent<Image> ().color = blueTeamColor;
		else
			nextTurnButton.GetComponent<Image> ().color = redTeamColor;

		updateButtons ();
		checkTeamHighlight ();
	}

	//this checks if the attack button should be active or not
	private void updateButtons()
	{
		if (currentHeroData.isEmpty || currentHeroData.team != GameController.gameController.turn) {
			attackButton.interactable = false;
			moveButton.interactable = false;
		} else {
			attackButton.interactable = true;
			moveButton.interactable = true;
		}
	}

	private void checkTeamHighlight()
	{
		if (GameController.gameController.isHeroOnTurn ()) {
			if (GameController.gameController.turn == Teams.Blue)
				charImgTeamHighlight.color = blueTeamColor;
			else
				charImgTeamHighlight.color = redTeamColor;
		} else {
			charImgTeamHighlight.color = noTeamColor;
		}
	}
}
