using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleRepeller : MonoBehaviour
{

    public List<GameObject> asteroidsInRange = new List<GameObject>();
    public float repelForce;
    // Update is called once per frame
    void Start()
    {
        StartCoroutine(Repeller());
    }

    /*void Update()
    {
        foreach (GameObject asteroid in asteroidsInRange)
        {
            if (asteroid != null)
            {
                float degree = MathHelper.degreeBetween2Points(transform.position, asteroid.transform.position);
                asteroid.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad) * repelForce, Mathf.Sin(degree * Mathf.Deg2Rad) * repelForce));
            }
            else
                asteroidsInRange.Remove(asteroid);
        }
    }*/

    void OnTriggerEnter2D(Collider2D collider)
    {
        asteroidsInRange.Add(collider.gameObject);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        asteroidsInRange.Remove(collider.gameObject);
    }

    IEnumerator Repeller()
    {
        while (true)
        {
            GameObject[] asteroids = asteroidsInRange.ToArray();
            foreach (GameObject asteroid in asteroids)
            {
                if (asteroid != null)
                {
                    float degree = MathHelper.degreeBetween2Points(transform.position, asteroid.transform.position);
                    asteroid.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad) * repelForce, Mathf.Sin(degree * Mathf.Deg2Rad) * repelForce));
                }
                else
                    asteroidsInRange.Remove(asteroid);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
