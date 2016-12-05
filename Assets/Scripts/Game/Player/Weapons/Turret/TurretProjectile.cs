using UnityEngine;
using System.Collections;

public class TurretProjectile : Projectile
{

    public new void Start()
    {
        tier = Turret.turretTier;
        base.Start();
    }

    void OnDestroy()
    {
        if (GameManager.Instance.isSoundOn)
            Turret.PlayProjectileExplosionSound();
    }
}
