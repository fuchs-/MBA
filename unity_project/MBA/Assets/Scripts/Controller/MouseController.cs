using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

	void Update () {
		Vector3 mp = Input.mousePosition;
		mp = Camera.main.ScreenToWorldPoint (mp);

		int x = Mathf.FloorToInt (mp.x);
		int y = Mathf.FloorToInt (mp.y);

		//This is just sad, terrible...
		//TODO Make this right
		GameObject map;
		map = GameObject.FindGameObjectWithTag ("Map");

		MapController mc = map.GetComponent<MapController> ();
		Tile t;
		t = mc.getTile (x, y);

		if (t != null)
			Debug.Log ("Mouse over tile: (" + t.x + "," + t.y + ")");
		else
			Debug.Log ("Mouse over no tile");
	}
}
