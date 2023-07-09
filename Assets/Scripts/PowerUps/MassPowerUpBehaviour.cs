using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassPowerUpBehaviour : MonoBehaviour
{
    private CanvasGroup massCanvasGroup;
    private CanvasGroup densityCanvasGroup;
    private CanvasGroup gravityCanvasGroup;
    private GameObject playerDroid;
    void Start() 
    {
        massCanvasGroup = GameObject.Find("MASS").GetComponent<CanvasGroup>();
        densityCanvasGroup = GameObject.Find("DENSITY").GetComponent<CanvasGroup>();
        gravityCanvasGroup = GameObject.Find("GRAVITY").GetComponent<CanvasGroup>();
        playerDroid = GameObject.Find("Player Droid UI");
    }
    private void OnTriggerEnter(Collider other)
    {
        massCanvasGroup.alpha = 1;
        massCanvasGroup.interactable = true;
        densityCanvasGroup.alpha = 1;
        gravityCanvasGroup.alpha = 1;     
    }
}
