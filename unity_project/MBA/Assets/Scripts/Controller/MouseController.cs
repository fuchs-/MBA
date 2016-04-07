using UnityEngine;

public class MouseController : MonoBehaviour {

	//Main camera
	private Camera camera;
	//The upper limit of the bottom hud
	private float viewBottomLimit;

	//position of the camera on the last frame
	private Vector3 lastMousePosition;


	//Zoom variables
	public float zoomSpeed;
	public float maxZoom, minZoom;

	//Checking for clicks
	private Position lastMouseButtonDown;

	void Start()
	{
		camera = Camera.main;

		//Getting the view limit
		GameObject hbp = GameObject.Find ("HUDBottom");
		if (!hbp) {
			Debug.LogError ("HUD bottom panel not found!");
			return;
		}

		viewBottomLimit = hbp.GetComponent<RectTransform> ().rect.height;
	}

	void Update () {

		//Calculate if the player is trying to drag the camera
		checkCameraDrag ();
		//Calculate if the player is trying to zoom in/out
		calculateCameraZoom();

		//Highlighting tile under the mouse
		MapController.mapController.highlightPosition (getMouseUnitPosition ());
		checkMouseClick ();
		lastMousePosition = getMouseWorldPosition ();
	}

	private void checkCameraDrag()
	{
		if (Input.GetMouseButton(1)) {

			Vector3 drag = lastMousePosition - getMouseWorldPosition();

			camera.transform.Translate(drag);
		}
	}

	private void calculateCameraZoom()
	{
		camera.orthographicSize -= Input.mouseScrollDelta.y * zoomSpeed;

		if (camera.orthographicSize > minZoom)
			camera.orthographicSize = minZoom;
		if (camera.orthographicSize < maxZoom)
			camera.orthographicSize = maxZoom;
	}

	private void checkMouseClick()
	{
		if (Input.GetMouseButtonDown (0)) {
			lastMouseButtonDown = getMouseUnitPosition ();
		}
			
		if(Input.GetMouseButtonUp(0) && isMouseInsideViewLimits())
		{
			if (getMouseUnitPosition () == lastMouseButtonDown) {
				//clicked a position on the map
				//Debug.Log("Clicked " + lastMouseButtonDown.ToString());

				MapController.mapController.MouseClickedAtPosition (lastMouseButtonDown);
			}
		}
	}

	private Position getMouseUnitPosition()
	{
		Position p = new Position (getMouseWorldPosition ());

		return p;
	}

	//Returns mouse position relative to the world
	private Vector3 getMouseWorldPosition()
	{
		return camera.ScreenToWorldPoint (Input.mousePosition);
	}

	//returns false if mouse is over hud
	private bool isMouseInsideViewLimits()
	{
		return Input.mousePosition.y > viewBottomLimit;
	}
}
