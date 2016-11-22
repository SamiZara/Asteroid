using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileLocker : MonoBehaviour
{

    public static GameObject lockedAsteroid = null;
    public static Transform trans;
    public GameObject lockOnSprite;
    public GameObject lockOnSpritePrefab;
    void Start()
    {
        trans = transform;
        StartCoroutine("LockOnAsteroid");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LockOnAsteroid()
    {
        while (true)
        {
            float distance = float.MaxValue;
            for (int i = 0; i < GeneratorManager.Instance.asteroids.Count; i++)
            {
                float tempDistance = MathHelper.distanceBetween2Points(transform.position, GeneratorManager.Instance.asteroids[i].transform.position);
                if (distance > tempDistance)
                {
                    lockedAsteroid = GeneratorManager.Instance.asteroids[i];
                    distance = tempDistance;
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

    public static void ManuelLockOnAsteroid()
    {
        if (!GameManager.Instance.isGameOver)
        {
            float distance = float.MaxValue;
            for (int i = 0; i < GeneratorManager.Instance.asteroids.Count; i++)
            {
                float tempDistance = MathHelper.distanceBetween2Points(trans.position, GeneratorManager.Instance.asteroids[i].transform.position);
                if (distance > tempDistance)
                {
                    lockedAsteroid = GeneratorManager.Instance.asteroids[i];
                    distance = tempDistance;
                }
            }
        }
    }

    public static List<GameObject> Lock4DifferentAsteroid()
    {
        if (!GameManager.Instance.isGameOver)
        {
            List<GameObject> asteroidList = new List<GameObject>();
            GameObject closestAsteroid = null;
            for (int i = 0; i < 4; i++)
            {
                float distance = float.MaxValue;
                for (int h = 0; h < GeneratorManager.Instance.asteroids.Count; h++)
                {
                    bool flag = false;
                    foreach (GameObject asteroid in asteroidList)
                        if (GeneratorManager.Instance.asteroids[h].transform.position == asteroid.transform.position)
                        {
                            flag = true;
                            break;
                        }
                    float tempDistance = MathHelper.distanceBetween2Points(trans.position, GeneratorManager.Instance.asteroids[h].transform.position);
                    if (distance > tempDistance && !flag)
                    {
                        closestAsteroid = GeneratorManager.Instance.asteroids[h];
                        distance = tempDistance;
                    }
                }
                asteroidList.Add(closestAsteroid);
            }
            return asteroidList;
        }
        return null;
    }
}

