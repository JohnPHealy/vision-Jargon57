using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ButtonContrller : MonoBehaviour
{
                    
    private void FixedUpdate()
    {
        if (Keyboard.current.rKey.isPressed)
        {
            restart();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Proceed()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
