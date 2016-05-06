using UnityEngine;
using System.Collections;

public class DesroyerWithNoChild : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(DestroyCheck());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator DestroyCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (transform.childCount == 0)
                Destroy(gameObject);
        }
    }
}
