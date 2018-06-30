using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileMapMouse : MonoBehaviour {

	public GameObject game_object;
	public Button build_button;
	public GameObject tile_map;

	private bool enable_build_mode;
	private GameObject building_cursor;

	// Use this for initialization
	void Start () {
		enable_build_mode = false;
//		if (!game_object)
//		{
		building_cursor = Instantiate (game_object, this.transform);
//		}
		//Cursor.visible = false;
//		
	}
	
	// Update is called once per frame
	void Update () {

		if (enable_build_mode)
			pre_build_check ();

	
	}

	void pre_build_check()
	{
		Ray ray = Camera.main.ScreenPointToRay ( Input.mousePosition);
		RaycastHit hitinfo;

		//		MeshCollider collider = GetComponent<MeshCollider> ();
		MeshCollider collider = tile_map.GetComponent<MeshCollider> ();
		//MeshRenderer renderer = GetComponent<MeshRenderer> ();

		if (collider.Raycast (ray, out hitinfo, Mathf.Infinity))
		{
			Vector3 local_position = transform.InverseTransformPoint (hitinfo.point);
			Debug.Log ("local position: " + local_position );

			Vector3 tile_position = new Vector3 (Mathf.Floor (local_position.x), local_position.y + 0.01f, Mathf.Floor (local_position.z));

			Debug.Log ("tile_position: " + tile_position );

			//game_object.SetActive(true);
			//Vector3 current_tile = new Vector3( hitinfo.point.x /
			//game_object.transform.position = tile_position;

			building_cursor.transform.position = tile_position;

			//game_object.GetComponent<MeshRenderer> ().material.color = Color.green;

			//if (BuildingCheck.can_build (tile_position, tile_map.GetComponent<TileMap>().tile_info_, game_object.GetComponent<Building> () ) )
			if (BuildingCheck.can_build (tile_position, tile_map.GetComponent<TileMap>().tile_info_, building_cursor.GetComponent<Building> () ) )
			{
				building_cursor.GetComponentInChildren<MeshRenderer> ().material.color = Color.green;
				//game_object.GetComponentInChildren<MeshRenderer> ().material.color = Color.green;
			} else {

				building_cursor.GetComponentInChildren<MeshRenderer> ().material.color = Color.red;
				//game_object.GetComponentInChildren<MeshRenderer> ().material.color = Color.red;
			}

			if ( Input.GetMouseButton (0) )
			{
				if (game_object.GetComponentInChildren<MeshRenderer> ().material.color == Color.green) 
				{
					//build
					Building building = game_object.GetComponent<Building>();

					for (int x = 0; x < building.size_x; ++x) {
						for (int y = 0; y < building.size_y; ++y)
						{
							Vector2 pos = new Vector2 (tile_position.x + x, tile_position.z + y);
							if (tile_map.GetComponent<TileMap>().tile_info_.ContainsKey (pos))
							{
								TileInfo info = tile_map.GetComponent<TileMap>().tile_info_ [pos];
								info.building = game_object;
							}	
						}
					}
					//free game_object
					game_object = Instantiate(game_object);
				}


			}

		}
		else
		{
			//renderer.material.color = Color.green;
		}
		
	}

	void on_mouse_left_click()
	{
	}


}
