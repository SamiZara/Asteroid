using UnityEngine;
using System.Collections;

public class Rocket : Weapon
{

    public float cooldown;
    private float lastShootTime;
    new void Start()
    {
        base.Start();
        ResourceManager.Instance.AllocateAndStore("Prefabs/WeaponProjectiles/Rocket/RocketProjectileTier" + tier, "RocketProjectile");
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot && (lastShootTime + cooldown) < Time.time)
        {
            lastShootTime = Time.time;
            Instantiate(ResourceManager.Instance.storedAllocations["RocketProjectile"], transform.position, transform.parent.rotation);
            if (PlayerPrefs.GetInt("Sound", 1) == 1) 
                sound.Play();
        }
    }
}
