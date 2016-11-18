using UnityEngine;
using System.Collections;

public class Missile : Weapon
{
    public float cooldown;
    private float lastShootTime;
    private static AudioSource explosionSound;
    new void Start()
    {
        base.Start();
        ResourceManager.Instance.AllocateAndStore("Prefabs/WeaponProjectiles/Missile/MissileProjectileTier"+tier, "MissileProjectile");
        ResourceManager.Instance.AllocateAndStore("Prefabs/WeaponProjectiles/Missile/ScatteredMissileProjectile", "ScatteredMissileProjectile");
        GameObject temp2 = (GameObject)Instantiate(explosionSoundObject, new Vector3(0, 0, 0), Quaternion.identity, GlobalsManager.Instance.soundParent.transform);
        explosionSound = temp2.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot && (lastShootTime + cooldown) < Time.time && MissileLocker.lockedAsteroid != null)
        {
            lastShootTime = Time.time;
            Instantiate(ResourceManager.Instance.storedAllocations["MissileProjectile"], transform.position, transform.parent.rotation);
            if (PlayerPrefs.GetInt("Sound", 1) == 1)
                fireSound.Play();
        }
    }

    public static void PlayProjectileExplosionSound()
    {
        explosionSound.Play();
    }
}

