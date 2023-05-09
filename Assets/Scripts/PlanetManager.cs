using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{    
    void Start()
    {
        // Not sure if Moon gravity.
        Physics.gravity = new Vector3(0, -1.0F, 0);
    }   
}
