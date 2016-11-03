﻿using UnityEngine;
using System.Collections;

public class RocketProjectile : Projectile
{

    public float aoeDamage;
    public GameObject aoeDamager;
    void Start()
    {
        startTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        float myRotation = transform.rotation.eulerAngles.z;
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * (float)Mathf.Cos(myRotation * Mathf.PI / 180), speed * (float)Mathf.Sin(myRotation * Mathf.PI / 180));
        aoeDamager.GetComponent<RocketAoeDamager>().damage = aoeDamage;
    }

    void Update()
    {
        if (startTime + duration < Time.time)
        {
            Destroy();
        }
    }

    new void Destroy()
    {

        explosionParticle.SetActive(true);
        explosionParticle.transform.parent = transform.parent.parent;

        aoeDamager.SetActive(true);
        aoeDamager.transform.parent = transform.parent.parent;

        Destroy(gameObject);
    }
}
