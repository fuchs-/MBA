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
	public Button attackButton;

	private HUDData currentHeroData;

	void Start()
	{
		//THERE CAN BE ONLY ONE!
		if (UI != null && UI != this) {
			Destroy (this);
		}

		UI = this;
	}

	void Update () {

	}

	public void updateHeroData(HUDData hudData)
	{
		currentHeroData = hudData;

		if (hudData.isEmpty)
			attackButton.interactable = false;
		else
			attackButton.interactable = true;

		selectedHeroText.text = currentHeroData.Name;

		if (hudData.charImg != null) {
		
			this.charImg.sprite = hudData.charImg;
		}
	}
}
