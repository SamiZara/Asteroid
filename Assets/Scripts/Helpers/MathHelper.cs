using UnityEngine;
using System.Collections;
using System;

public class MathHelper{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static double degreeBetween2Points(Vector3 p1, Vector3 p2)
    {
        float xDiff = p2.x - p1.x;
        float yDiff = p2.y - p1.y;
        return Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
    }
}
