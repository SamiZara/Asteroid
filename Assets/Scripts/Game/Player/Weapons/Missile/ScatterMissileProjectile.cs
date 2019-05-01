using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScatterMissileProjectile : Projectile
{
    public float rotateSpeed,splitDelay;
    public GameObject target,scatterParticle;
    //private TrailRenderer trainRenderer;
    private bool isReadyToDestroy;
    public new void Start()
    {
        tier = Missile.missileTier;
        startTime = Time.time;
        isReadyToDestroy = false;
        rb = GetComponent<Rigidbody2D>();
        target = MissileLocker.lockedAsteroid;
        if (tier % 2 == 0)
        {
            damage *= 1.5f;
        }
    }

    // Update is called once per frame
    new void Update()
    {
        if (startTime + splitDelay < Time.time)
        {
            Split();
            DestroyWithoutExplosion();
        }

    }

    void FixedUpdate()
    {
        if (target == null)
        {
            MissileLocker.ManuelLockOnAsteroid();
            target = MissileLocker.lockedAsteroid;
            if (target == null)
            {
                Destroy();
            }
        }
        if (!isReadyToDestroy)
        {
            float degree = (float)MathHelper.degreeBetween2Points(transform.position, target.transform.position);
            if (degree < 0)
                degree += 360;
            float myRotation = transform.rotation.eulerAngles.z;
            if (myRotation > degree)
            {
                if (Mathf.Abs(myRotation - degree) < 180)
                    rb.angularVelocity = -rotateSpeed;
                else
                {
                    rb.angularVelocity = rotateSpeed;
                }
            }
            else
            {
                if (Mathf.Abs(myRotation - degree) < 180)
                    rb.angularVelocity = rotateSpeed;
                else
                {
                    rb.angularVelocity = -rotateSpeed;
                }
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * (float)Mathf.Cos(myRotation * Mathf.PI / 180), speed * (float)Mathf.Sin(myRotation * Mathf.PI / 180));
        }
    }

    new void Destroy()
    {
        isReadyToDestroy = true;
        base.Destroy();  
    }

    void DestroyWithoutExplosion()
    {
        isReadyToDestroy = true;
        Destroy(gameObject);
    }

    /*IEnumerator ResetTrailRenderer(TrailRenderer tr)
    {
        tr.time = 0;
        yield return null;
        tr.time = 0.2f;
    }*/

    private void Split()
    {
        scatterParticle.SetActive(true);
        Destroyer temp = scatterParticle.AddComponent<Destroyer>();
        temp.destroyDelayTime = 0.59f;
        scatterParticle.transform.parent = transform.parent.parent;
        List<GameObject> asteroidList = MissileLocker.Lock4DifferentAsteroid();
        if (asteroidList != null)
        {
            GameObject projectile = (GameObject)Instantiate(ResourceManager.Instance.storedAllocations["ScatteredMissileProjectile"], transform.position, transform.parent.rotation);
            for (int i = 1; i < 5; i++)
            {
                if (i < asteroidList.Count)
                    projectile.transform.Find("Projectile" + i).GetComponent<MissileProjectile>().target = asteroidList[i];
                else
                    projectile.transform.Find("Projectile" + i).GetComponent<MissileProjectile>().target = asteroidList[0];
            }
        }
    }
}
