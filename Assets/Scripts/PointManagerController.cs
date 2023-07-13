using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManagerController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textThing;

    private int points = 0;

    void Start()
    {
        textThing.text = "Points: 0";
    }

    public void AddPoint()
    {
        ++points;
        textThing.text = "Points: " + points;
        if (points == 23)
        {
            textThing.text = "YOU WIN!";
        }
    }
}
