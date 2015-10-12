using UnityEngine;
using System.Collections;
using System;

public class CharachterControlManager : MonoBehaviour {

    public static CharachterControlManager Instance;
    public float rotateSpeed,speed;
    void Start () {
        if (Instance == null)
            Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void rotate()
    {
        var v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        double degree = MathHelper.degreeBetween2Points(transform.position, v3);
        if (degree < 0)
            degree += 360;
        float myRotation = transform.rotation.eulerAngles.z;
        if (myRotation > degree)
        {
            if (Math.Abs(myRotation - degree) > 180)
                transform.rotation = Quaternion.Euler(0, 0, myRotation + rotateSpeed * Time.deltaTime);
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, myRotation - rotateSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (Math.Abs(myRotation - degree) > 180)
                transform.rotation = Quaternion.Euler(0, 0, myRotation - rotateSpeed * Time.deltaTime);
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, myRotation + rotateSpeed * Time.deltaTime);
            }
        }
    }


}
