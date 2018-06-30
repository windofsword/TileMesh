using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    public int boundary = 10;
    public int scroll_speed = 50;
    public int zoom_speed = 50;
    //public enum Direction{Up, Down, Left, Right};

    private int width = Screen.width;
    private int height = Screen.height;
    //private Vector3 right_click_position = new Vector3();


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*
        Vector2 mouse_position = Input.mousePosition;

        if (mouse_position.x > width - boundary)
            move(Direction.Right, scroll_speed);    
        else if (mouse_position.x < boundary)
            move(Direction.Left, scroll_speed);
        else if (mouse_position.y > height - boundary)
            move(Direction.Up, scroll_speed);
        else if (mouse_position.y < boundary)
            move(Direction.Down, scroll_speed);

*/
        //zoom();


        //if (Input.GetMouseButtonUp(1))
            //right_click_position = Vector3.zero;
	}

/*    void drag_and_move()
    {
        if (right_click_position == Vector3.zero)
        {
            right_click_position = Input.mousePosition;
            return;
        }

        Vector3 offset = Input.mousePosition - right_click_position;

        move(Direction.Left, offset.x * Camera.main.transform.position.y / 5f );
        move(Direction.Down, offset.y * Camera.main.transform.position.y / 5f );
        


        right_click_position = Input.mousePosition;
        

    }
*/
    public void move(Direction d, float speed)
    {
        Vector3 camera_position = Camera.main.transform.position;
        if ( d == Direction.Up )
            camera_position.z += speed * Time.deltaTime;
        else if ( d == Direction.Down )
            camera_position.z -= speed * Time.deltaTime;
        else if ( d == Direction.Left )
            camera_position.x -= speed * Time.deltaTime;
        else if ( d == Direction.Right )
            camera_position.x += speed * Time.deltaTime;
        Camera.main.transform.position = camera_position;
    }


    public void zoom_in()
    {
        Camera.main.transform.position += Camera.main.transform.forward * zoom_speed * Time.deltaTime;
    }

    public void zoom_out()
    {
        Camera.main.transform.position -= Camera.main.transform.forward * zoom_speed * Time.deltaTime;
    }

    void zoom()
    {
        //move backward
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.transform.position -= Camera.main.transform.forward * zoom_speed * Time.deltaTime;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.transform.position += Camera.main.transform.forward * zoom_speed * Time.deltaTime;
        }
        
    }
}
