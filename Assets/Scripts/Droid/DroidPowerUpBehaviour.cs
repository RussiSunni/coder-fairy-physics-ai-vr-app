using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidPowerUpBehaviour : MonoBehaviour
{
    public CanvasGroup speedCanvasGroup;
    public Collider testPowerUp;
    void Start()
    {
        speedCanvasGroup = GameObject.Find("SPEED").GetComponent<CanvasGroup>();
        testPowerUp = GameObject.Find("PowerUp").GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == testPowerUp)
        {
            speedCanvasGroup.alpha = 1;
            speedCanvasGroup.interactable = true;
        }
    }
}
