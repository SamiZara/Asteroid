using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
	void Start () {
        Instance = this;
        Application.LoadLevelAdditiveAsync("Background");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
