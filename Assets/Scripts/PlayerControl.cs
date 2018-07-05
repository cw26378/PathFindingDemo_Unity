using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : MonoBehaviour {

    public Camera cam;
    public NavMeshAgent lead_agent;
    
    NavMeshAgent agent;
    private bool hasDestination;
    private float crowd_coeff = 2.0f;

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        hasDestination = false;
    }

    // Update is called once per frame
	void Update () 
    {

        if (Input.GetMouseButtonDown(0))
        {
            //re-activate the stopped agent
            agent.isStopped = false;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // if the raycast hit is within range    
       
                // MOVE THE AGENT 
                agent.SetDestination(hit.point);
                // The destination is already set
                hasDestination = true;
            }
        }

        if (hasDestination == true) 
        {
            float distance_destination = Vector3.Distance(agent.transform.position, agent.destination);

            
            if (distance_destination < agent.stoppingDistance)
            {
                // if current agent is close enough, stop it
                agent.isStopped = true;
            }

            //Debug.Log("Lead agent distance:" + lead_agent.remainingDistance.ToString());
            //Debug.Log("Current agent distance:" + distance_destination.ToString() + "lead agent status: " + lead_agent.isStopped.ToString());
            
            if (lead_agent.isStopped == true && distance_destination < agent.stoppingDistance * crowd_coeff) 
            {
                // if lead agent is already arrived and current one is not far off, stop the current agent
                agent.isStopped = true;
                
                //Debug.Log("after manual stop, Current agent stopped:" + agent.isStopped.ToString());
            }
            
        }


	}

}
