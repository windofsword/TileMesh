using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Task {

    public enum TargetType{Building, Ground};
    public enum State{Queuing, InProgress, Done};
    public TargetType target_type;

    public State state = State.Queuing;
    public Building building;
    Vector3 dest;


    public Task(Building b)
    {
        target_type = TargetType.Building;
        building = b;
        dest = Vector3.zero;
    }

    public Task(Vector3 destination)
    {
        target_type = TargetType.Ground;
        dest = destination;
    }

    public Vector3 destination()
    {
        if ( target_type == TargetType.Building )
        {
            //House house = building as House;
            Transform entrance = building.transform.Find("Offset").Find("Entrance");
            return entrance.position;
        }
        else
            return dest;
    }


    void execute(Character character)
    {
        if (target_type == TargetType.Building)
        {
            //character.GetComponent<NavMeshAgent>().SetDestination(building.
        }
    }


}
