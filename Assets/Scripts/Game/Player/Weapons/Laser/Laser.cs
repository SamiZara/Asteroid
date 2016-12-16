using UnityEngine;
using System.Collections;

public class Laser : Weapon
{

    LineRenderer lr;
    public Transform lrAim, laserEffectEnd, laserEffectStart;
    private GameObject player;
    public float laserDuration, cooldown;
    private float damage = 7;
    private float currentLaserTimer = float.MaxValue;
    private bool isLaserCharging;
    private float lastDebriGenerateTime;
    new void Start()
    {
        base.Start();
        lr = GetComponent<LineRenderer>();
        player = GlobalsManager.Instance.player;
        if (tier == 2)
        {
            damage = 10.5f;
        }
        else if (tier == 3)
        {
            lr.startWidth = 0.3f;
            lr.endWidth = 0.3f;
            laserEffectEnd.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            laserEffectStart.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            damage = 14f;
        }
        else if (tier == 4)
        {
            lr.startWidth = 0.3f;
            lr.endWidth = 0.3f;
            laserEffectEnd.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            laserEffectStart.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            damage = 21f;
        }
        else if (tier == 5)
        {
            lr.startWidth = 0.6f;
            lr.endWidth = 0.6f;
            laserEffectEnd.localScale = new Vector3(3, 3, 3);
            laserEffectStart.localScale = new Vector3(3, 3, 3);
            damage = 28f;
        }
        else if (tier == 6)
        {
            lr.startWidth = 0.6f;
            lr.endWidth = 0.6f;
            laserEffectEnd.localScale = new Vector3(3, 3, 3);
            laserEffectStart.localScale = new Vector3(3, 3, 3);
            damage = 42f;
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
                    if (lastDebriGenerateTime + 0.5f < Time.time)
                    {
                        temp3.createDebris(hit.point, MathHelper.degreeBetween2Points(hit.transform.position, hit.point));
                        lastDebriGenerateTime = Time.time;
                    }
                }
                else if (temp2 != null)
                {
                    temp2.Damage(damage * Time.fixedDeltaTime, MathHelper.degreeBetween2Points(hit.transform.position,hit.point),false);
                    if (lastDebriGenerateTime + 0.5f < Time.time)
                    {
                        temp3.createDebris(hit.point, MathHelper.degreeBetween2Points(hit.transform.position, hit.point));
                        lastDebriGenerateTime = Time.time;
                    }
                }
                else if (temp3 != null)
                {
                    temp3.Damage(damage * Time.fixedDeltaTime);
                    if (lastDebriGenerateTime + 0.5f < Time.time)
                    {
                        temp3.createDebris(hit.point, MathHelper.degreeBetween2Points(hit.transform.position, hit.point));
                        lastDebriGenerateTime = Time.time;
                    }
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
        if (GameManager.Instance.isSoundOn)
        {
            fireSound.Play();
        }
        yield return new WaitForSeconds(cooldown / 2);
        currentLaserTimer = 0;
        isLaserCharging = false;
    }
}
