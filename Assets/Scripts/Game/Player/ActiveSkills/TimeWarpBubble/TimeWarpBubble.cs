using UnityEngine;
using System.Collections;

public class TimeWarpBubble : MonoBehaviour {

    public float duration;
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
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Obstacle")
        {
            collider.GetComponent<Obstacle>().isInTimeWarpBubble = false;
            collider.GetComponent<Rigidbody2D>().velocity *= GlobalsManager.Instance.asteroidSpeed / collider.GetComponent<Rigidbody2D>().velocity.magnitude;
        }
    }

    IEnumerator Deactivator()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
