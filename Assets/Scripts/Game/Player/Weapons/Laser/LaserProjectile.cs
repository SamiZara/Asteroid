using UnityEngine;
using System.Collections;

public class LaserProjectile : Projectile {

    void OnBecameInvisible()
    {
        Destroy();
    }
}
