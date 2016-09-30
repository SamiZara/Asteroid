using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIReferenceManager : MonoBehaviour {

    public static UIReferenceManager Instance;
    public GameObject windowsWeapon1,windowsWeapon2,popUpMenu;
    public RectTransform iconSelectedWeapon;
    public Image iconActiveWeapon,iconSecondaryActiveWeapon,turretUpgradeButton,gaussUpgradeButton,beamUpgradeButton,mainMenuPrimaryWeapon,mainMenuSecondaryWeapon;
    public Text playerMoneyText,popUpMenuText,mainMenuPrimaryWeaponText,mainMenuSecondaryWeaponText,turretInfoText,gaussInfoText,beamInfoText;
    public Button turretButton,gaussButton, beamButton;
    void Awake () {
        Instance = this;
	}
}
