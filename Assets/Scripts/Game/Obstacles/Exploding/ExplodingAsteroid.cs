using UnityEngine;
using System.Collections;

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

    private new void Destroy()
    {
        aoeDamager.transform.parent = transform.parent;
        aoeDamager.SetActive(true);
        base.Destroy();
        Destroy(gameObject);
    }
}
