using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScatterMissileProjectile : MonoBehaviour
{
    public float speed, rotateSpeed, damage, startTime,splitDelay;
    public GameObject target, explosionParticle,scatterParticle;
    private TrailRenderer trainRenderer;
    private Rigidbody2D rb;
    private bool isReadyToDestroy;
    void Start()
    {
        startTime = Time.time;
        isReadyToDestroy = false;
        trainRenderer = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();
        target = MissileLocker.lockedAsteroid;        
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime + splitDelay < Time.time)
        {
            Split();
            Destroy();
        }

    }

    void OnBecameInvisible()
    {
        Vector3 missilePos = transform.position;
        if (transform.position.y > GlobalsManager.Instance.screenPos.y)
        {
            transform.position = new Vector3(missilePos.x, -missilePos.y, missilePos.z);
            missilePos = transform.position;
            //StartCoroutine("ResetTrailRenderer", trainRenderer);
        }
        else if (transform.position.y < -GlobalsManager.Instance.screenPos.y)
        {
            transform.position = new Vector3(missilePos.x, -missilePos.y, missilePos.z);
            missilePos = transform.position;
            //StartCoroutine("ResetTrailRenderer", trainRenderer);
        }
        if (transform.position.x > GlobalsManager.Instance.screenPos.x)
        {
            transform.position = new Vector3(-missilePos.x, missilePos.y, missilePos.z);
            missilePos = transform.position;
            //StartCoroutine("ResetTrailRenderer", trainRenderer);
        }
        else if (transform.position.x < -GlobalsManager.Instance.screenPos.x)
        {
            transform.position = new Vector3(-missilePos.x, missilePos.y, missilePos.z);
            missilePos = transform.position;
            //StartCoroutine("ResetTrailRenderer", trainRenderer);
        }
    }

    void FixedUpdate()
    {
        if (target == null)
            Destroy();
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        Asteroid temp = collision.GetComponent<Asteroid>();
        if (temp != null)
        {
            temp.Damage(damage);
        }
        else
        {
            Debug.Log("Something collided with something it should not");
        }
        Destroy();
    }

    void Destroy()
    {
        if (explosionParticle != null)
        {
            explosionParticle.SetActive(true);
            explosionParticle.transform.parent = transform.parent.parent;
            Destroyer temp = explosionParticle.AddComponent<Destroyer>();
            temp.destroyDelayTime = 1;
        }
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
        List<GameObject> asteroidList = MissileLocker.Lock4DifferentAsteroid();
        GameObject projectile = (GameObject)Instantiate(ResourceManager.Instance.storedAllocations["ScatteredMissileProjectile"], transform.position, transform.parent.rotation);
        foreach (GameObject x in asteroidList)
            Debug.Log(x.transform.position);
        for(int i = 1; i < 5; i++)
        {
            if (i < asteroidList.Count)
                projectile.transform.FindChild("Projectile" + i).GetComponent<MissileProjectile>().target = asteroidList[i];
            else
                projectile.transform.FindChild("Projectile" + i).GetComponent<MissileProjectile>().target = asteroidList[0];
        }
    }
}
