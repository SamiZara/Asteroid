using UnityEngine;
using System.Collections;

public class Laser : Weapon {

    LineRenderer lr;
    public GameObject lrAim;
    private GameObject player;
    public float cooldown,lastShootTime;
	void Start () {
        lr = GetComponent<LineRenderer>();
        player = GlobalsManager.Instance.player;
        ResourceManager.Instance.AllocateAndStore("Prefabs/WeaponProjectiles/Laser/LaserProjectileTier" + tier, "LaserProjectile");
        if(tier == 2)
        {
            lr.SetWidth(0.3f, 0.3f);
        }
        else if (tier == 3)
        {
            lr.SetWidth(0.6f, 0.6f);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        int layerMask = LayerMask.GetMask("Obstacle");
        Vector3 dir = Quaternion.AngleAxis(player.transform.rotation.eulerAngles.z, Vector3.forward) * transform.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(Mathf.Cos(player.transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(player.transform.rotation.eulerAngles.z * Mathf.Deg2Rad)),25,layerMask);
        lr.SetPosition(0, GlobalsManager.Instance.player.transform.position);
        if (hit.collider != null)
        {
            lr.SetPosition(1, hit.point);
        }
        else
        {
            lr.SetPosition(1, lrAim.transform.position);
        }
    }

    void Update()
    {
        if (canShoot && (lastShootTime + cooldown) < Time.time)
        {
            lastShootTime = Time.time;
            GameObject projectile = (GameObject)Instantiate(ResourceManager.Instance.storedAllocations["LaserProjectile"], transform.position, transform.parent.rotation);
        }
    }
}
