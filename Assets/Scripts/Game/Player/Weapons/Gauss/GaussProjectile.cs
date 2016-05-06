using UnityEngine;
using System.Collections;

public class GaussProjectile : MonoBehaviour
{

    public float speed, damage, duration, startTime;
    public GameObject explosionParticle;
    private Rigidbody2D rb;
    void Start()
    {
        startTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        float myRotation = transform.rotation.eulerAngles.z;
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * (float)Mathf.Cos(myRotation * Mathf.PI / 180), speed * (float)Mathf.Sin(myRotation * Mathf.PI / 180));
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
}
