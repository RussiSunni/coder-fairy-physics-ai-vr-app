using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehaviour : MonoBehaviour
{
    public GameObject door;
    public GameObject doorMesh;

    // Sound.
    public AudioSource audioSource;

    private bool hasChanged;
    void Start()
    {
        // Get gameobjects for prefab.
        door = GameObject.Find("Door");
        doorMesh = GameObject.Find("Door Mesh");
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.GetComponent<Rigidbody>().mass);
        if (other.gameObject.GetComponent<Rigidbody>().mass > 10 && !hasChanged)
        {            
            audioSource.Play();
            hasChanged = true;
            transform.localScale = new Vector3(5, 0.2f, 5);
            StartCoroutine(OpenDoor());         
        }
    }

    public IEnumerator OpenDoor()
    {
        //Wait here
        yield return new WaitForSeconds(1.00f);       
        door.GetComponent<AudioSource>().Play();
        doorMesh.SetActive(false);
    }
}
