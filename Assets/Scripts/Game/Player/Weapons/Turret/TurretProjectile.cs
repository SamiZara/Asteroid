using UnityEngine;
using System.Collections;

public class TurretProjectile : Projectile
{

    void OnDestroy()
    {
        Turret.PlayProjectileExplosionSound();
    }
}
