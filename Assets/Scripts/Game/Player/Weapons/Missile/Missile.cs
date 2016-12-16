using UnityEngine;
using System.Collections;

public class Missile : Weapon
{
    public float cooldown;
    private float lastShootTime;
    private static AudioSource explosionSound;
    public static int missileTier;
    new void Start()
    {
        base.Start();
        missileTier = tier;
        int tempTier = Mathf.CeilToInt((float)tier / 2);
        ResourceManager.Instance.AllocateAndStore("Prefabs/WeaponProjectiles/Missile/MissileProjectileTier"+ tempTier, "MissileProjectile");
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
            if (GameManager.Instance.isSoundOn)
                fireSound.Play();
        }
    }

    public static void PlayProjectileExplosionSound()
    {
        if (GameManager.Instance.isSoundOn)
        {
            if(explosionSound != null)
                explosionSound.Play();
        }
    }
}

