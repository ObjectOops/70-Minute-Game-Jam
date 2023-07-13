using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float lifetime;

    private float deltaSpawn = 0;

    void Update()
    {
        deltaSpawn += Time.deltaTime;
        if (deltaSpawn >= lifetime || transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
