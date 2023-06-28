using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassPowerUpBehaviour : MonoBehaviour
{
    private CanvasGroup massCanvasGroup;
    private CanvasGroup gravityCanvasGroup;
    private GameObject playerDroid;
    void Start() 
    {
        massCanvasGroup = GameObject.Find("MASS").GetComponent<CanvasGroup>();
        gravityCanvasGroup = GameObject.Find("GRAVITY").GetComponent<CanvasGroup>();
        playerDroid = GameObject.Find("Player Droid UI");
    }
    private void OnTriggerEnter(Collider other)
    {
        massCanvasGroup.alpha = 1;
        massCanvasGroup.interactable = true;
        gravityCanvasGroup.alpha = 1;
        playerDroid.GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);
    }
}
