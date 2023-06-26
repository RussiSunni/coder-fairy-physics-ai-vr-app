using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TutorDroidFollowBehaviour: MonoBehaviour
{
    NavMeshAgent agent;
    private GameObject target;
    public Toggle onOffToggle;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        target = GameObject.Find("XR Origin");
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
