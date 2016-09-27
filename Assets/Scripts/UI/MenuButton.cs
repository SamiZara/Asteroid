using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

    public int menuOption;
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MenuAction()
    {
        MenuManager.Instace.MenuAction(menuOption);
    }
}
