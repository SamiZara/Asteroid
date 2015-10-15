using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public static InputManager Instance;
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton (0)) 
		{
            PlayerController.Instance.rotate ();
		} 
		else if (Input.GetMouseButtonUp (0)) 
		{
            PlayerController.Instance.stopRotate();
		}
	}
}
