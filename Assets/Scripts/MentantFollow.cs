using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MentantFollow: MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Follow(Vector3 location)
    {
        agent.SetDestination(location);
    }
    
    void Update()
    {
        Follow(target.transform.position);
    }
}
