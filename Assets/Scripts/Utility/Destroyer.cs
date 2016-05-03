using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {
    public float destroyDelayTime;
	void Start () {
        StartCoroutine(Destroy());
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(destroyDelayTime);
        Destroy(gameObject);
    }
}
