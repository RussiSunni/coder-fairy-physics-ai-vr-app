using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using System;

public class PlayerDroidMoveBehaviour : MonoBehaviour
{
    Vector3 moveDirection;
    public Camera mainCamera;
    Rigidbody m_Rigidbody;

    // Speed.
    public float speed;
    public TMP_Text speedText;

    // Find the game object to fire methods.
    public GameObject droid;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        // Get gameobjects for droid prefab.
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        speedText = GameObject.Find("Speed text").GetComponent<TextMeshProUGUI>();
        // Find the game object to fire methods.
        droid = GameObject.Find("Droid Ball");
    }

    void Update()
    {
        speedText.text = speed.ToString("#.##");

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

                // Get the direction from the joystick.
                float X = primary2DAxisValue.x;
                float Y = primary2DAxisValue.y;

                Vector3 forward = Vector3.Normalize(Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up));
                Vector3 right = Vector3.Normalize(Vector3.ProjectOnPlane(mainCamera.transform.right, Vector3.up));

                moveDirection = Vector3.Normalize((X * right) + (Y * forward));
                transform.rotation = Quaternion.LookRotation(moveDirection);

                // Move.
                if (speed <= droid.GetComponent<PlayerDroidManager>().maxSpeed) 
                    speed += droid.GetComponent<PlayerDroidManager>().acceleration * Time.deltaTime;

                transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
            }
            else
            {
                // Deceleration.
                if (speed > 0)
                {
                    speed = speed - (speed * 0.01f);
                    transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
                }
            }

        }
    }
}
