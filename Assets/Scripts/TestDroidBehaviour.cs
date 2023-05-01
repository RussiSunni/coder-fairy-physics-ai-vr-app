using TMPro;
using UnityEngine;

public class TestDroidBehaviour : MonoBehaviour
{
    public GameObject goal;
    Vector3 direction;
    public TMP_Text speedText;
    public int speed = 0;
    public TMP_Dropdown directionDropdown;
    private bool isSteadyMoving;
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

    // Rotation.
    private float rotation = 0;
    public TMP_Text rotationText;

    // Sound with collision.
    AudioSource audioSource;


    void Start()
    {
        speedText.text = speed.ToString();

        m_Rigidbody = GetComponent<Rigidbody>();

        // To calculate distance travelled.
        lastPosition = transform.position;

        // To calculate mass.
        mass = m_Rigidbody.mass;
        massText.text = mass.ToString();

        // Rotation.
        rotationText.text = rotation.ToString();

        // Sound.
        audioSource = GetComponent<AudioSource>();
    }



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

    public void DecreaseMass()
    {
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
    }

    public void IncreaseMass()
    {
        mass++;
        m_Rigidbody.mass = mass;
        massText.text = mass.ToString();
    }

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
        if (isSteadyMoving)
        {
            // If droid set to 'Follow'.
            if (directionDropdown.value == 0)
            {
                // Work out direction vector.
                direction = goal.transform.position - transform.position;

                // Face goal.
                transform.LookAt(goal.transform.position);

                if (direction.magnitude > 2)
                {
                    Vector3 ballVelocity = direction.normalized * speed * Time.deltaTime;
                    transform.position = transform.position + ballVelocity;
                }
            }
            else if (directionDropdown.value == 1)
            {
                transform.rotation = Quaternion.Euler(rotation, -Input.compass.magneticHeading, 0);
            }
            else if (directionDropdown.value == 2)
            {
                transform.rotation = Quaternion.Euler(rotation, -Input.compass.magneticHeading + 90, 0);
            }
            else if (directionDropdown.value == 3)
            {
                transform.rotation = Quaternion.Euler(rotation, -Input.compass.magneticHeading + 180, 0);
            }
            else if (directionDropdown.value == 4)
            {
                transform.rotation = Quaternion.Euler(rotation, -Input.compass.magneticHeading - 90, 0);
            }
            // Move.
            Vector3 velocity = transform.forward.normalized * speed * Time.deltaTime;
            transform.position = transform.position + velocity;
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
        isSteadyMoving = false;

        if (directionDropdown.value == 1)
        {
            //transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading, 0);
            m_Rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
        }
        else if (directionDropdown.value == 2)
        {
           //transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading + 90, 0);
            m_Rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
        }
        else if (directionDropdown.value == 3)
        {
            //transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading + 180, 0);
            m_Rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
        }
        else if (directionDropdown.value == 4)
        {
            //transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading - 90, 0);
            m_Rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
        }

    }

    public void Steady()
    {
        if (!isSteadyMoving)
            isSteadyMoving = true;
        else
            isSteadyMoving = false;
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
