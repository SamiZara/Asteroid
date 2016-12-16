using UnityEngine;
using System.Collections;

public class RocketProjectile : Projectile
{

    public float aoeDamage;
    public GameObject aoeDamager;
    public new void Start()
    {
        tier = Rocket.rocketTier;
        if (tier % 2 == 0)
        {
            aoeDamage *= 1.5f;
        }
        aoeDamager.GetComponent<RocketAoeDamager>().damage = aoeDamage;
        base.Start();
    }

    new void Update()
    {
        if (startTime + duration < Time.time)
        {
            Destroy();
        }
    }

    new void Destroy()
    {
        if (GameManager.Instance.isSoundOn)
            Rocket.PlayProjectileExplosionSound();
        aoeDamager.SetActive(true);
        aoeDamager.transform.parent = transform.parent.parent;
        base.Destroy(); 
    }
    
    void OnDestroy()
    {
        if (GameManager.Instance.isSoundOn)
            Rocket.PlayProjectileExplosionSound();
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
