using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class TileMap : MonoBehaviour {

	//number of tiles
	public int size_x, size_z;
	public float tile_size = 1.0f;

	public Dictionary<Vector2, TileInfo> tile_info_;

    public Vector2 to_tile_position( Vector3 global_pos)
    {        
        Vector2 result = new Vector2( Mathf.FloorToInt(global_pos.x / tile_size), Mathf.FloorToInt(global_pos.z / tile_size) );
        return result;
    }

	// Use this for initialization
	void Start () {

		buildMesh ();

		tile_info_ = new Dictionary<Vector2, TileInfo> ();
		for (int i = 0; i < size_x; ++i)
			for (int j = 0; j < size_z; ++j)
			{
				Vector2 tile_pos= new Vector2 (i, j);
				tile_info_ [tile_pos] = new TileInfo ();
			}
	}

	public TileInfo tile_info( Vector2 tile_position) 
	{
		if (tile_info_.ContainsKey (tile_position)) {
			TileInfo info= tile_info_[tile_position];
			return info;
		} else
			return null;
	}

    public bool is_valid( Vector2 tile_pos )
    {
        return tile_info_.ContainsKey(tile_pos);
    }


	public void buildMesh()
	{
		int vertices_x = size_x + 1;
		int vertices_z = size_z + 1;
		int vertices_size = vertices_x * vertices_z;
		int tiles_size = size_x * size_z;
		
		Vector3[] vertices = build_vertices ();
		int[] triangles = build_triangles();
		Vector3[] normals = new Vector3[vertices_size];
		Vector2[] uv = new Vector2[vertices_size];

		for (int z = 0; z < vertices_z; ++z)
		{
			for (int x = 0; x < vertices_x; ++x)
			{
				uv [z * vertices_x + x] = new Vector2 ((float) x / size_x, (float) z / size_z);
			}
		}

		for (int i = 0; i < vertices_size; ++i)
		{
			normals [i] = Vector3.up;
		}

		Mesh mesh = new Mesh ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;

		MeshCollider mesh_collider = GetComponent<MeshCollider> ();
		MeshRenderer mesh_renderer = GetComponent<MeshRenderer> ();
		MeshFilter mesh_filter = GetComponent<MeshFilter> ();

		mesh_filter.mesh = mesh;
		mesh_collider.sharedMesh = mesh;
	}


	Vector3[] build_vertices()
	{
		int vertices_x = size_x + 1;
		int vertices_z = size_z + 1;
		int vertices_size = vertices_x * vertices_z;
	
		Vector3[] vertices = new Vector3[vertices_size];

		for (int i = 0; i < vertices_z; ++i)
		{
			float z_coordinate = i * tile_size;
			for (int j = 0; j < vertices_x; ++j)
			{
				float x_coordinate = j * tile_size;
				int index = i * vertices_x + j;
//				Debug.Log ("i="+i + ", j=" + j + ", index = " + index);

				vertices [index] = new Vector3 (x_coordinate, 0, z_coordinate);
//				Debug.Log ("vertex[" + index + "]= (" + x_coordinate + ", 0, " + z_coordinate + ")" );
			}
		}
		return vertices;
	}

	int[] build_triangles()
	{
		int tiles_size = size_x * size_z;
		int vertices_x = size_x + 1;
		int vertices_z = size_z + 1;

		int[] triangles = new int[tiles_size * 2 * 3];

		/*
		 * 0 1 2 3 4
		 * 5 6 7 8 9
		 * 
		 */

		int triangle_index = 0;

		//for each tile
		for (int i = 0; i < size_z; ++i)
		{
			for (int j = 0; j < size_x; ++j)
			{				
				int top_left_index = i * vertices_x + j;
				int top_right_index = i * vertices_x + j + 1;
				int bot_left_index = (i+1) * vertices_x + j;
				int bot_right_index = (i+1) * vertices_x + j + 1;

//				Debug.Log ("i="+i + ", j=" + j);
//				Debug.Log (top_left_index);
//				Debug.Log (top_right_index);
//				Debug.Log (bot_left_index);
//				Debug.Log (bot_right_index);


				//build upper triangle 1, 2, 5
				triangles[ triangle_index++ ] = top_left_index;
				triangles[ triangle_index++ ] = bot_left_index;
				triangles[ triangle_index++ ] = top_right_index;


				//build lower triangle 2, 6, 5
				triangles[ triangle_index++ ] = top_right_index;
				triangles[ triangle_index++ ] = bot_left_index;
				triangles[ triangle_index++ ] = bot_right_index;

			}
		}

		//Debug.Log (triangle_index);
		return triangles;
	}
}
