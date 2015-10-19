using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratorManager : MonoBehaviour {

    public static GeneratorManager Instance;
    public List<GameObject> asteroids = new List<GameObject>();
    void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
