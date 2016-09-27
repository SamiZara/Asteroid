using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIResourceManager : MonoBehaviour {
    public static UIResourceManager Instance;
    public Dictionary<string, Sprite> storedAllocations = new Dictionary<string, Sprite>();
    // Use this for initialization
    void Awake()
    {
        Instance = this;
    }

    public void AllocateAndStore(string path, string storedName)
    {
        storedAllocations.Add(storedName, Resources.Load<Sprite>(path));
    }

    public Sprite AllocateAndDump(string path)
    {
        return Resources.Load<Sprite>(path);
    }

    public void Dump(string name)
    {
        storedAllocations.Remove(name);
    }
}
