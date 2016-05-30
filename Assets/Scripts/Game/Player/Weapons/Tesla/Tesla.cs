﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tesla : Weapon
{

    public List<GameObject> asteroidsInRange = new List<GameObject>();
    public float damage = 3;
    void Start()
    {
        StartCoroutine(PeriodicShock());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Obstacle")
        {
            asteroidsInRange.Add(collider.gameObject);
        }
        else
        {
            Debug.Log("Tesla collided with:" + collider.name);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Obstacle")
        {
            asteroidsInRange.Remove(collider.gameObject);
        }
        else
        {
            Debug.Log("Tesla collided(exit) with:" + collider.name);
        }
    }

    IEnumerator PeriodicShock()
    {
        while (true)
        {
            List<GameObject> tempList = new List<GameObject>(asteroidsInRange);
            foreach (GameObject asteroid in tempList)
            {
                if (asteroid != null)
                {
                    ExplodingAsteroid temp = asteroid.GetComponent<ExplodingAsteroid>();
                    Obstacle temp2 = asteroid.GetComponent<Obstacle>();
                    if (temp != null)
                    {
                        temp.Damage(damage);
                    }
                    else if (temp2 != null)
                    {
                        temp2.Damage(damage);
                    }
                }
                else
                {
                    asteroidsInRange.Remove(asteroid);
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}