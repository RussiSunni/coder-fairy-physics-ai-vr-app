using TMPro;
using UnityEngine;

public class TestDroidBehaviour : MonoBehaviour
{
    public GameObject goal;
    Vector3 direction;
    public TMP_Text speedText;
    public int speed = 0;
    public TMP_Dropdown directionDropdown;
    public TMP_Dropdown movementTypeDropdown;
    private Vector3 north;

    Rigidbody m_Rigidbody;
    private float force = 10f;

    void Start()
    {
        speedText.text = speed.ToString();

        directionDropdown.onValueChanged.AddListener(delegate {
            DirectionDropdownValueChanged(directionDropdown);
        });

        m_Rigidbody = GetComponent<Rigidbody>();
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

    void LateUpdate()
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
                Vector3 velocity = direction.normalized * speed * Time.deltaTime;
                transform.position = transform.position + velocity;
            }
        }     
        else if (movementTypeDropdown.value == 1)
        {
            if (directionDropdown.value == 1)
            {
                transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading, 0);
            } 
            else if (directionDropdown.value == 2)
            {
                transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading + 90, 0);
            }
            else if(directionDropdown.value == 3)
            {
                transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading + 180, 0);
            }
            else if (directionDropdown.value == 4)
            {
                transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading - 90, 0);
            }
            // Move.
            Vector3 velocity = transform.forward.normalized * speed * Time.deltaTime;
            transform.position = transform.position + velocity;
        }
    }

    void DirectionDropdownValueChanged(TMP_Dropdown change)
    {
        if (movementTypeDropdown.value == 0)
        {
            if (change.value == 1)
            {
                transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading, 0);
                m_Rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
            }
            else if (change.value == 2)
            {
                transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading + 90, 0);
                m_Rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
            }
            else if (change.value == 3)
            {
                transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading + 180, 0);
                m_Rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
            }
            else if (change.value == 4)
            {
                transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading - 90, 0);
                m_Rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
            }
        }
    }
}
