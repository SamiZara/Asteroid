using UnityEngine;
using System.Collections;
using System;

public class CharachterControlManager : MonoBehaviour {

    public static CharachterControlManager Instance;
    public float rotateSpeed,speed;
	public GameObject player;
	private Rigidbody2D playerRb;
    void Start () {
        if (Instance == null)
            Instance = this;
		playerRb = player.GetComponent<Rigidbody2D> (); 
	}
	
	// Update is called once per frame
	void Update () {
		playerRb.AddForce (new Vector2(speed * Time.deltaTime * Mathf.Cos(player.transform.rotation.eulerAngles.z * Mathf.Deg2Rad),speed * Time.deltaTime * Mathf.Sin(player.transform.rotation.eulerAngles.z * Mathf.Deg2Rad)));
	}

    public void rotate()
    {
        var v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        double degree = MathHelper.degreeBetween2Points(player.transform.position, v3);
		if (degree < 0)
			degree += 360;
        float myRotation = player.transform.rotation.eulerAngles.z;
		Debug.Log (degree+","+myRotation);
		if (myRotation > degree)
        {
			if(Math.Abs(myRotation - degree) < 180)
				playerRb.angularVelocity = -rotateSpeed;
			else
				playerRb.angularVelocity = rotateSpeed;
        }
        else
        {
			if(Math.Abs(myRotation - degree) < 180)
				playerRb.angularVelocity = +rotateSpeed;
			else
				playerRb.angularVelocity = -rotateSpeed;
        }
    }

	public void stopRotate()
	{
		playerRb.angularVelocity = 0;
	}
}
