using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;
using System.Collections;

public class TestDroidBehaviour : MonoBehaviour
{
    public GameObject goal;
    Vector3 direction;
    public TMP_Text speedText;
    public int speed = 0;
    //public TMP_Dropdown directionDropdown;
    private bool isComeToPlayer;
    private Vector3 north;

    Rigidbody m_Rigidbody;
    private float force = 10f;

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
    public TMP_Text rotationText;
    private bool primaryButtonValue;
    private bool secondaryButtonValue;

    // Sound with collision.
    AudioSource audioSource;

    // Shoot.
    bool triggerValue;

    // Stop.
    bool gripValue;

    // Rotation.
    bool isButtonPressed = false;

    void Start()
    {
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

        // Rotation.
        rotationText.text = rotation.ToString();

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
        if (isComeToPlayer)
        {
            // Move.
            if (direction.magnitude > 2)
            {
                Vector3 ballVelocity = direction.normalized * speed * Time.deltaTime;
                transform.position = transform.position + ballVelocity;
            }
        }

       
        var rightHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);
        foreach (var device in rightHandedControllers)
        {
            // Shoot (droid shoots forward).
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                isComeToPlayer = false;
                m_Rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
            }
            // Stop.
            else if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue)
            {
                m_Rigidbody.velocity = Vector3.zero;
                m_Rigidbody.angularVelocity = Vector3.zero;
                isComeToPlayer = false;
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

    IEnumerator ButtonPressedCoroutine(string direction)
    {
        isButtonPressed = true;

        if (direction == "up")
            rotation++;
        else
            rotation--;

        transform.Rotate(rotation, 0.0f, 0, Space.Self);
        rotationText.text = rotation.ToString();

        yield return new WaitForSeconds(.1f);

        isButtonPressed = false;
    }



    public void ToPlayer()
    {
        // Work out direction vector.
        direction = goal.transform.position - transform.position;

        // Face goal.
        transform.LookAt(goal.transform.position);

        isComeToPlayer = true;
    }
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
