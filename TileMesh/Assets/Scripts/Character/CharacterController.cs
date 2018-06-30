using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour {
    
    private NavMeshAgent nav_mesh_agent;

	// Use this for initialization
	void Start () {
        nav_mesh_agent = GetComponentInChildren<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void move_to( Vector3 destination)
    {
        nav_mesh_agent.SetDestination(destination);
    }

}
