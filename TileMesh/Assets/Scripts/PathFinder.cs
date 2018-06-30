using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder{



    public TileMap tile_map;

    Queue<Vector2> find_path(Vector2 starting_point)
    {
        Queue<Vector2> result = new Queue<Vector2>();

        return result;
    }

    bool can_pass(Vector2 from, Vector2 to)
    {
        if (is_valid_pos(from) && is_valid_pos(to))
        {
            return true;
        }
        else
            return false;
    }

    bool is_adjacent(Vector2 pos1, Vector2 pos2)
    {
        return true;
    }

    bool is_valid_pos( Vector2 pos )
    {
        return (pos.x >= 0 && pos.x <= tile_map.size_x) &&
        (pos.y >= 0 && pos.y <= tile_map.size_z);
    }
    
}
