using UnityEngine;
using System.Collections;

public class Missile : Weapon
{
    public float tier = 1;
    public float cooldown;
    private float lastShootTime;
    void Start()
    {
        ResourceManager.Instance.AllocateAndStore("Prefabs/WeaponProjectiles/MissileProjectileTier"+tier, "MissileProjectile");
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot && (lastShootTime + cooldown) < Time.time && MissileLocker.lockedAsteroid != null)
        {
            lastShootTime = Time.time;
            GameObject projectile = (GameObject)Instantiate(ResourceManager.Instance.storedAllocations["MissileProjectile"], transform.position, transform.parent.rotation);
            MissileProjectile projectileScript = projectile.GetComponent<MissileProjectile>();
            /*projectileScript.damage = 5;
            projectileScript.duration = 3;
            projectileScript.rotateSpeed = 120;
            projectileScript.speed = 4;*/
        }
    }


}

