using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI : MonoBehaviour {

    Queue<Task> tasks= new Queue<Task>();
    public float distance_threshold = 0.05f;
    Task current_task;
    Task priority_task;

	// Use this for initialization
	void Start () {
        
		
	}

    public void add_priority_task(Task task)
    {
        priority_task = task;
        Debug.Log("add priority task");
        if (current_task != null)
        {
            //postpone current_task, set it's state to queuing
            current_task.state = Task.State.Queuing;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (current_task == null)
        {
            if ( tasks.Count > 0 )
                current_task = tasks.Dequeue();
        }

        Task task=null;
        if (priority_task != null)
        {
            //Debug.Log("priority task");
            task = priority_task;
        }
        else if (current_task != null)
        {
            //Debug.Log("current task");
            task = current_task;
        }

        if (task != null)
        {
            if (task.state == Task.State.Queuing)
                execute_task(task);            
            else if (task.state == Task.State.InProgress)
                update_task(task);
            else if (task.state == Task.State.Done)
                task = null;
        }

        if (GetComponent<Character>().character_info.stamina < 30f)
        {
            //Debug.Log("Stamina: " + GetComponent<Character>().character_info.stamina);
            if ( current_task == null || current_task.building.type != Building.Type.House)
                rest();
        }
	}

    void LateUpdate()
    {
        if (priority_task != null && priority_task.state == Task.State.Done)
            priority_task = null;
        if (current_task != null && current_task.state == Task.State.Done)
            current_task = null;
    }

    void rest()
    {
        House house = find_house();
        if (house != null)
        {
            Debug.Log("trying to rest");
            if (current_task != null)
                Debug.Log("current task != null");
            Task task= new Task(house);
            tasks.Enqueue(task);
        }
    }

    void update_task(Task task)
    {
        if ( Vector3.Distance(this.transform.position, task.destination() ) < distance_threshold )
        {
            if (task.target_type == Task.TargetType.Building)
            {
                GetComponent<Character>().enter(task.building);
                //current_task.building.enter(GetComponent<Character>());
            }
            Debug.Log("task done");
            task.state = Task.State.Done;
        }
    }

    void execute_task(Task task)
    {
        Debug.Log("Execute task " + task.destination());
        GetComponent<CharacterController>().move_to( task.destination());
        task.state = Task.State.InProgress;
    }

    House find_house()
    {
        //replace by BuildingManager, which holds all buildings
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        foreach (GameObject i in buildings)
        {
            Building building = i.GetComponent<Building>();
            if (building == null)
                continue;
            if (building.type == Building.Type.House)
            {
                House house = i.GetComponent<House>();
                if (house.vacanies() > 0)
                    return house;
            }

        }
        return null;
    }

}
