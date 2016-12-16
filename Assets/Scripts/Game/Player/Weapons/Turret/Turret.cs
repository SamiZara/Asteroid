using UnityEngine;
using System.Collections;

public class Turret : Weapon {

    public float cooldown;
    private float lastShootTime;
    private static AudioSource explosionSound;
    public static int turretTier;
    new void Start () {
        base.Start();
        turretTier = tier;
        int tempTier = Mathf.CeilToInt((float)tier/2);
        ResourceManager.Instance.AllocateAndStore("Prefabs/WeaponProjectiles/Turret/TurretProjectileTier" + tempTier, "TurretProjectile");
        GameObject temp2 = (GameObject)Instantiate(explosionSoundObject, new Vector3(0, 0, 0), Quaternion.identity, GlobalsManager.Instance.soundParent.transform);
        explosionSound = temp2.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (canShoot && (lastShootTime + cooldown) < Time.time)
        {
            lastShootTime = Time.time;
            Instantiate(ResourceManager.Instance.storedAllocations["TurretProjectile"], transform.position, transform.parent.rotation);
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
