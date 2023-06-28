using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumePowerUpBehaviour : MonoBehaviour
{
    private CanvasGroup volumeCanvasGroup;
    private CanvasGroup densityCanvasGroup;
    private GameObject playerDroid;
    void Start()
    {
        volumeCanvasGroup = GameObject.Find("VOLUME").GetComponent<CanvasGroup>();
        densityCanvasGroup = GameObject.Find("DENSITY").GetComponent<CanvasGroup>();        
        playerDroid = GameObject.Find("Player Droid UI");
    }
    private void OnTriggerEnter(Collider other)
    {
        volumeCanvasGroup.alpha = 1;
        volumeCanvasGroup.interactable = true;
        densityCanvasGroup.alpha = 1;
        playerDroid.GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);
    }
}
