using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehaviour : MonoBehaviour
{
    public GameObject door;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.GetComponent<Rigidbody>().mass);
        if (other.gameObject.GetComponent<Rigidbody>().mass > 10)
        {
            // Debug.Log("success");
            door.SetActive(false);
        }
    }
}
