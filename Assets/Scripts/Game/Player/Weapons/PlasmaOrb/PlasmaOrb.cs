using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaOrb : Weapon {

    public float cooldown;
    private float lastShootTime;
    public static int plasmaOrbTier;
	
	new void Start () {
        base.Start();
        plasmaOrbTier = tier;
        ResourceManager.Instance.AllocateAndStore("Prefabs/WeaponProjectiles/PlasmaOrb/PlasmaOrbProjectile", "PlasmaOrbProjectile");
        ResourceManager.Instance.AllocateAndStore("Prefabs/WeaponProjectiles/PlasmaOrb/PlasmaOrbScatterProjectile", "PlasmaOrbScatterProjectile");
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot && (lastShootTime + cooldown) < Time.time)
        {
            lastShootTime = Time.time;
            Instantiate(ResourceManager.Instance.storedAllocations["PlasmaOrbProjectile"], transform.position, transform.parent.rotation);
            if (GameManager.Instance.isSoundOn)
                fireSound.Play();
        }
    }
}
