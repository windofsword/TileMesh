using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileMap))]
public class TileMapInspector : Editor {

	public override void OnInspectorGUI()
	{
		//base.OnInspectorGUI ();
		DrawDefaultInspector ();

		if (GUILayout.Button ("Regenerate"))
		{
			TileMap tile_map = (TileMap)target;
			tile_map.buildMesh();
		}
	}

}
