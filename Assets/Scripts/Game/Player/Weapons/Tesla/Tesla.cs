using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tesla : Weapon
{

    public List<GameObject> asteroidsInRange = new List<GameObject>();
    public GameObject lightningEffect;
    private float damage = 1.8f;
    new void Start()
    {
        base.Start();
        StartCoroutine(PeriodicShock());
        if(tier == 2)
        {
            GetComponent<CircleCollider2D>().radius = 2.4f;
            damage = 2.4f;
        }
        if (tier == 3)
        {
            GetComponent<CircleCollider2D>().radius = 3f;
            damage = 3.4f;
        }
        if (tier == 4)
        {
            GetComponent<CircleCollider2D>().radius = 3f;
            damage = 4.08f;
        }
        if (tier == 5)
        {
            GetComponent<CircleCollider2D>().radius = 3f;
            damage = 4.88f;
        }
        if (tier == 6)
        {
            GetComponent<CircleCollider2D>().radius = 3f;
            damage = 5.86f;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Obstacle")
        {
            asteroidsInRange.Add(collider.gameObject);
        }
        else
        {
            Debug.Log("Tesla collided with:" + collider.name);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Obstacle")
        {
            asteroidsInRange.Remove(collider.gameObject);
        }
        else
        {
            Debug.Log("Tesla collided(exit) with:" + collider.name);
        }
    }

    IEnumerator PeriodicShock()
    {
        while (true)
        {
            List<GameObject> tempList = new List<GameObject>(asteroidsInRange);
            if (GameManager.Instance.isSoundOn && tempList.Count != 0)
                        fireSound.Play();
            foreach (GameObject asteroid in tempList)
            {
                if (asteroid != null)
                {
                    GameObject effect = (GameObject)Instantiate(lightningEffect, transform.parent);
                    effect.transform.FindChild("Destination").transform.position = asteroid.transform.position;
                    //effect.transform.FindChild("Destination").transform.parent = asteroid.transform;
                    effect.transform.FindChild("Source").transform.position = transform.position;
                    ExplodingAsteroid temp = asteroid.GetComponent<ExplodingAsteroid>();
                    Asteroid temp2 = asteroid.GetComponent<Asteroid>();
                    Obstacle temp3 = asteroid.GetComponent<Obstacle>();
                    if (temp != null)
                    {
                        temp.Damage(damage);
                        
                    }
                    else if (temp2 != null)
                    {
                        temp2.Damage(damage, MathHelper.degreeBetween2Points(asteroid.transform.position, transform.position),false);
                    }
                    else if (temp3 != null)
                    {
                        temp3.Damage(damage);
                    }
                    else
                    {
                        Debug.Log("Something collided with something it should not " + GetComponent<Collider>().name);
                    }
                    //effect.GetComponent<Lightn>
                }
                else
                {
                    asteroidsInRange.Remove(asteroid);
                }
            }
            yield return new WaitForSeconds(0.75f);
        }
    }
}
