using UnityEngine;
using System.Collections;

public class Asteroid : Obstacle
{
    public bool isMedium,isBig;
    public GameObject smallerAsteroid;
    public float immuneTimer;

    void Awake()
    {
        immuneTimer = Time.time + 0.6f;
    }

    public new void Damage(float damage)
    {
        if (immuneTimer < Time.time)
        {
            hp -= damage;
            if (hp <= 0)
            {
                Destroy();
            }
        }
    }

    private void Destroy()
    {
        explosionParticle.SetActive(true);
        explosionParticle.transform.parent = transform.parent;
        GeneratorManager.Instance.asteroids.Remove(gameObject);
        float asteroidScatterDistance = 0.1f;
        if(smallerAsteroid != null)
        {
            int scatterCount = 3;//Scatter count of smaller asteroids
            for(int i = 0; i < scatterCount; i++){
                Vector3 pos = new Vector3(asteroidScatterDistance * Mathf.Cos((120*i) * Mathf.Deg2Rad), asteroidScatterDistance * Mathf.Sin((120 * i) * Mathf.Deg2Rad), 0) + transform.position;
                GameObject temp = (GameObject)Instantiate(smallerAsteroid, pos, Quaternion.Euler(0,0,120*i));
                temp.GetComponent<Obstacle>().isScatterObject = true;
                temp.GetComponent<Rigidbody2D>().velocity = new Vector2(GlobalsManager.Instance.asteroidSpeed / 3 * Mathf.Cos(120 * i * Mathf.Deg2Rad), GlobalsManager.Instance.asteroidSpeed / 3* Mathf.Sin(120 * i * Mathf.Deg2Rad));
                temp.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(1200, 1800);
                GeneratorManager.Instance.asteroids.Add(temp);
            }
        }
        Destroy(gameObject);
    }

}
