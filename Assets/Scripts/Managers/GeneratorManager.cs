using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratorManager : MonoBehaviour {

    public static GeneratorManager Instance;
    public List<GameObject> asteroids = new List<GameObject>();
	void Awake () {
        Instance = this;
	}

	void Start()
	{
		ResourceManager.Instance.AllocateAndStore ("Prefabs/asteroid", "asteroid");
		Instantiate (ResourceManager.Instance.storedAllocations ["asteroid"], new Vector3 (2, 0, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
