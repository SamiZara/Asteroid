using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIReferenceManager : MonoBehaviour
{

    public static UIReferenceManager Instance;
    public GameObject windowsWeapon1, windowsWeapon2, windowsActive, popUpMenu, mainMenuShipUnlockButton;
    public RectTransform selectedWeapon1Icon, selectedWeapon2Icon, selectedActiveIcon;
    public Image iconActiveWeapon1, iconSecondaryActiveWeapon1, iconActiveWeapon2, iconPrimaryActiveWeapon2, turretUpgradeButton, gaussUpgradeButton, beamUpgradeButton, rocketUpgradeButton, missileUpgradeButton, teslaUpgradeButton, mainMenuPrimaryWeapon, mainMenuSecondaryWeapon, mainMenuActiveSkill, iconActiveWindowSkill, mainMenuActiveShipIcon;
    public Text playerMoneyText, popUpMenuText, mainMenuPrimaryWeaponText, mainMenuSecondaryWeaponText, mainMenuActiveSkillText, turretInfoText, gaussInfoText, beamInfoText, rocketInfoText, missileInfoText, teslaInfoText, dashInfoText, warpInfoText, bombInfoText;
    public Button turretButton, gaussButton, beamButton, rocketButton, missileButton, teslaButton, dashButton, warpButton, bombButton, dashUnlockButton, warpUnlockButton, bombUnlockButton,soundOffButton,soundOnButton,popUpYesButton;
    void Awake()
    {
        Instance = this;
    }
}
