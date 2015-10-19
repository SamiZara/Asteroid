using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
	void Awake () {
        Instance = this;
        Application.LoadLevelAdditiveAsync("Background");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
