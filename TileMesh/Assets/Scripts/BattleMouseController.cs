using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMouseController : MonoBehaviour {

    public LogBox log_box;
    public Button attack_button;
    public MoveTileIndicator move_tile_indicator;
    public TileMap tile_map;
    public BattleGameManager battle_game_manager;

    private GameObject selected_character;

	// Use this for initialization
	void Start () {
        //attack_button.enabled = false;
        attack_button.gameObject.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (selected_character != null)
            check_move_select();
        else
            check_character_select();

        

        if (Input.GetMouseButton(1))
            deselect();


	}

    void check_character_select()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawLine(ray.origin, ray.direction * 50000000, Color.red);

            RaycastHit hitinfo;
            int layer_mask = LayerMask.GetMask("Character");
            //Debug.Log("layer_mask = " + layer_mask);

            if (Physics.Raycast(ray, out hitinfo, Mathf.Infinity, layer_mask))
            {
                if (selected_character != hitinfo.transform.parent.parent.parent.gameObject)
                {
                    selected_character = hitinfo.transform.parent.parent.parent.gameObject;
                    move_tile_indicator.show_movable_tiles(selected_character.GetComponent<Character>());

                    log_box.AddEvent("Selected Character");
                }

            }
        }
    }

    void check_move_select()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitinfo;
            int layer_mask = LayerMask.GetMask("MoveTile");
            //Debug.Log("layer_mask = " + layer_mask);

            if (Physics.Raycast(ray, out hitinfo, Mathf.Infinity, layer_mask))
            {
                Debug.Log("Move to " + tile_map.to_tile_position(hitinfo.transform.parent.position) );
                battle_game_manager.move(selected_character.GetComponent<Character>(), tile_map.to_tile_position(hitinfo.transform.parent.position));
                attack_button.gameObject.SetActive(true);
                deselect();
            }
        }
    }

    void deselect()
    {
        selected_character = null;
        move_tile_indicator.remove_tile_indicators();
        attack_button.enabled = false;
    }
}
