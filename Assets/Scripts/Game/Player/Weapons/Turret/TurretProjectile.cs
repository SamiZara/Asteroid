using UnityEngine;
using System.Collections;

public class TurretProjectile : MonoBehaviour {

    public float speed, damage, duration, startTime;
    public GameObject explosionParticle;
    private Rigidbody2D rb;
    private bool isReadyToDestroy;
    private float destroyTime;
    void Start()
    {
        startTime = Time.time;
        isReadyToDestroy = false;
        rb = GetComponent<Rigidbody2D>();
        float myRotation = transform.rotation.eulerAngles.z;
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * (float)Mathf.Cos(myRotation * Mathf.PI / 180), speed * (float)Mathf.Sin(myRotation * Mathf.PI / 180));
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
        Vector3 turretProjectile = transform.position;
        if (transform.position.y > GlobalsManager.Instance.screenPos.y)
        {
            transform.position = new Vector3(turretProjectile.x, -turretProjectile.y, turretProjectile.z);
            turretProjectile = transform.position;
        }
        else if (transform.position.y < -GlobalsManager.Instance.screenPos.y)
        {
            transform.position = new Vector3(turretProjectile.x, -turretProjectile.y, turretProjectile.z);
            turretProjectile = transform.position;
        }
        if (transform.position.x > GlobalsManager.Instance.screenPos.x)
        {
            transform.position = new Vector3(-turretProjectile.x, turretProjectile.y, turretProjectile.z);
            turretProjectile = transform.position;
        }
        else if (transform.position.x < -GlobalsManager.Instance.screenPos.x)
        {
            transform.position = new Vector3(-turretProjectile.x, turretProjectile.y, turretProjectile.z);
            turretProjectile = transform.position;
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
            destroyTime = Time.time + 2;
        }
        Destroy(gameObject);
    }
}
