using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    float currentVelocity;
    public AnimationCurve LightEffector;
    public AnimationCurve pointLighteffector;
    public AnimationCurve cameraPan;

    CarBehaviour car;

    Rigidbody2D rb;

    public Light2D light1;
    public Light2D light2;

    float speedasDecimal;

    public CinemachineVirtualCamera cam;

    public float lightWideness;
    public float pointLightWideness;
    public float CameraZoom;

    public Transform mousePosGameObject;
    Vector2 MousePos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        car = GetComponent<CarBehaviour>();
    }

    private void Update()
    {
        currentVelocity = Mathf.Abs(rb.velocity.magnitude);

        speedasDecimal = currentVelocity / car.MaxSpeed;
    }

    private void FixedUpdate()
    {
        /*
        MousePos = Mouse.current.position.ReadValue();

        MousePos = Camera.main.ScreenToWorldPoint(MousePos);

        Vector3 worldPos = new Vector3();
        worldPos.x = MousePos.x;
        worldPos.y = MousePos.y;

        mousePosGameObject.position = worldPos;

        light1.transform.LookAt(mousePosGameObject);
        light2.transform.LookAt(mousePosGameObject);

        Quaternion offset = new Quaternion();

        if (light1.transform.rotation.eulerAngles.x < 0)
        {
            offset = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            offset = Quaternion.Euler(0, -90, 0);
        }

        light1.transform.rotation *= offset;
        light2.transform.rotation *= offset;

        */
        light1.pointLightOuterRadius = Mathf.Lerp(light1.pointLightOuterRadius, LightEffector.Evaluate(speedasDecimal) * lightWideness, 0.05f);
        light1.pointLightInnerRadius = light1.pointLightOuterRadius / 2;

        light2.pointLightOuterRadius = Mathf.Lerp(light2.pointLightOuterRadius, pointLighteffector.Evaluate(speedasDecimal) * pointLightWideness, 0.05f);
        light2.pointLightInnerRadius = light2.pointLightOuterRadius / 2;

        cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, cameraPan.Evaluate(speedasDecimal) * CameraZoom, 0.05f);
    }


}
