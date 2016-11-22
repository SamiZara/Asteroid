using UnityEngine;
using System.Collections;

public class GaussProjectile : Projectile
{

    void OnDestroy()
    {
        if (GameManager.Instance.isSoundOn)
            Gauss.PlayProjectileExplosionSound();
    }
}
