using UnityEngine;
using System.Collections;

public class TimeWarpBubble : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Obstacle")
        {
            collider.GetComponent<Rigidbody2D>().velocity *= Constants.SLOW_BUBBLE_FACTOR;
            collider.GetComponent<Obstacle>().isInTimeWarpBubble = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Obstacle")
        {
            collider.GetComponent<Obstacle>().isInTimeWarpBubble = false;
        }
    }
}
