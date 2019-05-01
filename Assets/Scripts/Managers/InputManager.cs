using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public static InputManager Instance;
    //private float lastTapTime = float.MinValue;
    void Awake () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.Instance.isGameOver)
        {
            /*if (Input.GetMouseButtonDown(0))
            {
                if (Time.time - lastTapTime < Constants.TIME_GAP_TO_ACTIVATE_SKILL)
                {
                    GlobalsManager.Instance.playerController.ActivateSkill();
                }
                lastTapTime = Time.time;
            }*/
            if (Input.GetMouseButton(0))
            {
                PlayerController.Instance.rotate();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                GlobalsManager.Instance.knob.transform.localPosition = Vector3.zero;
                PlayerController.Instance.stopRotate();
            }
        }
	}
}
