using UnityEngine;
using System.Collections;

public class Rocket : Weapon
{

    public float tier;
    public float cooldown;
    private float lastShootTime;
    void Start()
    {
        ResourceManager.Instance.AllocateAndStore("Prefabs/WeaponProjectiles/Rocket/RocketProjectileTier" + tier, "RocketProjectile");
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot && (lastShootTime + cooldown) < Time.time)
        {
            lastShootTime = Time.time;
            GameObject projectile = (GameObject)Instantiate(ResourceManager.Instance.storedAllocations["RocketProjectile"], transform.position, transform.parent.rotation);
        }
    }
}
