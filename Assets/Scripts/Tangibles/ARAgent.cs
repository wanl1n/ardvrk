using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ARAgent : MonoBehaviour
{
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        this.agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.gameObject.name + " at " + this.transform.position);
    }

    public void MoveAgent(Vector3 position)
    {
        agent.isStopped = false;
        agent.destination = position;
    }

    public void StopAgent() 
    { 
        agent.isStopped = true; 
    }
}
