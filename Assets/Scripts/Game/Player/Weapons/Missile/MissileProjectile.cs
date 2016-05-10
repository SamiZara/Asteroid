using UnityEngine;
using System.Collections;

public class MissileProjectile : MonoBehaviour
{
    public float speed, rotateSpeed, damage, duration, startTime;
    public GameObject target, explosionParticle;
    private TrailRenderer trainRenderer;
    private Rigidbody2D rb;
    public bool autoLock;
    private bool isReadyToDestroy = false;
    void Start()
    {
        startTime = Time.time;
        trainRenderer = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();
        if (autoLock)
            target = MissileLocker.lockedAsteroid;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime + duration < Time.time)
        {
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
        {
            Debug.Log("null");
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacle temp = collision.GetComponent<Obstacle>();
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
        explosionParticle.SetActive(true);
        explosionParticle.transform.parent = transform.parent.parent;
        Destroyer temp = explosionParticle.AddComponent<Destroyer>();
        temp.destroyDelayTime = 1;
        isReadyToDestroy = true;
        Destroy(gameObject);
    }

    /*IEnumerator ResetTrailRenderer(TrailRenderer tr)
    {
        tr.time = 0;
        yield return null;
        tr.time = 0.2f;
    }*/
}
