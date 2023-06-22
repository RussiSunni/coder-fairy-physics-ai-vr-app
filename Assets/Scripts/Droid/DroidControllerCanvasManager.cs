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
    public Slider massSlider;

    void Start()
    {
        // Assign camera in script, as canvas is a prefab.
        Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        GetComponent<Canvas>().worldCamera = camera;

        canvasToggle.onValueChanged.AddListener(delegate
        {
            CanvasToggleValueChanged(canvasToggle);
        });

        // Find the game object to fire methods.
        droid = GameObject.Find("Droid Ball");

        // Set the mass slider value.
        massSlider.value = droid.GetComponent<Rigidbody>().mass;
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
    public void ChangeMaxSpeed()
    {       
        droid.GetComponent<TestDroidBehaviour>().ChangeMaxSpeed(maxSpeedSlider.value);
    }

    // Force.    
    public void ChangeForce()
    {
        droid.GetComponent<TestDroidBehaviour>().ChangeForce(forceSlider.value);
    }

    // Mass.
    public void ChangeMass()
    {
        droid.GetComponent<TestDroidBehaviour>().ChangeMass(massSlider.value);
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
