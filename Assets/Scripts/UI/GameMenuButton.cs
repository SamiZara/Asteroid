using UnityEngine;
using System.Collections;

public class GameMenuButton : MonoBehaviour {

    public int menuOption;

    public void MenuAction()
    {
        GameUIManager.Instance.MenuAction(menuOption);
    }
}
