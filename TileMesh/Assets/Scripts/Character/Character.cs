using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using System;

public class Character : MonoBehaviour, ISelectHandler, IPointerClickHandler, IDeselectHandler
{

    public string character_name;
    public House home;

    //public GameObject character_prefab;
    //public int move=3;
    //public Vector2 position{ get; set;} //tile position


    public CharacterInfo character_info;
    //public TileMap tile_map;
    //private GameObject character_model;


    // Use this for initialization
    void Start ()
    {
        character_info.stamina = 100f;

    }


    // Update is called once per frame
    void Update () {
        if (character_info.stamina > 0f)
        {
            character_info.stamina -= 5f * Time.deltaTime;
            character_info.stamina = Mathf.Clamp(character_info.stamina, 0f, 100f);
            //Debug.Log("character_info = " + character_info.stamina);
        }
    }

    public void move_to( Vector3 destination)
    {
        NavMeshAgent nav_mesh_agent = GetComponent<NavMeshAgent>();
        nav_mesh_agent.SetDestination(destination);
    }

    public void enter(Building building)
    {
        if ( building.enter(this) )
            gameObject.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Character selected");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Character clicked");
        if ( eventData.button == PointerEventData.InputButton.Left)
            OnSelect(eventData);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("Character deselected");
    }
}



/*
public class Character : MonoBehaviour {
    
    public string character_name;

    public GameObject character_prefab;
    public int move=3;
    public Vector2 position{ get; set;} //tile position
    public TileMap tile_map;
    private GameObject character_model;

	// Use this for initialization
	void Start ()
    {
//        character_model = Instantiate(character_prefab, this.transform);
        spawn();
        position = tile_map.to_tile_position(this.transform.position);
        Debug.Log(position);

	}

    public void spawn()
    {
        if (character_model == null)
        {
            character_model = Instantiate(character_prefab, this.transform);
            character_model.name = "BattleCharacter";
        }
    }


	
	// Update is called once per frame
	void Update () {
 
	}

    public void move_to( Vector3 destination)
    {
        NavMeshAgent nav_mesh_agent = GetComponent<NavMeshAgent>();
        nav_mesh_agent.SetDestination(destination);
    }


}
*/