using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarBehaviour : MonoBehaviour
{
    public float MaxSpeed;
    public float Power;
    public float Turningangle;
    public float TurningReducer;
    public AnimationCurve lowSpeedTurnLimiter;
    public float brakingForce;

    float moveDir;
    float rotDir;

    float currentVelocity;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        currentVelocity = rb.velocity.magnitude;
    }

    private void FixedUpdate()
    {


        if (Mathf.Abs(rb.velocity.magnitude) < MaxSpeed)
        {
            if (moveDir < 0 && currentVelocity > 0)
            {
                rb.AddForce(transform.up * moveDir * brakingForce);
            }
            else
            {
                rb.AddForce(transform.up * moveDir * Power);
            }
        }

        float turningRed = (1 / currentVelocity) * TurningReducer * lowSpeedTurnLimiter.Evaluate(currentVelocity);
        

        if (turningRed < 0.00001f)
        {
            turningRed = 0;
        }
        Debug.Log(turningRed);
        rb.AddTorque(-rotDir * Turningangle * turningRed);
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        moveDir = ctx.ReadValue<Vector2>().y;
        rotDir = ctx.ReadValue<Vector2>().x;
    }
}
