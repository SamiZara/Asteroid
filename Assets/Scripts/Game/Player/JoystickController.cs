using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    bool isRotating = false;
    public GameObject knob;
    public void Update()
    {
        if (isRotating)
        {
            Debug.Log(knob.transform.position);
        }
    }

    public void Rotate()
    {
        isRotating = true;
        Debug.Log("1");
    }

    public void StopRotate()
    {
        isRotating = false;
        Debug.Log("2");
    }
}
