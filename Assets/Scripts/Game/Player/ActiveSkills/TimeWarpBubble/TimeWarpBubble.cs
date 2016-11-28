using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeWarpBubble : MonoBehaviour {

    public float duration;
    List<GameObject> asteroidList = new List<GameObject>();

	void OnEnable()
    {
        StartCoroutine(Deactivator());
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Obstacle")
        {
            collider.GetComponent<Rigidbody2D>().velocity *= Constants.SLOW_BUBBLE_FACTOR;
            collider.GetComponent<Rigidbody2D>().angularVelocity *= Constants.SLOW_BUBBLE_FACTOR;
            collider.GetComponent<Obstacle>().isInTimeWarpBubble = true;
            asteroidList.Add(collider.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Obstacle")
        {
            collider.GetComponent<Obstacle>().isInTimeWarpBubble = false;
            collider.GetComponent<Rigidbody2D>().velocity *= GlobalsManager.Instance.asteroidSpeed / collider.GetComponent<Rigidbody2D>().velocity.magnitude;          
            asteroidList.Remove(collider.gameObject);
        }
    }

    IEnumerator Deactivator()
    {
        yield return new WaitForSeconds(duration);
        foreach(GameObject asteroid in asteroidList)
        {
            if(asteroid != null)
            {
                asteroid.GetComponent<Obstacle>().isInTimeWarpBubble = false;
                asteroid.GetComponent<Rigidbody2D>().velocity *= GlobalsManager.Instance.asteroidSpeed / asteroid.GetComponent<Rigidbody2D>().velocity.magnitude;
            }
        }
        asteroidList.Clear();
        gameObject.SetActive(false);
    }
}
