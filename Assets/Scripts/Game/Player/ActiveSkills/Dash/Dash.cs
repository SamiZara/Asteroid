using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour {

    public bool isDashed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && !isDashed)
        {
            Activate();
        }
	}

    void Activate()
    {
        isDashed = true;
        GlobalsManager.Instance.player.GetComponent<TrailRenderer>().enabled = true;
        GlobalsManager.Instance.player.GetComponent<PlayerController>().Dash();
        GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Obstacle")
        {
            ExplodingAsteroid temp = collider.GetComponent<ExplodingAsteroid>();
            Obstacle temp2 = collider.GetComponent<Obstacle>();
            if (temp != null)
            {
                temp.Damage(float.MaxValue);
            }
            else if (temp2 != null)
            {
                temp2.Damage(float.MaxValue);
            }
            else
            {
                Debug.Log("Something collided with something it should not " + collider.name);
            }
        }
    }
}
