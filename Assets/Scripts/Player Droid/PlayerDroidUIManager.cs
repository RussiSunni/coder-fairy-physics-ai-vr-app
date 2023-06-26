using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerDroidUIManager : MonoBehaviour
{
    // Show / Hide UI.
    //public Toggle canvasToggle;
    public CanvasGroup droidControllerUICanvasGroup;
    private bool primaryButtonValue;
    bool isLeftPrimaryButtonPressed;

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

        //canvasToggle.onValueChanged.AddListener(delegate
        //{
        //    CanvasToggleValueChanged(canvasToggle);
        //});

        // Find the game object to fire methods.
        droid = GameObject.Find("Droid Ball");

        // Set the mass slider value.
        massSlider.value = droid.GetComponent<Rigidbody>().mass;
    }

    //void CanvasToggleValueChanged(Toggle canvasToggle)
    //{
    //    if (canvasToggle.isOn)
    //    {
    //        droidControllerUICanvasGroup.alpha = 1;
    //        droidControllerUICanvasGroup.interactable = true;
    //        droidControllerUICanvasGroup.blocksRaycasts = true;
    //    }
    //    else
    //    {
    //        droidControllerUICanvasGroup.alpha = 0;
    //        droidControllerUICanvasGroup.interactable = false;
    //        droidControllerUICanvasGroup.blocksRaycasts = false;
    //    }
    //}


    // Controls are firing methods on the "TestDroidBehaviour" script, as are prefabs.
    // Speed.   
    public void ChangeMaxSpeed()
    {       
        droid.GetComponent<PlayerDroidManager>().ChangeMaxSpeed(maxSpeedSlider.value);
    }

    // Force.    
    public void ChangeForce()
    {
        droid.GetComponent<PlayerDroidManager>().ChangeForce(forceSlider.value);
    }

    // Mass.
    public void ChangeMass()
    {
        droid.GetComponent<PlayerDroidManager>().ChangeMass(massSlider.value);
    }

    // Volume.
    public void IncreaseVolume()
    {
        droid.GetComponent<PlayerDroidManager>().IncreaseVolume();
    }

    public void DecreaseVolume()
    {
        droid.GetComponent<PlayerDroidManager>().DecreaseVolume();
    }


    void LateUpdate()
    {
        // Hide/show controls. 
        if (isLeftPrimaryButtonPressed == false)
        {
            var leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
            var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);
            foreach (var device in leftHandedControllers)
            {
                if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primaryButtonValue) && primaryButtonValue)
                {
                    StartCoroutine(LeftPrimaryButtonPressedCoroutine());
                }
            }
        }
    }
    // Coroutine to provide delay between presses in the update method.
    IEnumerator LeftPrimaryButtonPressedCoroutine()
    {
        isLeftPrimaryButtonPressed = true;

        if (droidControllerUICanvasGroup.alpha == 1)
        {
            droidControllerUICanvasGroup.alpha = 0;
            droidControllerUICanvasGroup.interactable = false;
            droidControllerUICanvasGroup.blocksRaycasts = false;
        }
        else
        {
            droidControllerUICanvasGroup.alpha = 1;
            droidControllerUICanvasGroup.interactable = true;
            droidControllerUICanvasGroup.blocksRaycasts = true;
        }

        yield return new WaitForSeconds(.5f);
        isLeftPrimaryButtonPressed = false;
    }
}
