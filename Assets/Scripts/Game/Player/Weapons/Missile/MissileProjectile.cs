using UnityEngine;
using System.Collections;

public class MissileProjectile : MonoBehaviour
{
    public float speed, rotateSpeed, damage, duration, startTime;
    public GameObject target,explosionParticle;
    private TrailRenderer trainRenderer;
    private Rigidbody2D rb;
    private bool isReadyToDestroy;
    private float destroyTime;
    void Start()
    {
        startTime = Time.time;
        isReadyToDestroy = false;
        trainRenderer = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReadyToDestroy && Time.time > destroyTime)
            Destroy(gameObject);
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
        Destroy();
    }

    void Destroy()
    {
        if (explosionParticle != null)
        {
            explosionParticle.SetActive(true);
            explosionParticle.transform.parent = transform.parent;
            Destroyer temp = explosionParticle.AddComponent<Destroyer>();
            temp.destroyDelayTime = 1;
            //isReadyToDestroy = true;
            destroyTime = Time.time + 2;
        }
        Destroy(gameObject);
        /*if (GetComponent<Rigidbody2D>() != null)
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<SpriteRenderer>().sprite = null;
        if (GetComponent<BoxCollider2D>() != null)
            Destroy(GetComponent<BoxCollider2D>());
        if (GetComponent<Rigidbody2D>() != null)
            Destroy(GetComponent<Rigidbody2D>());*/
    }

    IEnumerator ResetTrailRenderer(TrailRenderer tr)
    {
        tr.time = 0;
        yield return null;
        tr.time = 0.2f;
    }
}
