using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CarBehaviour : MonoBehaviour
{
    public float MaxSpeed;
    public float Power;
    public float Turningangle;
    public AnimationCurve lowSpeedTurnLimiter;
    public AnimationCurve Drag;
    public float initialDrag;
    public float brakingForce;

    float moveDir;
    float rotDir;

    float currentVelocity;
    public float speedAsDec;

    Rigidbody2D rb;

    public Slider SpeedSlider;

    WheelCollider a;

    public float NonSpinningForce;
    public AnimationCurve slideForwards;

    bool isTouchingTrack;
    bool isTouchingGravel;
    bool isTouchingSand;

    bool hasWon;
    public GameObject WinCanvas;
    public Text finalTime;

    Timer timer_;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = initialDrag;

        Application.targetFrameRate = 144;

        timer_ = GetComponent<Timer>();

        Cursor.visible = true;
    }

    private void Update()
    {
        currentVelocity = rb.velocity.magnitude;
        speedAsDec = currentVelocity / MaxSpeed;    
    }

    private void FixedUpdate()
    {
        if (!hasWon)
        {
            if (moveDir > 0)
            {
                rb.drag = Drag.Evaluate(Mathf.Abs(currentVelocity)) * initialDrag;
            }
            else
            {
                rb.drag = Drag.Evaluate(Mathf.Abs(currentVelocity)) * initialDrag * NonSpinningForce;
                // rb.AddForce(transform.up * slideForwards.Evaluate(speedAsDec));
            }

            SpeedSlider.maxValue = MaxSpeed;
            SpeedSlider.value = currentVelocity;

            if (Mathf.Abs(currentVelocity) < MaxSpeed)
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

            if (moveDir > 0)
            {
                rb.AddTorque(-rotDir * lowSpeedTurnLimiter.Evaluate(Mathf.Abs(currentVelocity)) * Turningangle);
            }
            else
            {
                rb.AddTorque(-rotDir * lowSpeedTurnLimiter.Evaluate(Mathf.Abs(currentVelocity)) * Turningangle / 5);
            }

            if (!isTouchingTrack && !isTouchingSand)
            {
                isTouchingGravel = true;
            }
            else
            {
                isTouchingGravel = false;
            }

            if (isTouchingGravel)
            {
                rb.drag *= 2f;
            }
            if (isTouchingSand)
            {
                rb.drag *= 7f;
            }
            if (isTouchingTrack)
            {
                //hdbs
            }
        }

    }

    public void Move(InputAction.CallbackContext ctx)
    {
        moveDir = ctx.ReadValue<Vector2>().y;
        rotDir = ctx.ReadValue<Vector2>().x;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Track"))
        {
            isTouchingTrack = true;
        }

        if (other.gameObject.CompareTag("Sand"))
        {
            isTouchingSand = true;
        }

        if (other.gameObject.CompareTag("Win") && !hasWon)
        {
            Win();
        }

    }

    void Win()
    {
        hasWon = true;
        WinCanvas.SetActive(true);

        timer_.shouldCount = false;
        finalTime.text = "Your Score: " + timer_.value.ToString("0.00") + " seconds!";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouchingTrack = false;
        isTouchingSand = false;
    }
}
