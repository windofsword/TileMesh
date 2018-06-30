using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCheck {

	public BuildingCheck(){
	}
	
	public static bool test() {
		return true;
	}

	public static bool can_build( Vector3 tile_pos, Dictionary<Vector2, TileInfo> tile_info, Building building)
	{
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
        for (int x = 0; x < x_bound; ++x)
        {
            for (int z = 0; z < z_bound; ++z)
			{
				Vector2 pos = new Vector2 (tile_pos.x + x, tile_pos.z + z);
				if (tile_info.ContainsKey (pos))
                {
					TileInfo info = tile_info [pos];
					if (info.building)
						return false;
				} else
					return false;
			}
		}
		
		return true;
	}

}
