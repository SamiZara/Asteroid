﻿using UnityEngine;
using System.Collections;

public class Missile : Weapon
{
    public float cooldown;
    private float lastShootTime;
    new void Start()
    {
        base.Start();
        ResourceManager.Instance.AllocateAndStore("Prefabs/WeaponProjectiles/Missile/MissileProjectileTier"+tier, "MissileProjectile");
        ResourceManager.Instance.AllocateAndStore("Prefabs/WeaponProjectiles/Missile/ScatteredMissileProjectile", "ScatteredMissileProjectile");
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot && (lastShootTime + cooldown) < Time.time && MissileLocker.lockedAsteroid != null)
        {
            lastShootTime = Time.time;
            GameObject projectile = (GameObject)Instantiate(ResourceManager.Instance.storedAllocations["MissileProjectile"], transform.position, transform.parent.rotation);
            sound.Play();
        }
    }


}

