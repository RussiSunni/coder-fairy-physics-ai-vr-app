using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using System;

public class MoveDroid : MonoBehaviour
{    
    Vector3 moveDirection;
    public Camera mainCamera;
    Rigidbody m_Rigidbody;
    public TMP_Text speedText;
    
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        // Get gameobjects for droid prefab.
        speedText = GameObject.Find("Speed text").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        int speed = Int32.Parse(speedText.text); 

        var rightHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);

        foreach (var device in rightHandedControllers)
        {                  
            if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
            {
                // First stop any other movement, like from a previous "shoot".
                m_Rigidbody.velocity = Vector3.zero;
                m_Rigidbody.angularVelocity = Vector3.zero;              

                float X = primary2DAxisValue.x;
                float Y = primary2DAxisValue.y;

                Vector3 forward = Vector3.Normalize(Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up));
                Vector3 right = Vector3.Normalize(Vector3.ProjectOnPlane(mainCamera.transform.right, Vector3.up));

                moveDirection = Vector3.Normalize((X * right) + (Y * forward));
                transform.rotation = Quaternion.LookRotation(moveDirection);

                transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
            }
        } 
    }
}
