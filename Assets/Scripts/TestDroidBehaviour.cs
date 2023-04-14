using System.Collections;
using TMPro;
using UnityEngine;

public class TestDroidBehaviour : MonoBehaviour
{
    public GameObject goal;
    Vector3 direction;
    public TMP_Text speedText;
    public int speed = 0;

    void Start()
    {
        speedText.text = speed.ToString();
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
}
