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

    // 
    public GameObject droid;

    void Start()
    {
        canvasToggle.onValueChanged.AddListener(delegate
        {
            CanvasToggleValueChanged(canvasToggle);
        });

        droid = GameObject.Find("Droid Ball");      
    }

    void CanvasToggleValueChanged(Toggle canvasToggle)
    {
        if (canvasToggle.isOn)
        {
            droidControllerUICanvasGroup.alpha = 1;
            droidControllerUICanvasGroup.interactable = true;
        }
        else
        {
            droidControllerUICanvasGroup.alpha = 0;
            droidControllerUICanvasGroup.interactable = false;
        }
    }


    public void IncreaseSpeed()
    {
        droid.GetComponent<TestDroidBehaviour>().IncreaseSpeed();
    }

    public void DecreaseSpeed()
    {
        droid.GetComponent<TestDroidBehaviour>().DecreaseSpeed();
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
