using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Grid))]

public class GridEditor : Editor 
{
	Grid grid;
	private bool keyPressed = false;

	public void OnEnable()
	{
		grid = (Grid)target;

		SceneView.onSceneGUIDelegate += gridUpdate;
	}

	public void OnDisable()
	{
		SceneView.onSceneGUIDelegate -= gridUpdate;
	}

	public void gridUpdate(SceneView sceneView)
	{
		Event e = Event.current;

		Ray rayon = Camera.current.ScreenPointToRay
			(
				new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight)
			);

		Vector3 mousePos = rayon.origin;

		if (e.ToString () == "Repaint" && e.control && !keyPressed) {
			keyPressed = true;

			GameObject obj;

			Object prefab = PrefabUtility.GetPrefabParent (Selection.activeObject);

			obj = (GameObject)PrefabUtility.InstantiatePrefab (prefab);

			Vector3 aligned = new Vector3 (
				Mathf.Floor (mousePos.x / grid.width) * grid.width + grid.width * .5f,
				Mathf.Floor (mousePos.y / grid.height) * grid.height + grid.height * .5f

			);

			obj.transform.position = aligned;
		} 
		else if (e.ToString () == "Repaint" && e.control && keyPressed)
			keyPressed = false;
	}

}
