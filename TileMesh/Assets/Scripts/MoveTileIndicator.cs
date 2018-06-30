using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTileIndicator : MonoBehaviour {

    public TileMap tile_map;
    public GameObject tile_indicator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void show_movable_tiles(Character character)
    {
        /*
        Vector2 position = character.position;
        int move = character.move;

        for (int i = -move; i <= move; ++i)
        {
            for (int j = -move; j <= move; ++j)
            {
                if (i == 0 && j == 0)
                    continue;
                if (Mathf.Abs(i) + Mathf.Abs(j) > move)
                    continue;

                Vector2 target_pos = new Vector2(position.x + i, position.y + j);
                if (tile_map.is_valid(target_pos))
                {
                    spawn_tile_indicator(target_pos);
                }
                
            }
        }
        */
    }

    public void remove_tile_indicators()
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void spawn_tile_indicator(Vector2 pos)
    {
        GameObject indicator = Instantiate(tile_indicator, this.transform);
        Vector3 local_pos = new Vector3( pos.x * tile_map.tile_size, 0, pos.y * tile_map.tile_size );
        indicator.transform.localPosition = local_pos;
    }

}
