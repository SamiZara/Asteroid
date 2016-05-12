using UnityEngine;
using System.Collections;
using UnityEditor;

public class ExplodingAsteroid : Obstacle
{

    public GameObject aoeDamager;

    public new void Damage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        explosionParticle.SetActive(true);
        explosionParticle.transform.parent = transform.parent;
        GeneratorManager.Instance.asteroids.Remove(gameObject);

        aoeDamager.transform.parent = transform.parent;
        aoeDamager.SetActive(true);
        Debug.Log("3");

        Destroy(gameObject);
    }
}
