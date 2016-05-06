using UnityEngine;
using System.Collections;

public class RocketAoeDamager : MonoBehaviour {

    public float damage;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
    }
}
