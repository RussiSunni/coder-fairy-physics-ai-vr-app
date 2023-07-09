using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalPowerUpBehaviour : MonoBehaviour
{
    private GameObject playerDroid;
    public float rotationsPerMinute = 10.0f;  
   
    void Update()
    {
        playerDroid = GameObject.Find("Player Droid UI");
        // Make it spin.
        transform.Rotate(0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {     
        playerDroid.GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);
    }
}
