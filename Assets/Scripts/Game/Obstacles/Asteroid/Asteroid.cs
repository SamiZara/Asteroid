using UnityEngine;
using System.Collections;

public class Asteroid : Obstacle
{
    public bool isMedium,isBig;
    public GameObject smallerAsteroid;

    public new void Damage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        explosionParticle.SetActive(true);
        explosionParticle.transform.parent = transform.parent;
        GeneratorManager.Instance.asteroids.Remove(gameObject);
        if(smallerAsteroid != null)
        {
            int scatterCount = 3;//Scatter count of smaller asteroids
            for(int i = 0; i < scatterCount; i++){
                Vector3 pos = new Vector3(1 * Mathf.Cos((120*i) * Mathf.Deg2Rad), 1 * Mathf.Sin((120 * i) * Mathf.Deg2Rad), 0) + transform.position;
                GameObject temp = (GameObject)Instantiate(smallerAsteroid, pos, Quaternion.Euler(0,0,120*i));
                temp.GetComponent<Obstacle>().isScatterObject = true;
                temp.GetComponent<Rigidbody2D>().velocity = new Vector2(GlobalsManager.Instance.asteroidSpeed / 3 * Mathf.Cos(120 * i * Mathf.Deg2Rad), GlobalsManager.Instance.asteroidSpeed / 3* Mathf.Sin(120 * i * Mathf.Deg2Rad));
                GeneratorManager.Instance.asteroids.Add(temp);
            }
        }
        Destroy(gameObject);
    }

}
