﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tesla : Weapon
{

    public List<GameObject> asteroidsInRange = new List<GameObject>();
    public GameObject lightningEffect;
    public float damage = 3;
    new void Start()
    {
        base.Start();
        StartCoroutine(PeriodicShock());
        if(tier == 2)
        {
            GetComponent<CircleCollider2D>().radius = 2.4f;
            damage = 4;
        }
        if (tier == 3)
        {
            GetComponent<CircleCollider2D>().radius = 3f;
            damage = 5;
        }
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
            if (tempList.Count != 0)
                sound.Play();
            foreach (GameObject asteroid in tempList)
            {
                if (asteroid != null)
                {
                    ExplodingAsteroid temp = asteroid.GetComponent<ExplodingAsteroid>();
                    Asteroid temp2 = asteroid.GetComponent<Asteroid>();
                    Obstacle temp3 = asteroid.GetComponent<Obstacle>();
                    if (temp != null)
                    {
                        temp.Damage(damage);
                    }
                    else if (temp2 != null)
                    {
                        temp2.Damage(damage);
                    }
                    else if (temp3 != null)
                    {
                        temp3.Damage(damage);
                    }
                    else
                    {
                        Debug.Log("Something collided with something it should not " + GetComponent<Collider>().name);
                    }
                    GameObject effect = (GameObject)Instantiate(lightningEffect, transform.parent);
                    effect.transform.FindChild("Destination").transform.position = asteroid.transform.position;
                    effect.transform.FindChild("Destination").transform.parent = asteroid.transform;
                    effect.transform.FindChild("Source").transform.position = transform.position;
                    //effect.GetComponent<Lightn>
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
