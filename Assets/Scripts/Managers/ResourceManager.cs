using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager {
    public static ResourceManager Instance;
    Dictionary<string, GameObject> storedAllocations = new Dictionary<string, GameObject>();
    // Use this for initialization
    void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AllocateAndStore(string path,string storedName)
    {
        storedAllocations.Add(storedName, Resources.Load<GameObject>(path));
    }

    public GameObject AllocateAndDump(string path)
    {
        return Resources.Load<GameObject>(path);
    }
}
