using UnityEngine;
using System.Collections;

public class TurretProjectile : Projectile
{

    void OnDestroy()
    {
        if (GameManager.Instance.isSoundOn)
            Turret.PlayProjectileExplosionSound();
    }
}
