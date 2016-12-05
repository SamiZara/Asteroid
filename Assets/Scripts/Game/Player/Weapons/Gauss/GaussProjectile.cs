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
}
