using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIReferenceManager : MonoBehaviour {

    public static UIReferenceManager Instance;
    public GameObject windowsWeapon1,windowsWeapon2,popUpMenu;
    public RectTransform iconSelectedWeapon1,iconSelectedWeapon2;
    public Image iconActiveWeapon1,iconSecondaryActiveWeapon1, iconActiveWeapon2, iconPrimaryActiveWeapon2, turretUpgradeButton, gaussUpgradeButton,beamUpgradeButton,rocketUpgradeButton,missileUpgradeButton,teslaUpgradeButton,mainMenuPrimaryWeapon,mainMenuSecondaryWeapon;
    public Text playerMoneyText,popUpMenuText,mainMenuPrimaryWeaponText,mainMenuSecondaryWeaponText,turretInfoText,gaussInfoText,beamInfoText,rocketInfoText,missileInfoText,teslaInfoText;
    public Button turretButton,gaussButton, beamButton,rocketButton,missileButton,teslaButton;
    void Awake () {
        Instance = this;
	}
}
