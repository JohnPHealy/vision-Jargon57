using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool shouldCount;
    public float value;
    public long maxValue;

    public Text scoreText;

    bool hasStartedMoving;

    Rigidbody2D rb;

    void Start()
    {
        InvokeRepeating("Count", 0, 0.05f);
        InvokeRepeating("Updater", 0.5f, 0.02f);

        rb = GetComponent<Rigidbody2D>();
    }

    void Updater()
    {
        scoreText.text = value.ToString("0.00");
    }

    void Count()
    {
        if (!hasStartedMoving)
        {
            if (rb.velocity.magnitude > 0)
            {
                hasStartedMoving = true;
                shouldCount = true;
            }
        }

        if (maxValue == 0)
        {
            maxValue = 2147483647;
        }
        if (shouldCount && value < maxValue)
        {
            value += 0.05f;
        }
    }
}
