using UnityEngine;
using System.Collections;

public class Turret : Weapon {

    public float cooldown;
    private float lastShootTime;
    private static AudioSource explosionSound;
    new void Start () {
        base.Start();
        ResourceManager.Instance.AllocateAndStore("Prefabs/WeaponProjectiles/Turret/TurretProjectileTier" + tier, "TurretProjectile");
        GameObject temp2 = (GameObject)Instantiate(explosionSoundObject, new Vector3(0, 0, 0), Quaternion.identity, GlobalsManager.Instance.soundParent.transform);
        explosionSound = temp2.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (canShoot && (lastShootTime + cooldown) < Time.time)
        {
            lastShootTime = Time.time;
            Instantiate(ResourceManager.Instance.storedAllocations["TurretProjectile"], transform.position, transform.parent.rotation);
            if (PlayerPrefs.GetInt("Sound", 1) == 1)
                fireSound.Play();
        }
    }

    public static void PlayProjectileExplosionSound()
    {
        explosionSound.Play();
    }
}
