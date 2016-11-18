using UnityEngine;
using System.Collections;

public class GaussProjectile : Projectile
{

    void OnDestroy()
    {
        Gauss.PlayProjectileExplosionSound();
    }
}
