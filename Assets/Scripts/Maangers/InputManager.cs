using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public static InputManager Instance;
	void Start () {
        if (Instance == null)
            Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            CharachterControlManager.Instance.rotate();
        }
	}
}
