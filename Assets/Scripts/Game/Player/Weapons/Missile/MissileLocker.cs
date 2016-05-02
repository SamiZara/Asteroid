﻿using UnityEngine;
using System.Collections;

public class MissileLocker : MonoBehaviour
{

    public static GameObject lockedAsteroid;
    public GameObject lockOnSprite;
    public GameObject lockOnSpritePrefab;
    void Start()
    {
        StartCoroutine("ResetTrailRenderer");
    }

    // Update is called once per frame
    void Update()
    {

    }

    float distanceBetween2Points(Vector3 p1, Vector3 p2)
    {
        return Mathf.Sqrt(Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.y - p1.y, 2));
    }

    IEnumerator ResetTrailRenderer()
    {
        while (true)
        {
            float distance = float.MaxValue;
            for (int i = 0; i < GeneratorManager.Instance.asteroids.Count; i++)
            {
                float tempDistance = distanceBetween2Points(transform.position, GeneratorManager.Instance.asteroids[i].transform.position);
                if (distance > tempDistance)
                {
                    lockedAsteroid = GeneratorManager.Instance.asteroids[i];
                    distance = tempDistance;
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
}
