using UnityEngine;
using System.Collections;

public class RocketProjectile : Projectile
{

    public float aoeDamage;
    public GameObject aoeDamager;
    public new void Start()
    {
        tier = Rocket.rocketTier;
        if (tier > 3)
        {
            int diff = tier - 3;
            aoeDamage *= Mathf.Pow(1.20f, diff);
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
        base.Update(); 
    }
    
    void OnDestroy()
    {
        if (GameManager.Instance.isSoundOn)
            Rocket.PlayProjectileExplosionSound();
    }
}
