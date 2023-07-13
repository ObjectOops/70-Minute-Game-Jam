using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDestroyer : MonoBehaviour
{

    void FixedUpdate()
    {
        if (transform.position.y < -100)
        {
            Destroy(gameObject);
        }
    }
}
