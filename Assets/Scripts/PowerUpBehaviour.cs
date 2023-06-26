using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    public float rotationsPerMinute = 10.0f;
    private CanvasGroup massCanvasGroup;
    private CanvasGroup gravityCanvasGroup;
    public GameObject player;

    void Start()
    {
        massCanvasGroup = GameObject.Find("MASS").GetComponent<CanvasGroup>();
        gravityCanvasGroup = GameObject.Find("GRAVITY").GetComponent<CanvasGroup>();
        player = GameObject.Find("Droid Controller Canvas");
    }
    void Update()
    {
        // Make it spin.
        transform.Rotate(0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        massCanvasGroup.alpha = 1;
        massCanvasGroup.interactable = true;
        gravityCanvasGroup.alpha = 1;        
        player.GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);
    }
}
