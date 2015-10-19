using UnityEngine;
using System.Collections;

public class MissileLocker : MonoBehaviour
{

    public static GameObject lockedAsteroid;
    public GameObject lockOnSprite;
    public GameObject lockOnSpritePrefab;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = float.MaxValue;
        for (int i = 0; i < GeneratorManager.Instance.asteroids.Count; i++)
        {
            float tempDistance = distanceBetween2Points(transform.position, GeneratorManager.Instance.asteroids[i].transform.position);
            if (distance > tempDistance)
            {
                lockedAsteroid = GeneratorManager.Instance.asteroids[i];
                if (lockOnSprite == null)
                    lockOnSprite = (GameObject)Instantiate(lockOnSpritePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                lockOnSprite.transform.parent = lockedAsteroid.transform;
                lockOnSprite.transform.localPosition = new Vector3(0, 0, 0);
                distance = tempDistance;
            }
        }
    }

    float distanceBetween2Points(Vector3 p1, Vector3 p2)
    {
        return Mathf.Sqrt(Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.y - p1.y, 2));
    }
}

