using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;
using System.Collections;

public class TestDroidBehaviour : MonoBehaviour 
{
    public GameObject goal;
    public TMP_Text speedText;
    public int speed = 0;        

    Rigidbody m_Rigidbody;

    // Turns.
    public TMP_Text turnCounterText;
    private int turnCounter = 0;

    // To calculate distance travelled.
    private Vector3 lastPosition;
    private float totalDistance;
    public TMP_Text distanceText;

    // Mass.
    public TMP_Text massText;
    public float mass;

    // Volume.
    SphereCollider sphereCollider;
    public TMP_Text volumeText;
    private float volume;

    // Density.
    public TMP_Text densityText;
    public float density;    

    // Rotation.
    private float rotation = 0;    
    private bool primaryButtonValue;
    private bool secondaryButtonValue;

    // Sound with collision.
    public AudioSource audioSource;

    // Shoot.
    bool isRightTriggerPressed;
    bool triggerValue;

    // Stop.
    bool gripValue;

    // Rotation.
    bool isButtonPressed = false;

    void Start()
    {
        // Get gameobjects for droid prefab.
        turnCounterText = GameObject.Find("Move counter text").GetComponent<TextMeshProUGUI>();
        speedText = GameObject.Find("Speed text").GetComponent<TextMeshProUGUI>();
        distanceText = GameObject.Find("Distance text").GetComponent<TextMeshProUGUI>();
        massText = GameObject.Find("Mass text").GetComponent<TextMeshProUGUI>();
        volumeText = GameObject.Find("Volume text").GetComponent<TextMeshProUGUI>();
        densityText = GameObject.Find("Density text").GetComponent<TextMeshProUGUI>();        

        speedText.text = speed.ToString();  
           
        // To calculate distance travelled.
        lastPosition = transform.position;        

        // Volume.
        // Get radius.
        m_Rigidbody = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        // Work out volume.
        volume = (float)(4.0 / 3 * Math.PI * sphereCollider.radius * sphereCollider.radius * sphereCollider.radius);
        volumeText.text = volume.ToString("#.##");

        // To calculate mass.
        mass = m_Rigidbody.mass;
        massText.text = mass.ToString();

        // Density.
        density = mass / volume;
        densityText.text = density.ToString("#.##");       

        // Sound.
        audioSource = GetComponent<AudioSource>();
    }

    // Speed.
    public void IncreaseSpeed()
    {
        speed++;
        speedText.text = speed.ToString();
    }

    public void DecreaseSpeed()
    {
        if (speed > 0)
        {
            speed--;
            speedText.text = speed.ToString();
        }
    }

    // Mass.
    public void DecreaseMass()
    {
        // Mass.
        if (mass >= 2f)
        {         
            mass--;
            m_Rigidbody.mass = mass;
            massText.text = mass.ToString("#.##");            
        }
        else if (mass > 0.1f)
        {            
            mass = mass - 0.1f;
            m_Rigidbody.mass = mass;
            massText.text = mass.ToString("#.##");            
        }

        // Density.
        density = mass / volume;
        densityText.text = density.ToString("#.##");
    }

    public void IncreaseMass()
    {
        // Mass.
        mass++;
        m_Rigidbody.mass = mass;
        massText.text = mass.ToString();

        // Density.
        density = mass / volume;
        densityText.text = density.ToString("#.##");
    }

    // Volume.
    public void IncreaseVolume()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + 0.1f,
            gameObject.transform.localScale.y + 0.1f,
            gameObject.transform.localScale.z + 0.1f);

        volume = (float)(4.0 / 3 * Math.PI * ((gameObject.transform.localScale.x / 2) * (gameObject.transform.localScale.x / 2) * (gameObject.transform.localScale.x / 2)));
        volumeText.text = volume.ToString("#.##");

        CalculateDensity();
    }

    public void DecreaseVolume()
    {
        if (gameObject.transform.localScale.x > 0.1f)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - 0.1f,
             gameObject.transform.localScale.y - 0.1f,
             gameObject.transform.localScale.z - 0.1f);

            volume = (float)(4.0 / 3 * Math.PI * ((gameObject.transform.localScale.x / 2) * (gameObject.transform.localScale.x / 2) * (gameObject.transform.localScale.x / 2)));
            volumeText.text = volume.ToString("#.##");

            CalculateDensity();
        }
    }

    // Work out density.
    public void CalculateDensity()
    {
        density = mass / volume;
        densityText.text = density.ToString("#.##");
    }
      

    void LateUpdate()
    {
        // Movement controls. 
        var rightHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);
        foreach (var device in rightHandedControllers)
        {
            // Shoot (droid shoots forward).
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                if (isRightTriggerPressed == false)
                    StartCoroutine(RightTriggerPressedCoroutine());                
            }
            // Stop.
            else if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue)
            {
                m_Rigidbody.velocity = Vector3.zero;
                m_Rigidbody.angularVelocity = Vector3.zero;                
            }
            // Rotation.
            else if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primaryButtonValue) && primaryButtonValue)
            {
                if (isButtonPressed == false)
                    StartCoroutine(ButtonPressedCoroutine("up"));              
            }
            else if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out secondaryButtonValue) && secondaryButtonValue)
            {
                if (isButtonPressed == false)
                    StartCoroutine(ButtonPressedCoroutine("down"));
            }
        }

        // To calculate distance travelled.
        float distance = Vector3.Distance(lastPosition, transform.position);
        totalDistance += distance;
        lastPosition = transform.position;
        //Debug.Log("Total distance travelled:" + totalDistance);
        distanceText.text = totalDistance.ToString("#.##");
    }

    IEnumerator RightTriggerPressedCoroutine()
    {
        isRightTriggerPressed = true;
        m_Rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
        turnCounter++;
        turnCounterText.text = turnCounter.ToString();
        yield return new WaitForSeconds(.5f);
        isRightTriggerPressed = false;
    }

    IEnumerator ButtonPressedCoroutine(string direction)
    {
        isButtonPressed = true;

        if (direction == "up")
            rotation++;
        else
            rotation--;

        transform.Rotate(rotation, 0.0f, 0, Space.Self);        

        yield return new WaitForSeconds(.5f);

        isButtonPressed = false;
    }

    // Play sound on collision.
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        if (collision.relativeVelocity.magnitude > 2)
            audioSource.Play();
    }
}
