﻿using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

    public int menuOption;
	
    public void MenuAction()
    {
        MenuManager.Instace.MenuAction(menuOption);
    }
}
