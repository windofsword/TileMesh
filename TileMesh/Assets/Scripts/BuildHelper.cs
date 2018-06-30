using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildHelper : MonoBehaviour {

	public GameObject building_prefab;
	public GameObject tile_map;
    public NavMeshSurface nav_mesh_surface;
    public GameObject arrow_prefab;

	private bool enable_build_mode;
	private GameObject building_cursor;
    private GameObject arrow_cursor;

	// Use this for initialization
	void Start ()
	{
		enable_build_mode = false;
	}

	public void EnableBuildMode(GameObject selected_building)
	{
        DisableBuildMode();
		building_prefab = selected_building;
        Debug.Log("EnableBuildMode: " + building_prefab.name);
		//building_cursor = Instantiate (building_prefab, this.transform);
		enable_build_mode = true;
	}

	void DisableBuildMode()
	{
        Cursor.visible = true;
		enable_build_mode = false;
		if (building_cursor && building_cursor.activeSelf)
			Destroy (building_cursor);
        if (arrow_cursor && arrow_cursor.activeSelf)
            Destroy(arrow_cursor);
	}

	// Update is called once per frame
	void Update ()
	{
		if (enable_build_mode)
		{
			if (Input.GetMouseButton(1))
			{
				DisableBuildMode();
				return;
			}

            if (Input.GetKeyUp(KeyCode.Tab))
            {
                if (building_cursor)
                    building_cursor.GetComponent<Building>().rotate();
            }

			pre_build_check();
		}



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
            Cursor.visible = false;
            
			Vector3 local_position = transform.InverseTransformPoint (hitinfo.point);

            if (building_cursor == null)
            {
                building_cursor = Instantiate(building_prefab, this.transform);
                attach_arrow();
            }
            

			//Debug.Log ("local position: " + local_position );

			Vector3 tile_position = new Vector3 (Mathf.Floor (local_position.x), local_position.y + 0.01f, Mathf.Floor (local_position.z));

			//Debug.Log ("tile_position: " + tile_position );

			//game_object.SetActive(true);
			//Vector3 current_tile = new Vector3( hitinfo.point.x /
			//game_object.transform.position = tile_position;

			building_cursor.transform.position = tile_position;

			//game_object.GetComponent<MeshRenderer> ().material.color = Color.green;

			//if (BuildingCheck.can_build (tile_position, tile_map.GetComponent<TileMap>().tile_info_, game_object.GetComponent<Building> () ) )
			if (BuildingCheck.can_build (tile_position, tile_map.GetComponent<TileMap>().tile_info_, building_cursor.GetComponent<Building> () ) )
			{
                set_building_cursor_color(Color.green);
                //if ( building_cursor.GetComponentInChildren<MeshRenderer> () != null )
				    //building_cursor.GetComponentInChildren<MeshRenderer> ().material.color = Color.green;
				//game_object.GetComponentInChildren<MeshRenderer> ().material.color = Color.green;
			} else {
                set_building_cursor_color(Color.red);
                //if ( building_cursor.GetComponentInChildren<MeshRenderer> () != null )
				    //building_cursor.GetComponentInChildren<MeshRenderer> ().material.color = Color.red;
				//game_object.GetComponentInChildren<MeshRenderer> ().material.color = Color.red;
			}

			if ( Input.GetMouseButton (0) )
			{
				if (building_cursor.GetComponentInChildren<MeshRenderer> ().material.color == Color.green) 
				{
					//build
                    build_at(tile_position);
                    nav_mesh_surface.BuildNavMesh();
				}
			}
		}
		else
		{
            Cursor.visible = true;
			//renderer.material.color = Color.green;
		}

	}

    void set_building_cursor_color(Color color)
    {
        MeshRenderer[] mesh_renderers = building_cursor.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh_renderer in mesh_renderers)
            mesh_renderer.material.color = color;
    }

    void attach_arrow()
    {
        if (building_cursor != null)
        {
            Building building = building_cursor.GetComponent<Building>();
            Transform offset = building.transform.Find("Offset");
            if (offset != null)
            {
                Transform entrance = offset.Find("Entrance");
                if (entrance != null)
                    arrow_cursor = Instantiate(arrow_prefab, entrance);
            }
        }
    }

    void detach_arrow()
    {
        if (arrow_cursor)
        {
            arrow_cursor.transform.parent = null;
            Destroy(arrow_cursor);
        }
    }

    void build_at(Vector3 tile_position)
    {
        detach_arrow();

        Building building = building_cursor.GetComponent<Building>();

        int x_bound=0;
        int z_bound=0;
        if (building.direction == Direction.Up || building.direction == Direction.Down)
        {
            x_bound = building.size_x;
            z_bound = building.size_z;
        }
        else if (building.direction == Direction.Right || building.direction == Direction.Left)
        {
            x_bound = building.size_z;
            z_bound = building.size_x;
        }

        for (int x = 0; x < x_bound; ++x) {
            for (int y = 0; y < z_bound; ++y)
            {
                Vector2 pos = new Vector2 (tile_position.x + x, tile_position.z + y);
                if (tile_map.GetComponent<TileMap>().tile_info_.ContainsKey (pos))
                {
                    TileInfo info = tile_map.GetComponent<TileMap>().tile_info_ [pos];
                    info.building = building_cursor;
                }   
            }
        }



        building_cursor.GetComponentInChildren<MeshRenderer> ().material.color = Color.gray;

        building_cursor = Instantiate(building_cursor, this.transform);
        building_cursor.GetComponentInChildren<MeshRenderer> ().material.color = Color.red;
        attach_arrow();
        LogBox.get_log_box().AddEvent("Built " + building_cursor.name);
    }
}
