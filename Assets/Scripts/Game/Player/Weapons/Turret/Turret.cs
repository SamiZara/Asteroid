using UnityEngine;
using System.Collections;

public class Turret : Weapon {

    public float tier = 1;
    public float cooldown;
    private float lastShootTime;
    void Start () {
        ResourceManager.Instance.AllocateAndStore("Prefabs/TurretProjectileTier"+tier, "TurretProjectile");
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("1");
        if (canShoot && (lastShootTime + cooldown) < Time.time)
        {
            Debug.Log("2");
            lastShootTime = Time.time;
            GameObject projectile = (GameObject)Instantiate(ResourceManager.Instance.storedAllocations["TurretProjectile"], transform.position, transform.parent.rotation);
            /*projectileScript.damage = 5;
            projectileScript.duration = 3;
            projectileScript.rotateSpeed = 120;
            projectileScript.speed = 4;*/
        }
    }
}
