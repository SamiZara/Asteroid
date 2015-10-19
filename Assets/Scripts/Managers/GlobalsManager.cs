using UnityEngine;
using System.Collections;

public class GlobalsManager : MonoBehaviour {
    public static GlobalsManager Instance;
    public Vector3 screenPos;
    // Use this for initialization
	void Awake () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }
}
