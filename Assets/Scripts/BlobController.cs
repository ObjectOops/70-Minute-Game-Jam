using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobController : MonoBehaviour
{
    private GameObject pointManager;

    void Start()
    {
        pointManager = GameObject.FindGameObjectWithTag("Finish");
    }

    void FixedUpdate()
    {
        if (transform.position.y < -10)
        {
            pointManager.GetComponent<PointManagerController>().AddPoint();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tomato"))
        {
            pointManager.GetComponent<PointManagerController>().AddPoint();
            Destroy(gameObject);
        }
    }
}
