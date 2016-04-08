using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	public Text selectedHeroText;

	public Button attackButton;


	void Update () {
		if (!GameController.gameController.selectedHero) {
			attackButton.interactable = false;
			selectedHeroText.text = "No Hero Selected";
		} else
		{
			attackButton.interactable = true;
			selectedHeroText.text = GameController.gameController.selectedHero.gameObject.name;
		}
	}
}
