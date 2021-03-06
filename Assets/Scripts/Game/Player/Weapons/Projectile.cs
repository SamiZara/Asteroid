﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float speed, damage, duration, startTime;
    public GameObject explosionParticle;
    public Rigidbody2D rb;
    public AudioSource sound;
    public int tier;
    public void Start()
    {
        startTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        float myRotation = transform.rotation.eulerAngles.z;
        rb.velocity = new Vector2(speed * (float)Mathf.Cos(myRotation * Mathf.PI / 180), speed * (float)Mathf.Sin(myRotation * Mathf.PI / 180));
        if(tier%2 == 0)
        {
            damage *= 1.5f;
        }
    }

    // Update is called once per frame
    public void Update()
    { 
        if (startTime + duration < Time.time)
        {
            Destroy();
        }
    }

    void OnBecameInvisible()
    {
        Vector3 turretProjectile = transform.position;
        if (transform.position.y > GlobalsManager.Instance.screenPos.y)
        {
            transform.position = new Vector3(turretProjectile.x, -turretProjectile.y, turretProjectile.z);
            turretProjectile = transform.position;
        }
        else if (transform.position.y < -GlobalsManager.Instance.screenPos.y)
        {
            transform.position = new Vector3(turretProjectile.x, -turretProjectile.y, turretProjectile.z);
            turretProjectile = transform.position;
        }
        if (transform.position.x > GlobalsManager.Instance.screenPos.x)
        {
            transform.position = new Vector3(-turretProjectile.x, turretProjectile.y, turretProjectile.z);
            turretProjectile = transform.position;
        }
        else if (transform.position.x < -GlobalsManager.Instance.screenPos.x)
        {
            transform.position = new Vector3(-turretProjectile.x, turretProjectile.y, turretProjectile.z);
            turretProjectile = transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Obstacle")
        {
            ExplodingAsteroid temp = collider.GetComponent<ExplodingAsteroid>();
            Asteroid temp2 = collider.GetComponent<Asteroid>();
            Obstacle temp3 = collider.GetComponent<Obstacle>();         
            if (temp != null)
            {
                temp.Damage(damage);
                temp3.createDebris(transform.position, MathHelper.degreeBetween2Points(collider.transform.position, transform.position));
            }
            else if (temp2 != null)
            {
                temp2.Damage(damage, MathHelper.degreeBetween2Points(collider.transform.position, transform.position),false);
                temp3.createDebris(transform.position, MathHelper.degreeBetween2Points(collider.transform.position, transform.position));
            }
            else if (temp3 != null)
            {
                temp3.Damage(damage);
                temp3.createDebris(transform.position, MathHelper.degreeBetween2Points(collider.transform.position, transform.position));
            }
            else
            {
                Debug.Log("Something collided with something it should not " + collider.name);
            }
        }
        Destroy();
    }

    public void Destroy()
    {
        if (explosionParticle != null)
        {
            explosionParticle.SetActive(true);
            explosionParticle.transform.parent = transform.parent.parent;
            Destroyer temp = explosionParticle.AddComponent<Destroyer>();
            temp.destroyDelayTime = 1;
        }
        Destroy(gameObject);
    }
}
