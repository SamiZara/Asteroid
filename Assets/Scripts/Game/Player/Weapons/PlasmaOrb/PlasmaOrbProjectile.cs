using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaOrbProjectile : Projectile {

    private float rotateSpeed,delayBetweenScatter;

	// Use this for initialization
	new void Start () {
        tier = PlasmaOrb.plasmaOrbTier;
        if (tier == 1)
        {
            rotateSpeed = 90;
            delayBetweenScatter = 0.1125f;
            damage = 1;
        }
        if (tier == 2)
        {
            rotateSpeed = 90;
            delayBetweenScatter = 0.1125f;
            damage = 3;
        }
        if (tier == 3)
        {
            rotateSpeed = 135;
            delayBetweenScatter = 0.09f;
            damage = 3;
        }
        if (tier == 4)
        {
            rotateSpeed = 135;
            delayBetweenScatter = 0.09f;
            damage = 5;
        }
        if (tier == 5)
        {
            rotateSpeed = 180;
            delayBetweenScatter = 0.075f;
            damage = 5;
        }
        if (tier == 6)
        {
            rotateSpeed = 180;
            delayBetweenScatter = 0.075f;
            damage = 7;
        }
        startTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        float myRotation = transform.rotation.eulerAngles.z;
        rb.velocity = new Vector2(speed * (float)Mathf.Cos(myRotation * Mathf.PI / 180), speed * (float)Mathf.Sin(myRotation * Mathf.PI / 180));
        rb.angularVelocity = rotateSpeed;
        ScatterProjectile();
    }
	
    private void ScatterProjectile()
    {
        Instantiate(ResourceManager.Instance.storedAllocations["PlasmaOrbScatterProjectile"], transform.position, transform.rotation).GetComponent<PlasmaOrbScatterProjectile>().damage = damage;
        Invoke("ScatterProjectile", delayBetweenScatter);
    }

    public new void Update()
    {
        if (startTime + duration < Time.time)
        {
            Destroy();
        }
    }

    public new void Destroy()
    {
        if (explosionParticle != null)
        {
            explosionParticle.SetActive(true);
            explosionParticle.transform.parent = transform.parent;
            Destroyer temp = explosionParticle.AddComponent<Destroyer>();
            temp.destroyDelayTime = 1;
        }
        Destroy(gameObject);
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
                temp2.Damage(damage, MathHelper.degreeBetween2Points(collider.transform.position, transform.position), false);
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
}
