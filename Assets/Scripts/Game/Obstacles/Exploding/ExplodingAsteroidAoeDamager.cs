﻿using UnityEngine;
using System.Collections;

public class ExplodingAsteroidAoeDamager : MonoBehaviour {

    public float damage;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            ExplodingAsteroid temp = collision.GetComponent<ExplodingAsteroid>();
            Asteroid temp2 = collision.GetComponent<Asteroid>();
            Obstacle temp3 = collision.GetComponent<Obstacle>();
            if (temp != null)
            {
                temp.Damage(damage);
            }
            else if (temp2 != null)
            {
                temp2.Damage(damage, MathHelper.degreeBetween2Points(collision.transform.position, transform.position),true);
            }
            else if (temp3 != null)
            {
                temp3.Damage(damage);
            }
            else
            {
                Debug.Log("Something collided with something it should not " + collision.name);
            }
        }
        else if(collision.tag == "Player")
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if(player != null)
            {
                player.Destroy();
            }
            else
            {
                Debug.Log("Something collided with something it should not");
            }
        }
    }
}
