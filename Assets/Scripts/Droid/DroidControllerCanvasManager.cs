using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DroidControllerCanvasManager : MonoBehaviour
{
    // Show / Hide UI.
    public Toggle canvasToggle;
    public CanvasGroup droidControllerUICanvasGroup;

    // Find the game object to fire methods.
    public GameObject droid;

    public AudioSource audioSource;

    public Slider maxSpeedSlider;
    public Slider forceSlider;


    void Start()
    {
        canvasToggle.onValueChanged.AddListener(delegate
        {
            CanvasToggleValueChanged(canvasToggle);
        });

        // Find the game object to fire methods.
        droid = GameObject.Find("Droid Ball");      
    }

    void CanvasToggleValueChanged(Toggle canvasToggle)
    {
        if (canvasToggle.isOn)
        {
            droidControllerUICanvasGroup.alpha = 1;
            droidControllerUICanvasGroup.interactable = true;
            droidControllerUICanvasGroup.blocksRaycasts = true;
        }
        else
        {
            droidControllerUICanvasGroup.alpha = 0;
            droidControllerUICanvasGroup.interactable = false;
            droidControllerUICanvasGroup.blocksRaycasts = false;
        }
    }


    // Controls are firing methods on the "TestDroidBehaviour" script, as are prefabs.
    // Speed.
    public void IncreaseSpeed()
    {
        droid.GetComponent<TestDroidBehaviour>().IncreaseMaxSpeed();
    }

    public void DecreaseSpeed()
    {
        droid.GetComponent<TestDroidBehaviour>().DecreaseMaxSpeed();
    }

    public void ChangeMaxSpeed()
    {        
        droid.GetComponent<TestDroidBehaviour>().ChangeMaxSpeed(maxSpeedSlider.value);
    }

    // Force.
    public void IncreaseForce()
    {
        droid.GetComponent<TestDroidBehaviour>().IncreaseForce();
    }

    public void DecreaseForce()
    {
        droid.GetComponent<TestDroidBehaviour>().DecreaseForce();
    }

    public void ChangeForce()
    {
        droid.GetComponent<TestDroidBehaviour>().ChangeForce(forceSlider.value);
    }

    // Mass.
    public void DecreaseMass()
    {
        droid.GetComponent<TestDroidBehaviour>().DecreaseMass();      
    }

    public void IncreaseMass()
    {
        droid.GetComponent<TestDroidBehaviour>().IncreaseMass();       
    }

    // Volume.
    public void IncreaseVolume()
    {
        droid.GetComponent<TestDroidBehaviour>().IncreaseVolume();
    }

    public void DecreaseVolume()
    {
        droid.GetComponent<TestDroidBehaviour>().DecreaseVolume();
    }
}
