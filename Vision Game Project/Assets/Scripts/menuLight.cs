using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class menuLight : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        Vector3 mouse = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;
        }
    }
}
