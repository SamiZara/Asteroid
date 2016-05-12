using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    public float hp;
    public GameObject explosionParticle;
    public Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float degreeToMiddle = MathHelper.degreeBetween2Points(transform.position, new Vector3(0, 0, 0));
        degreeToMiddle = Random.Range(degreeToMiddle - 20, degreeToMiddle + 20);
        rb.velocity = new Vector2(GlobalsManager.Instance.asteroidSpeed * Time.fixedDeltaTime * Mathf.Cos((float)degreeToMiddle * Mathf.Deg2Rad), GlobalsManager.Instance.asteroidSpeed * Time.deltaTime * Mathf.Sin((float)degreeToMiddle * Mathf.Deg2Rad));
    }

    // Update is called once per frame
    void Update () {

    }

    void OnBecameInvisible()
    {
        Vector3 obstaclePos = transform.position;
        if (transform.position.y > GlobalsManager.Instance.screenPos.y)
        {
            transform.position = new Vector3(obstaclePos.x, -obstaclePos.y, obstaclePos.z);
            obstaclePos = transform.position;
        }
        else if (transform.position.y < -GlobalsManager.Instance.screenPos.y)
        {
            transform.position = new Vector3(obstaclePos.x, -obstaclePos.y, obstaclePos.z);
            obstaclePos = transform.position;
        }
        if (transform.position.x > GlobalsManager.Instance.screenPos.x)
        {
            transform.position = new Vector3(-obstaclePos.x, obstaclePos.y, obstaclePos.z);
            obstaclePos = transform.position;
        }
        else if (transform.position.x < -GlobalsManager.Instance.screenPos.x)
        {
            transform.position = new Vector3(-obstaclePos.x, obstaclePos.y, obstaclePos.z);
            obstaclePos = transform.position;
        }
    }

    public void Damage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        explosionParticle.SetActive(true);
        explosionParticle.transform.parent = transform.parent;
        GeneratorManager.Instance.asteroids.Remove(gameObject);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            Debug.Log("Player çarptı");
        }
    }
}
