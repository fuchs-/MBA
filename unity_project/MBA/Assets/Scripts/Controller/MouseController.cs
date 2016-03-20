using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

	//this prevents getting the camera on every single frame
	private Camera camera;

	//position of the camera on the last frame
	private Vector3 lastMousePosition;


	//Zoom limits
	private static int minZoom = 3, maxZoom = 8;

	void Start()
	{
		camera = Camera.main;


	}

	void Update () {

		//Calculate if the player is trying to drag the camera
		checkCameraDrag ();

		//Calculate if the player is trying to zoom in/out
		calculateCameraZoom();

		//Highlighting tile under the mouse
		MapController.mapController.highlightPosition (getMouseUnitPosition ());

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
		camera.orthographicSize -= Input.mouseScrollDelta.y;

		if (camera.orthographicSize < minZoom)
			camera.orthographicSize = minZoom;
		if (camera.orthographicSize > maxZoom)
			camera.orthographicSize = maxZoom;
	}

	private Vector3 getMouseUnitPosition()
	{
		Vector3 mp = getMouseWorldPosition ();

		mp.x = Mathf.Floor (mp.x);
		mp.y = Mathf.Floor (mp.y);
		mp.z = 0;

		return mp;
	}

	//Returns mouse position relative to the world
	private Vector3 getMouseWorldPosition()
	{
		return camera.ScreenToWorldPoint (Input.mousePosition);
	}
}
