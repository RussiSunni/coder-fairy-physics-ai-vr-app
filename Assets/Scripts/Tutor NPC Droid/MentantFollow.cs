using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MentantFollow: MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;
    public Toggle onOffToggle;

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
        if (onOffToggle.isOn)
        {
            Follow(target.transform.position);
        }
    }
}
