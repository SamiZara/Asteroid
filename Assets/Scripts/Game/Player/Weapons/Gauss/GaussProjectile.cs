using UnityEngine;
using System.Collections;

public class GaussProjectile : Projectile
{
    public new void Start()
    {
        tier = Gauss.gaussTier;
        base.Start();
    }

    void OnDestroy()
    {
        if (GameManager.Instance.isSoundOn)
            Gauss.PlayProjectileExplosionSound();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        startTime = Time.time;
        float hp = collider.GetComponent<Obstacle>().hp;
        GameObject asteroid = null;
        if (damage > hp)
        {
            float distance = float.MaxValue;
            for (int i = 0; i < GeneratorManager.Instance.asteroids.Count; i++)
            {
                if (GeneratorManager.Instance.asteroids[i] != collider.gameObject)
                {
                    float tempDistance = MathHelper.distanceBetween2Points(transform.position, GeneratorManager.Instance.asteroids[i].transform.position);
                    if (distance > tempDistance)
                    {
                        asteroid = GeneratorManager.Instance.asteroids[i];
                        distance = tempDistance;
                    }
                }
            }
            if (asteroid != null)
            {
                float degree = MathHelper.degreeBetween2Points(transform.position, asteroid.transform.position);
                rb.velocity = new Vector2(speed * (float)Mathf.Cos(degree * Mathf.PI / 180), speed * (float)Mathf.Sin(degree * Mathf.PI / 180));
            }
        }
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
        transform.localScale *= (damage - hp) / damage;
        if (transform.localScale.x < 0.4f)
        {
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
        damage -= hp;
        if (damage <= 0)
            Destroy();
    }
}
