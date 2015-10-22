using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
}
