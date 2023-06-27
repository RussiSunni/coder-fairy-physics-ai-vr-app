using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform position1, position2;
    private float _speed = 3.0f;
    private bool _switch = false;

    // Move the platform between the 2 points.
    void FixedUpdate()
    {
        if (_switch == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, position1.position,
                _speed * Time.deltaTime);
        }
        else if (_switch == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, position2.position,
                _speed * Time.deltaTime);
        }

        if (transform.position == position1.position)
        {
            _switch = true;
        }
        else if (transform.position == position2.position)
        {
            _switch = false;
        }
    }

    // So that droid can land and stay on platform.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player droid"))
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player droid"))
        {
            other.transform.parent = null;
        }
    }
}
