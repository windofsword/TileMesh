using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InputController : MonoBehaviour {

    public enum InputMode{Normal, Build};

    public CameraController camera_controller;
    private bool is_mouse_button_0_pressed_down = false;
    private bool is_mouse_button_1_pressed_down = false;
    private Vector3 previous_mouse_position;
    private GameObject selected_gameobject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //get selected object when left mouse button release
        if ( is_mouse_button_0_pressed_down && Input.GetMouseButton(0) )
        {
            GameObject collided_game_object = get_mouse_hover_object();
            if (collided_game_object)
            {                
                GameObject game_object = GetGameObjectFromCollider(collided_game_object);
                selected_gameobject = game_object;
                if (game_object)
                    Debug.Log(game_object.name);
            }
            else
                selected_gameobject = null;
        }



        //check drag
        if (Input.GetMouseButton(1))
        {
            if (is_mouse_button_1_pressed_down)
            {
                Vector3 offset = Input.mousePosition - previous_mouse_position;
                if (camera_controller)
                {
                    camera_controller.move(Direction.Left, offset.x * Camera.main.transform.position.y / 5f);
                    camera_controller.move(Direction.Down, offset.y * Camera.main.transform.position.y / 5f);
                }
            }
        }

        if (selected_gameobject != null && Input.GetMouseButtonUp(1))
        {
            selected_game_object_right_click();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            camera_controller.zoom_out();
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            camera_controller.zoom_in();
	}

    void LateUpdate()
    {
        
        if (Input.GetMouseButtonDown(0))
            is_mouse_button_0_pressed_down = true;
        else
            is_mouse_button_0_pressed_down = false;

        if (Input.GetMouseButton(1))
            is_mouse_button_1_pressed_down = true;
        else
            is_mouse_button_1_pressed_down = false;

        previous_mouse_position = Input.mousePosition;
    }

    GameObject get_mouse_hover_object()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;

        LayerMask layer = LayerMask.GetMask("Building", "Character");

        if ( Physics.Raycast(ray, out hitinfo, Mathf.Infinity, layer) )
        {
            return hitinfo.transform.gameObject;
        }
        return null;
    }

    RaycastHit get_mouse_hover_hit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;

        LayerMask layer = LayerMask.GetMask("Building", "Character", "Ground");
        Physics.Raycast(ray, out hitinfo, Mathf.Infinity, layer);
        return hitinfo;
    }

    GameObject GetGameObjectFromCollider(GameObject game_object)
    {   
        if (game_object.tag == "Character")
        {
            return game_object.transform.parent.gameObject;
        }
        else if (game_object.tag == "Building")
            return game_object.transform.parent.parent.gameObject;
        else
            return null;
    }

    void selected_game_object_right_click()
    {
        if (selected_gameobject.tag == "Character")
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;

            LayerMask layer = LayerMask.GetMask("Building", "Character", "Ground");
            if (Physics.Raycast(ray, out hitinfo, Mathf.Infinity, layer))
            {
                //CharacterController character_controller = selected_gameobject.GetComponent<CharacterController>();
                CharacterAI character_ai = selected_gameobject.GetComponent<CharacterAI>();
                if (hitinfo.transform.gameObject.tag == "Ground")
                {                    
                    //NavMeshAgent nav_mesh_agent = selected_gameobject.GetComponent<NavMeshAgent>();

                    //if (Vector3.Distance(nav_mesh_agent.destination, hitinfo.point) >= 0.1)
                    //{
                        //nav_mesh_agent.SetDestination(hitinfo.point);
                    //character_controller.move_to(hitinfo.point);
                    character_ai.add_priority_task(new Task(hitinfo.point));
                    LogBox.get_log_box().AddEvent("Character moving to " + hitinfo.point);
                    //}
                    
                }
                else if (hitinfo.transform.gameObject.tag == "Building")
                {
                    GameObject building_gameobject = GetGameObjectFromCollider(hitinfo.transform.gameObject);
                    Building building = building_gameobject.GetComponent<Building>();
                    character_ai.add_priority_task( new Task(building) );
                    /*
                    Transform entrance = hitinfo.transform.gameObject.transform.parent.Find("Entrance");
                    Debug.Log(entrance.position);
                    if (entrance)
                    {
                        //NavMeshAgent nav_mesh_agent = selected_gameobject.GetComponent<NavMeshAgent>();
                        //nav_mesh_agent.SetDestination( entrance.position );
                        character_controller.move_to(entrance.position);
                    }
                    else
                        Debug.Log("Cannot find Entrance from " + hitinfo.transform.gameObject.transform.parent.name);
                        */
                }
            }
        }
            
        
    }


}
