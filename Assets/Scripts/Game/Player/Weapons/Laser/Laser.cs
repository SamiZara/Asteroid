using UnityEngine;
using System.Collections;

public class Laser : Weapon
{

    LineRenderer lr;
    public Transform lrAim, laserEffectEnd, laserEffectStart;
    private GameObject player;
    public float laserDuration, cooldown;
    private float damage = 5;
    private float currentLaserTimer = float.MaxValue;
    private bool isLaserCharging;
    new void Start()
    {
        base.Start();
        lr = GetComponent<LineRenderer>();
        player = GlobalsManager.Instance.player;
        ResourceManager.Instance.AllocateAndStore("Prefabs/WeaponProjectiles/Laser/LaserProjectileTier" + tier, "LaserProjectile");
        if (tier == 2)
        {
            lr.SetWidth(0.3f, 0.3f);
            laserEffectEnd.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            laserEffectStart.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            damage = 11f;
        }
        else if (tier == 3)
        {
            lr.SetWidth(0.6f, 0.6f);
            laserEffectEnd.localScale = new Vector3(3, 3, 3);
            laserEffectStart.localScale = new Vector3(3, 3, 3);
            damage = 22.5f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int layerMask = LayerMask.GetMask("Obstacle");
        if (currentLaserTimer < laserDuration)
        {
            lr.enabled = true;
            laserEffectEnd.gameObject.SetActive(true);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(Mathf.Cos(player.transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(player.transform.rotation.eulerAngles.z * Mathf.Deg2Rad)), 25, layerMask);
            lr.SetPosition(0, laserEffectStart.position);
            currentLaserTimer += Time.deltaTime;
            if (hit.collider != null)
            {
                lr.SetPosition(1, hit.point);
                laserEffectEnd.position = hit.point;
                ExplodingAsteroid temp = hit.collider.GetComponent<ExplodingAsteroid>();
                Asteroid temp2 = hit.collider.GetComponent<Asteroid>();
                Obstacle temp3 = hit.collider.GetComponent<Obstacle>();
                if (temp != null)
                {
                    temp.Damage(damage * Time.fixedDeltaTime);
                }
                else if (temp2 != null)
                {
                    temp2.Damage(damage * Time.fixedDeltaTime, MathHelper.degreeBetween2Points(hit.transform.position, transform.position));
                }
                else if (temp3 != null)
                {
                    temp3.Damage(damage);
                }
                else
                {
                    Debug.Log("Something collided with something it should not " + GetComponent<Collider>().name);
                }
            }
            else
            {
                lr.SetPosition(1, lrAim.position);
                laserEffectEnd.position = lrAim.position;
            }
        }
        else if (!isLaserCharging)
        {
            isLaserCharging = true;
            lr.enabled = false;
            laserEffectEnd.gameObject.SetActive(false);
            laserEffectStart.gameObject.SetActive(false);
            StartCoroutine(LaserStarter());
        }
    }

    IEnumerator LaserStarter()
    {
        yield return new WaitForSeconds(cooldown / 2);
        laserEffectStart.gameObject.SetActive(true);
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            sound.Play();
        }
        yield return new WaitForSeconds(cooldown / 2);
        currentLaserTimer = 0;
        isLaserCharging = false;
    }
}
