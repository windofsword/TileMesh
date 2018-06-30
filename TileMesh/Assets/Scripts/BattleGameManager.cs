using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGameManager: MonoBehaviour {

    public TileMap tile_map;

    public void move(Character character, Vector2 target_tile)
    {
        /*
        TileInfo original_tile = tile_map.tile_info(character.position);

        TileInfo tile_info = tile_map.tile_info(target_tile);
        if (tile_info.unit_on_tile == null)
        {
            original_tile.unit_on_tile = null;
            tile_info.unit_on_tile = character;
            character.position = target_tile;
            Vector3 world_pos = new Vector3(target_tile.x * tile_map.tile_size, 0, target_tile.y * tile_map.tile_size);
            character.transform.position = world_pos;
        }
        */

    }

	
}
