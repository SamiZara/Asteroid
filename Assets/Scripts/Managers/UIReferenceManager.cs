using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIReferenceManager : MonoBehaviour {

    public static UIReferenceManager Instance;
    public GameObject windowsWeapon1,popUpMenu;
    public RectTransform iconSelectedWeapon;
    public Image iconActiveWeapon,turretUpgradeButton,gaussUpgradeButton,beamUpgradeButton,mainMenuPrimaryWeapon;
    public Text textActiveWeapon1,textPlayerMoney,popUpMenuText;
    void Awake () {
        Instance = this;
	}
}
