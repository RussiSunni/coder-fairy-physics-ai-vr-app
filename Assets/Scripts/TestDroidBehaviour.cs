using System;
using TMPro;
using UnityEngine;

public class TestDroidBehaviour : MonoBehaviour
{
    public GameObject goal;
    Vector3 direction;
    public TMP_Text speedText;
    public int speed = 0;
    public TMP_Dropdown directionDropdown;
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

    // Sound with collision.
    AudioSource audioSource;


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

        volume = (float)(4.0 / 3 * Math.PI * sphereCollider.radius * sphereCollider.radius * sphereCollider.radius);
        volumeText.text = volume.ToString("#.##");
    }

    public void DecreaseVolume()
    {
        if (sphereCollider.radius > 0.1f)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - 0.1f,
             gameObject.transform.localScale.y - 0.1f,
             gameObject.transform.localScale.z - 0.1f);

            volume = (float)(4.0 / 3 * Math.PI * sphereCollider.radius * sphereCollider.radius * sphereCollider.radius);
            volumeText.text = volume.ToString("#.##");
        }
    }

    // Movement Controls.
    public void DecreaseRotation()
    {
        rotation--;
        transform.Rotate(rotation, 0.0f, 0, Space.Self);
        rotationText.text = rotation.ToString();
    }

    public void IncreaseRotation()
    {
        rotation++;
        transform.Rotate(rotation, 0.0f, 0, Space.Self);
        rotationText.text = rotation.ToString();
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

        // To calculate distance travelled.
        float distance = Vector3.Distance(lastPosition, transform.position);
        totalDistance += distance;
        lastPosition = transform.position;
        //Debug.Log("Total distance travelled:" + totalDistance);
        distanceText.text = totalDistance.ToString("#.##");
    }

    public void Shoot()
    {
        isComeToPlayer = false;
        m_Rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    public void Stop()
    {
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;
        isComeToPlayer = false;
    }
    
    public void ChangeDirection()
    {      
        // Face player.
        if (directionDropdown.value == 0)
        {
            // Work out direction vector.
            direction = goal.transform.position - transform.position;

            // Face goal.
            transform.LookAt(goal.transform.position);
        }
        else if (directionDropdown.value == 1)
        {
            transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading, 0);
        }
        else if (directionDropdown.value == 2)
        {
            transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading + 90, 0);
        }
        else if (directionDropdown.value == 3)
        {
            transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading + 180, 0);
        }
        else if (directionDropdown.value == 4)
        {
            transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading - 90, 0);
        }
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
