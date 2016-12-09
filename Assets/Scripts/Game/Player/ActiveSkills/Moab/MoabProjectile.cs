using UnityEngine;
using System.Collections;

public class MoabProjectile : Projectile {

    public float aoeDamage;
    public GameObject aoeDamager;
    new void Start()
    {
        startTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        float myRotation = transform.rotation.eulerAngles.z;
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * (float)Mathf.Cos(myRotation * Mathf.PI / 180), speed * (float)Mathf.Sin(myRotation * Mathf.PI / 180));
        Destroy();
        aoeDamager.GetComponent<RocketAoeDamager>().damage = aoeDamage;
    }

    new void Update()
    {
        if (startTime + duration < Time.time)
        {
            Destroy();
        }
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
        Destroy();
    }


    new void Destroy()
    {

        explosionParticle.SetActive(true);
        explosionParticle.transform.parent = transform.parent;

        aoeDamager.SetActive(true);
        aoeDamager.transform.parent = transform.parent;

        Destroy(gameObject);
    }
}
