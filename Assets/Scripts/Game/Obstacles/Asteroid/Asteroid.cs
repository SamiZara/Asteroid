using UnityEngine;
using System.Collections;

public class Asteroid : Obstacle
{
    public bool isMedium, isBig;
    public GameObject smallerAsteroid;
    public float immuneTimer;

    void Awake()
    {
        immuneTimer = Time.time + 0.6f;
    }

    public void Damage(float damage, float degree)
    {
        if (immuneTimer < Time.time)
        {
            hp -= damage;
            if (hp <= 0)
            {
                Destroy(degree);
            }
        }
    }

    private void Destroy(float degree)
    {
        degree += 180;
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            GlobalsManager.Instance.asteroidExplosionSound.Play();
        float asteroidScatterDistance = 0.5f;
        if (smallerAsteroid != null)
        {
            int scatterCount = 3;//Scatter count of smaller asteroids
            for (int i = 0; i < scatterCount; i++)
            {
                Vector3 pos = new Vector3(asteroidScatterDistance * Mathf.Cos((degree + ((i - 1) * 20)) * Mathf.Deg2Rad), asteroidScatterDistance * Mathf.Sin((degree + ((i - 1) * 20)) * Mathf.Deg2Rad), 0) + transform.position;
                GameObject temp = (GameObject)Instantiate(smallerAsteroid, pos, Quaternion.Euler(0, 0, degree + ((i - 1) * 20)));
                temp.GetComponent<Obstacle>().isScatterObject = true;
                //Debug.Log(new Vector2(GlobalsManager.Instance.asteroidSpeed * Mathf.Cos((transform.rotation.eulerAngles.z)), GlobalsManager.Instance.asteroidSpeed * Mathf.Sin((transform.rotation.eulerAngles.z))));
                temp.GetComponent<Rigidbody2D>().velocity = new Vector2((GlobalsManager.Instance.asteroidSpeed) * Mathf.Cos(temp.transform.rotation.eulerAngles.z * Mathf.Deg2Rad), (GlobalsManager.Instance.asteroidSpeed)  * Mathf.Sin(temp.transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
                //temp.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(1200, 1800);
                GeneratorManager.Instance.asteroids.Add(temp);
            }
        }
        base.Destroy();
    }
}
