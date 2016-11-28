using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public static MenuManager Instace;
    private int itemCode, selectedShipIndex;//Decides which item is being bought for popup menu
    private int[,] weaponCosts;
    private int[] activeSkillCosts;
    private int[] shipCosts;


    void Start()
    {
        //Allocation
        UIResourceManager.Instance.AllocateAndStore("UI/Buttons/bt-upgrade", "ButtonUpgrade");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icon-Turret", "TurretIcon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icon-GaussGun", "GaussIcon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icon-Beam", "BeamIcon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icon-Rocket", "RocketIcon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icon-HomMissile", "MissileIcon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icon-TeslaGun", "TeslaIcon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icons-active-bomb", "BombIcon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icons-active-dash", "DashIcon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icons-active-im-shield", "ShieldIcon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icons-active-time-warp", "WarpIcon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/ui-ship1", "Ship1Icon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/ui-ship2", "Ship2Icon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/ui-ship3", "Ship3Icon");
        //Primary Weapon
        if (PlayerPrefs.GetInt("Weapon1", 0) == 0)
        {
            UIReferenceManager.Instance.mainMenuPrimaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["TurretIcon"];
            UIReferenceManager.Instance.mainMenuPrimaryWeapon.SetNativeSize();
            UIReferenceManager.Instance.mainMenuPrimaryWeaponText.text = "Turret Gun";
        }
        else if (PlayerPrefs.GetInt("Weapon1", 0) == 1)
        {
            UIReferenceManager.Instance.mainMenuPrimaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["GaussIcon"];
            UIReferenceManager.Instance.mainMenuPrimaryWeapon.SetNativeSize();
            UIReferenceManager.Instance.mainMenuPrimaryWeaponText.text = "Gauss Gun";
        }
        else if (PlayerPrefs.GetInt("Weapon1", 0) == 2)
        {
            UIReferenceManager.Instance.mainMenuPrimaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["BeamIcon"];
            UIReferenceManager.Instance.mainMenuPrimaryWeapon.SetNativeSize();
            UIReferenceManager.Instance.mainMenuPrimaryWeaponText.text = "Beam";
        }
        //Secondary Weapon
        if (PlayerPrefs.GetInt("Weapon2", 0) == 0)
        {
            UIReferenceManager.Instance.mainMenuSecondaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["RocketIcon"];
            UIReferenceManager.Instance.mainMenuSecondaryWeapon.SetNativeSize();
            UIReferenceManager.Instance.mainMenuSecondaryWeaponText.text = "Rocket";
        }
        else if (PlayerPrefs.GetInt("Weapon2", 0) == 1)
        {
            UIReferenceManager.Instance.mainMenuSecondaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["MissileIcon"];
            UIReferenceManager.Instance.mainMenuSecondaryWeapon.SetNativeSize();
            UIReferenceManager.Instance.mainMenuSecondaryWeaponText.text = "Homming";
        }
        else if (PlayerPrefs.GetInt("Weapon2", 0) == 2)
        {
            UIReferenceManager.Instance.mainMenuSecondaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["TeslaIcon"];
            UIReferenceManager.Instance.mainMenuSecondaryWeapon.SetNativeSize();
            UIReferenceManager.Instance.mainMenuSecondaryWeaponText.text = "Tesla";
        }
        //Active
        if (PlayerPrefs.GetInt("ActiveSkill", 0) == 0)
        {
            UIReferenceManager.Instance.mainMenuActiveSkill.sprite = UIResourceManager.Instance.storedAllocations["ShieldIcon"];
            UIReferenceManager.Instance.mainMenuActiveSkill.SetNativeSize();
            UIReferenceManager.Instance.mainMenuActiveSkillText.text = "Immunity Shield";
        }
        else if (PlayerPrefs.GetInt("ActiveSkill", 0) == 1)
        {
            UIReferenceManager.Instance.mainMenuActiveSkill.sprite = UIResourceManager.Instance.storedAllocations["DashIcon"];
            UIReferenceManager.Instance.mainMenuActiveSkill.SetNativeSize();
            UIReferenceManager.Instance.mainMenuActiveSkillText.text = "Dash";
        }
        else if (PlayerPrefs.GetInt("ActiveSkill", 0) == 2)
        {
            UIReferenceManager.Instance.mainMenuActiveSkill.sprite = UIResourceManager.Instance.storedAllocations["WarpIcon"];
            UIReferenceManager.Instance.mainMenuActiveSkill.SetNativeSize();
            UIReferenceManager.Instance.mainMenuActiveSkillText.text = "Time Warp";
        }
        else if (PlayerPrefs.GetInt("ActiveSkill", 0) == 3)
        {
            UIReferenceManager.Instance.mainMenuActiveSkill.sprite = UIResourceManager.Instance.storedAllocations["BombIcon"];
            UIReferenceManager.Instance.mainMenuActiveSkill.SetNativeSize();
            UIReferenceManager.Instance.mainMenuActiveSkillText.text = "Big Bomb";
        }
        //Ship
        if (PlayerPrefs.GetInt("SelectedShip", 0) == 0)
        {
            UIReferenceManager.Instance.mainMenuActiveShipIcon.sprite = UIResourceManager.Instance.storedAllocations["Ship1Icon"];
            UIReferenceManager.Instance.mainMenuActiveShipIcon.SetNativeSize();
            UIReferenceManager.Instance.mainMenuShipUnlockButton.SetActive(false);
            selectedShipIndex = 0;
        }
        else if (PlayerPrefs.GetInt("SelectedShip", 0) == 1)
        {
            UIReferenceManager.Instance.mainMenuActiveShipIcon.sprite = UIResourceManager.Instance.storedAllocations["Ship2Icon"];
            UIReferenceManager.Instance.mainMenuActiveShipIcon.SetNativeSize();
            UIReferenceManager.Instance.mainMenuShipUnlockButton.SetActive(false);
            selectedShipIndex = 1;
        }
        else if (PlayerPrefs.GetInt("SelectedShip", 0) == 2)
        {
            UIReferenceManager.Instance.mainMenuActiveShipIcon.sprite = UIResourceManager.Instance.storedAllocations["Ship3Icon"];
            UIReferenceManager.Instance.mainMenuActiveShipIcon.SetNativeSize();
            UIReferenceManager.Instance.mainMenuShipUnlockButton.SetActive(false);
            selectedShipIndex = 2;
        }
        //Sound
        if (PlayerPrefs.GetInt("Sound",1) == 0)
        {
            UIReferenceManager.Instance.soundOffButton.interactable = false;
            UIReferenceManager.Instance.soundOnButton.interactable = true;
        }
    }

    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("PlayerMoney", 1000000);
        Instace = this;
        UIReferenceManager.Instance.playerMoneyText.text = PlayerPrefs.GetInt("PlayerMoney", 0).ToString();
        //Weapons
        weaponCosts = new int[6, 3];
        weaponCosts[0, 0] = 0;
        weaponCosts[0, 1] = 50;
        weaponCosts[0, 2] = 200;
        weaponCosts[1, 0] = 75;
        weaponCosts[1, 1] = 150;
        weaponCosts[1, 2] = 450;
        weaponCosts[2, 0] = 100;
        weaponCosts[2, 1] = 200;
        weaponCosts[2, 2] = 650;
        weaponCosts[3, 0] = 0;
        weaponCosts[3, 1] = 100;
        weaponCosts[3, 2] = 250;
        weaponCosts[4, 0] = 100;
        weaponCosts[4, 1] = 175;
        weaponCosts[4, 2] = 500;
        weaponCosts[5, 0] = 125;
        weaponCosts[5, 1] = 250;
        weaponCosts[5, 2] = 750;
        //Actives
        activeSkillCosts = new int[4];
        activeSkillCosts[0] = 0;
        activeSkillCosts[1] = 150;
        activeSkillCosts[2] = 250;
        activeSkillCosts[3] = 400;
        //Ships
        shipCosts = new int[2];
        shipCosts[0] = 1000;
        shipCosts[1] = 2500;
    }

    public void MenuAction(int option)
    {
        switch (option)
        {
            case 0:
                SceneManager.LoadScene("Game");
                break;
            case 1:
                UIReferenceManager.Instance.windowsWeapon1.SetActive(true);
                int turretState = PlayerPrefs.GetInt("WeaponTurret", 1);
                if (turretState != 0 && turretState < 3)
                {
                    UIReferenceManager.Instance.turretInfoText.text = "Tier:" + (turretState) + "\nUpgrade Cost: " + weaponCosts[0, turretState] + "";
                }
                else if (turretState >= 3)
                {
                    UIReferenceManager.Instance.turretUpgradeButton.GetComponent<Button>().interactable = false;
                    UIReferenceManager.Instance.turretInfoText.text = "Tier:" + (turretState) + "\nMaxed";
                }
                int gaussState = PlayerPrefs.GetInt("WeaponGauss", 0);
                if (gaussState > 0)
                {
                    UIReferenceManager.Instance.gaussButton.interactable = true;
                    UIReferenceManager.Instance.gaussUpgradeButton.sprite = UIResourceManager.Instance.storedAllocations["ButtonUpgrade"];
                    UIReferenceManager.Instance.gaussUpgradeButton.SetNativeSize();
                    if (gaussState < 3)
                        UIReferenceManager.Instance.gaussInfoText.text = "Tier:" + (gaussState) + "\nUpgrade Cost: " + weaponCosts[1, gaussState] + "";
                    else if (gaussState >= 3)
                    {
                        UIReferenceManager.Instance.gaussUpgradeButton.GetComponent<Button>().interactable = false;
                        UIReferenceManager.Instance.gaussInfoText.text = "Tier:" + (gaussState) + "\nMaxed";
                    }
                }
                int beamState = PlayerPrefs.GetInt("WeaponBeam", 0);
                if (beamState > 0)
                {
                    UIReferenceManager.Instance.beamButton.interactable = true;
                    UIReferenceManager.Instance.beamUpgradeButton.sprite = UIResourceManager.Instance.storedAllocations["ButtonUpgrade"];
                    UIReferenceManager.Instance.beamUpgradeButton.SetNativeSize();
                    if (beamState < 3)
                        UIReferenceManager.Instance.beamInfoText.text = "Tier:" + (beamState) + "\nUpgrade Cost: " + weaponCosts[2, beamState] + "";
                    if (beamState >= 3)
                    {
                        UIReferenceManager.Instance.beamUpgradeButton.GetComponent<Button>().interactable = false;
                        UIReferenceManager.Instance.beamInfoText.text = "Tier:" + (beamState) + "\nMaxed";
                    }
                }
                MenuAction(PlayerPrefs.GetInt("Weapon1", 0) + 2);
                int secondaryWeaponState = PlayerPrefs.GetInt("Weapon2", 0);
                if (secondaryWeaponState == 0)
                {
                    UIReferenceManager.Instance.iconSecondaryActiveWeapon1.sprite = UIResourceManager.Instance.storedAllocations["RocketIcon"];
                }
                else if (secondaryWeaponState == 1)
                {
                    UIReferenceManager.Instance.iconSecondaryActiveWeapon1.sprite = UIResourceManager.Instance.storedAllocations["MissileIcon"];
                }
                else if (secondaryWeaponState == 2)
                {
                    UIReferenceManager.Instance.iconSecondaryActiveWeapon1.sprite = UIResourceManager.Instance.storedAllocations["TeslaIcon"];
                }
                UIReferenceManager.Instance.iconSecondaryActiveWeapon1.SetNativeSize();
                break;
            case 2:
                UIReferenceManager.Instance.selectedWeapon1Icon.localPosition = new Vector3(-305, 117, 0);
                UIReferenceManager.Instance.iconActiveWeapon1.sprite = UIResourceManager.Instance.storedAllocations["TurretIcon"];
                UIReferenceManager.Instance.iconActiveWeapon1.SetNativeSize();
                UIReferenceManager.Instance.mainMenuPrimaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["TurretIcon"];
                UIReferenceManager.Instance.mainMenuPrimaryWeapon.SetNativeSize();
                UIReferenceManager.Instance.mainMenuPrimaryWeaponText.text = "Turret Gun";
                PlayerPrefs.SetInt("Weapon1", 0);
                break;
            case 3:
                UIReferenceManager.Instance.selectedWeapon1Icon.localPosition = new Vector3(0, 117, 0);
                UIReferenceManager.Instance.iconActiveWeapon1.sprite = UIResourceManager.Instance.storedAllocations["GaussIcon"];
                UIReferenceManager.Instance.iconActiveWeapon1.SetNativeSize();
                UIReferenceManager.Instance.mainMenuPrimaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["GaussIcon"];
                UIReferenceManager.Instance.mainMenuPrimaryWeapon.SetNativeSize();
                UIReferenceManager.Instance.mainMenuPrimaryWeaponText.text = "Gauss Gun";
                PlayerPrefs.SetInt("Weapon1", 1);
                break;
            case 4:
                UIReferenceManager.Instance.selectedWeapon1Icon.localPosition = new Vector3(305, 117, 0);
                UIReferenceManager.Instance.iconActiveWeapon1.sprite = UIResourceManager.Instance.storedAllocations["BeamIcon"];
                UIReferenceManager.Instance.iconActiveWeapon1.SetNativeSize();
                UIReferenceManager.Instance.mainMenuPrimaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["BeamIcon"];
                UIReferenceManager.Instance.mainMenuPrimaryWeapon.SetNativeSize();
                UIReferenceManager.Instance.mainMenuPrimaryWeaponText.text = "Beam Gun";
                PlayerPrefs.SetInt("Weapon1", 2);
                break;
            case 5:
                UIReferenceManager.Instance.windowsWeapon1.SetActive(false);
                break;
            case 6:
                itemCode = 0;
                int state = PlayerPrefs.GetInt("WeaponTurret", 1);
                ShowPopUp("Turret Gun Tier " + (state + 1), weaponCosts[itemCode, state].ToString());
                break;
            case 7:
                itemCode = 1;
                state = PlayerPrefs.GetInt("WeaponGauss", 0);
                ShowPopUp("Gauss Gun Tier " + (state + 1), weaponCosts[itemCode, state].ToString());
                break;
            case 8:
                itemCode = 2;
                state = PlayerPrefs.GetInt("WeaponBeam", 0);
                ShowPopUp("Beam Tier " + (state + 1), weaponCosts[itemCode, state].ToString());
                break;
            case 9:
                PopUpYes();
                break;
            case 10:
                PopUpNo();
                break;
            case 11:
                UIReferenceManager.Instance.windowsWeapon2.SetActive(true);
                int rocketState = PlayerPrefs.GetInt("WeaponRocket", 1);
                if (rocketState != 0 && rocketState < 3)
                {
                    UIReferenceManager.Instance.rocketInfoText.text = "Tier:" + (rocketState) + "\nUpgrade Cost: " + weaponCosts[3, rocketState] + "";
                }
                else if (rocketState >= 3)
                {
                    UIReferenceManager.Instance.rocketUpgradeButton.GetComponent<Button>().interactable = false;
                    UIReferenceManager.Instance.rocketInfoText.text = "Tier:" + (rocketState) + "\nMaxed";
                }
                int missileState = PlayerPrefs.GetInt("WeaponMissile", 0);
                if (missileState > 0)
                {
                    UIReferenceManager.Instance.missileButton.interactable = true;
                    UIReferenceManager.Instance.missileUpgradeButton.sprite = UIResourceManager.Instance.storedAllocations["ButtonUpgrade"];
                    UIReferenceManager.Instance.missileUpgradeButton.SetNativeSize();
                    if (missileState < 3)
                        UIReferenceManager.Instance.missileInfoText.text = "Tier:" + (missileState) + "\nUpgrade Cost: " + weaponCosts[4, missileState] + "";
                    else if (missileState >= 3)
                    {
                        UIReferenceManager.Instance.missileUpgradeButton.GetComponent<Button>().interactable = false;
                        UIReferenceManager.Instance.missileInfoText.text = "Tier:" + (missileState) + "\nMaxed";
                    }
                }
                int teslaState = PlayerPrefs.GetInt("WeaponTesla", 0);
                if (teslaState > 0)
                {
                    UIReferenceManager.Instance.teslaButton.interactable = true;
                    UIReferenceManager.Instance.teslaUpgradeButton.sprite = UIResourceManager.Instance.storedAllocations["ButtonUpgrade"];
                    UIReferenceManager.Instance.teslaUpgradeButton.SetNativeSize();
                    if (teslaState < 3)
                        UIReferenceManager.Instance.teslaInfoText.text = "Tier:" + (teslaState) + "\nUpgrade Cost: " + weaponCosts[5, teslaState] + "";
                    if (teslaState >= 3)
                    {
                        UIReferenceManager.Instance.teslaUpgradeButton.GetComponent<Button>().interactable = false;
                        UIReferenceManager.Instance.teslaInfoText.text = "Tier:" + (teslaState) + "\nMaxed";
                    }
                }
                MenuAction(PlayerPrefs.GetInt("Weapon2", 0) + 12);
                int primaryWeaponState = PlayerPrefs.GetInt("Weapon1", 0);
                if (primaryWeaponState == 0)
                {
                    UIReferenceManager.Instance.iconPrimaryActiveWeapon2.sprite = UIResourceManager.Instance.storedAllocations["TurretIcon"];
                }
                else if (primaryWeaponState == 1)
                {
                    UIReferenceManager.Instance.iconPrimaryActiveWeapon2.sprite = UIResourceManager.Instance.storedAllocations["GaussIcon"];
                }
                else if (primaryWeaponState == 2)
                {
                    UIReferenceManager.Instance.iconPrimaryActiveWeapon2.sprite = UIResourceManager.Instance.storedAllocations["BeamIcon"];
                }
                UIReferenceManager.Instance.iconPrimaryActiveWeapon2.SetNativeSize();
                break;
            case 12:
                UIReferenceManager.Instance.selectedWeapon2Icon.localPosition = new Vector3(-305, 117, 0);
                UIReferenceManager.Instance.iconActiveWeapon2.sprite = UIResourceManager.Instance.storedAllocations["RocketIcon"];
                UIReferenceManager.Instance.mainMenuSecondaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["RocketIcon"];
                UIReferenceManager.Instance.mainMenuSecondaryWeapon.SetNativeSize();
                UIReferenceManager.Instance.mainMenuSecondaryWeaponText.text = "Rocket";
                PlayerPrefs.SetInt("Weapon2", 0);
                break;
            case 13:
                UIReferenceManager.Instance.selectedWeapon2Icon.localPosition = new Vector3(0, 117, 0);
                UIReferenceManager.Instance.iconActiveWeapon2.sprite = UIResourceManager.Instance.storedAllocations["MissileIcon"];
                UIReferenceManager.Instance.mainMenuSecondaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["MissileIcon"];
                UIReferenceManager.Instance.mainMenuSecondaryWeapon.SetNativeSize();
                UIReferenceManager.Instance.mainMenuSecondaryWeaponText.text = "Missile";
                PlayerPrefs.SetInt("Weapon2", 1);
                break;
            case 14:
                UIReferenceManager.Instance.selectedWeapon2Icon.localPosition = new Vector3(305, 117, 0);
                UIReferenceManager.Instance.iconActiveWeapon2.sprite = UIResourceManager.Instance.storedAllocations["TeslaIcon"];
                UIReferenceManager.Instance.mainMenuSecondaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["TeslaIcon"];
                UIReferenceManager.Instance.mainMenuSecondaryWeapon.SetNativeSize();
                UIReferenceManager.Instance.mainMenuSecondaryWeaponText.text = "Tesla";
                PlayerPrefs.SetInt("Weapon2", 2);
                break;
            case 15:
                UIReferenceManager.Instance.windowsWeapon2.SetActive(false);
                break;
            case 16:
                itemCode = 3;
                state = PlayerPrefs.GetInt("WeaponRocket", 1);
                ShowPopUp("Rocket Gun Tier " + (state + 1), weaponCosts[itemCode, state].ToString());
                break;
            case 17:
                itemCode = 4;
                state = PlayerPrefs.GetInt("WeaponMissile", 0);
                ShowPopUp("Homing Missile Tier " + (state + 1), weaponCosts[itemCode, state].ToString());
                break;
            case 18:
                itemCode = 5;
                state = PlayerPrefs.GetInt("WeaponTesla", 0);
                ShowPopUp("Beam Tier " + (state + 1), weaponCosts[itemCode, state].ToString());
                break;
            case 19:
                UIReferenceManager.Instance.windowsActive.SetActive(true);
                int dashState = PlayerPrefs.GetInt("ActiveDash");
                if (dashState == 1)
                {
                    UIReferenceManager.Instance.dashButton.interactable = true;
                    UIReferenceManager.Instance.dashUnlockButton.interactable = false;
                }
                int warpState = PlayerPrefs.GetInt("ActiveWarp");
                if (warpState == 1)
                {
                    UIReferenceManager.Instance.warpButton.interactable = true;
                    UIReferenceManager.Instance.warpUnlockButton.interactable = false;
                }
                int bombState = PlayerPrefs.GetInt("ActiveBomb");
                if (bombState == 1)
                {
                    UIReferenceManager.Instance.bombButton.interactable = true;
                    UIReferenceManager.Instance.bombUnlockButton.interactable = false;
                }
                MenuAction(PlayerPrefs.GetInt("ActiveSkill", 0) + 20);
                break;
            case 20:
                UIReferenceManager.Instance.iconActiveWindowSkill.sprite = UIResourceManager.Instance.storedAllocations["ShieldIcon"];
                UIReferenceManager.Instance.iconActiveWindowSkill.SetNativeSize();
                UIReferenceManager.Instance.selectedActiveIcon.localPosition = new Vector3(-600, 120, 0);
                UIReferenceManager.Instance.mainMenuActiveSkill.sprite = UIResourceManager.Instance.storedAllocations["ShieldIcon"];
                UIReferenceManager.Instance.mainMenuActiveSkill.SetNativeSize();
                UIReferenceManager.Instance.mainMenuActiveSkillText.text = "Shield";
                PlayerPrefs.SetInt("ActiveSkill", 0);
                break;
            case 21:
                UIReferenceManager.Instance.iconActiveWindowSkill.sprite = UIResourceManager.Instance.storedAllocations["DashIcon"];
                UIReferenceManager.Instance.iconActiveWindowSkill.SetNativeSize();
                UIReferenceManager.Instance.selectedActiveIcon.localPosition = new Vector3(-325, 120, 0);
                UIReferenceManager.Instance.mainMenuActiveSkill.sprite = UIResourceManager.Instance.storedAllocations["DashIcon"];
                UIReferenceManager.Instance.mainMenuActiveSkill.SetNativeSize();
                UIReferenceManager.Instance.mainMenuActiveSkillText.text = "Dash";
                PlayerPrefs.SetInt("ActiveSkill", 1);
                break;
            case 22:
                UIReferenceManager.Instance.iconActiveWindowSkill.sprite = UIResourceManager.Instance.storedAllocations["WarpIcon"];
                UIReferenceManager.Instance.iconActiveWindowSkill.SetNativeSize();
                UIReferenceManager.Instance.selectedActiveIcon.localPosition = new Vector3(-50, 120, 0);
                UIReferenceManager.Instance.mainMenuActiveSkill.sprite = UIResourceManager.Instance.storedAllocations["WarpIcon"];
                UIReferenceManager.Instance.mainMenuActiveSkill.SetNativeSize();
                UIReferenceManager.Instance.mainMenuActiveSkillText.text = "Warp";
                PlayerPrefs.SetInt("ActiveSkill", 2);
                break;
            case 23:
                UIReferenceManager.Instance.iconActiveWindowSkill.sprite = UIResourceManager.Instance.storedAllocations["BombIcon"];
                UIReferenceManager.Instance.iconActiveWindowSkill.SetNativeSize();
                UIReferenceManager.Instance.selectedActiveIcon.localPosition = new Vector3(225, 120, 0);
                UIReferenceManager.Instance.mainMenuActiveSkill.sprite = UIResourceManager.Instance.storedAllocations["BombIcon"];
                UIReferenceManager.Instance.mainMenuActiveSkill.SetNativeSize();
                UIReferenceManager.Instance.mainMenuActiveSkillText.text = "Bomb";
                PlayerPrefs.SetInt("ActiveSkill", 3);
                break;
            case 24:
                itemCode = 6;
                ShowPopUp("Dash Active Skill", activeSkillCosts[1].ToString());
                break;
            case 25:
                itemCode = 7;
                ShowPopUp("Warp Active Skill", activeSkillCosts[2].ToString());
                break;
            case 26:
                itemCode = 8;
                ShowPopUp("Bomb Active Skill", activeSkillCosts[3].ToString());
                break;
            case 27:
                UIReferenceManager.Instance.windowsActive.SetActive(false);
                break;
            case 28:
                selectedShipIndex = (selectedShipIndex - 1) % 3;
                if (selectedShipIndex < 0)
                    selectedShipIndex += 3;
                UIReferenceManager.Instance.mainMenuActiveShipIcon.sprite = UIResourceManager.Instance.storedAllocations["Ship" + (selectedShipIndex + 1) + "Icon"];
                UIReferenceManager.Instance.mainMenuActiveShipIcon.SetNativeSize();
                if (PlayerPrefs.GetInt("Ship" + selectedShipIndex, 0) == 0 && selectedShipIndex != 0)
                {
                    UIReferenceManager.Instance.mainMenuActiveShipIcon.GetComponent<Button>().interactable = false;
                    UIReferenceManager.Instance.mainMenuShipUnlockButton.SetActive(true);
                }
                else
                {
                    UIReferenceManager.Instance.mainMenuActiveShipIcon.GetComponent<Button>().interactable = true;
                    UIReferenceManager.Instance.mainMenuShipUnlockButton.SetActive(false);
                    PlayerPrefs.SetInt("SelectedShip", selectedShipIndex);
                }
                break;
             case 29:
                selectedShipIndex = (selectedShipIndex + 1) % 3;
                UIReferenceManager.Instance.mainMenuActiveShipIcon.sprite = UIResourceManager.Instance.storedAllocations["Ship" + (selectedShipIndex + 1) + "Icon"];
                UIReferenceManager.Instance.mainMenuActiveShipIcon.SetNativeSize();
                if (PlayerPrefs.GetInt("Ship" + selectedShipIndex, 0) == 0 && selectedShipIndex != 0)
                {
                    UIReferenceManager.Instance.mainMenuActiveShipIcon.GetComponent<Button>().interactable = false;
                    UIReferenceManager.Instance.mainMenuShipUnlockButton.SetActive(true);
                }
                else
                {
                    UIReferenceManager.Instance.mainMenuActiveShipIcon.GetComponent<Button>().interactable = true;
                    UIReferenceManager.Instance.mainMenuShipUnlockButton.SetActive(false);
                    PlayerPrefs.SetInt("SelectedShip",selectedShipIndex);
                }
                break;
            case 30:
                itemCode = 9;
                if(selectedShipIndex == 1)
                    ShowPopUp("Viper(only skin)", shipCosts[0].ToString());
                else if(selectedShipIndex == 2)
                    ShowPopUp("Orion(only skin)", shipCosts[1].ToString());
                break;
            case 31:
                UIReferenceManager.Instance.soundOffButton.interactable = false;
                UIReferenceManager.Instance.soundOnButton.interactable = true;
                PlayerPrefs.SetInt("Sound",0);
                break;
            case 32:
                UIReferenceManager.Instance.soundOffButton.interactable = true;
                UIReferenceManager.Instance.soundOnButton.interactable = false;
                PlayerPrefs.SetInt("Sound", 1);
                break;
            case 33:
                MenuAction(5);
                MenuAction(11);
                break;
            case 34:
                MenuAction(1);
                MenuAction(15);
                break;
        }
    }

    void ShowPopUp(string item, string cost)
    {
        UIReferenceManager.Instance.popUpMenu.SetActive(true);
        UIReferenceManager.Instance.popUpMenuText.text = "Do you want to buy " + item + " for " + cost + "?";
    }

    void PopUpYes()
    {
        int playerMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
        int cost = 0;
        int state = 0;
        if (itemCode == 0)
            state = PlayerPrefs.GetInt("WeaponTurret", 1);
        else if (itemCode == 1)
            state = PlayerPrefs.GetInt("WeaponGauss", 0);
        else if (itemCode == 2)
            state = PlayerPrefs.GetInt("WeaponBeam", 0);
        else if (itemCode == 3)
            state = PlayerPrefs.GetInt("WeaponRocket", 1);
        else if (itemCode == 4)
            state = PlayerPrefs.GetInt("WeaponMissile", 0);
        else if (itemCode == 5)
            state = PlayerPrefs.GetInt("WeaponTesla", 0);
        else if (itemCode == 6)
            state = 1;
        else if (itemCode == 7)
            state = 2;
        else if (itemCode == 8)
            state = 3;
        //Deciding cost
        if (itemCode <= 5 && itemCode >= 0)
            cost = weaponCosts[itemCode, state];
        else if (itemCode >= 6)
            cost = activeSkillCosts[state];
        //Checking money
        if (playerMoney >= cost)
        {
            playerMoney -= cost;
            PlayerPrefs.SetInt("PlayerMoney", playerMoney);
            state += 1;
            if (itemCode == 0)
            {
                if (state < 3)
                {
                    UIReferenceManager.Instance.turretInfoText.text = "Tier:" + (state) + "\nUpgrade Cost: " + weaponCosts[0, state] + "";
                }
                if (state == 3)
                {
                    UIReferenceManager.Instance.turretButton.interactable = false;
                    UIReferenceManager.Instance.turretInfoText.text = "Tier:" + (state) + "\nMaxed";
                    UIReferenceManager.Instance.turretUpgradeButton.GetComponent<Button>().interactable = false;
                }
                PlayerPrefs.SetInt("WeaponTurret", state);
                MenuAction(2);
            }
            else if (itemCode == 1)
            {
                if (state == 1)
                {
                    UIReferenceManager.Instance.gaussUpgradeButton.sprite = UIResourceManager.Instance.storedAllocations["ButtonUpgrade"];
                    UIReferenceManager.Instance.gaussUpgradeButton.SetNativeSize();
                    UIReferenceManager.Instance.gaussButton.interactable = true;
                }
                if (state < 3)
                {
                    UIReferenceManager.Instance.gaussInfoText.text = "Tier:" + ((state)) + "\nUpgrade Cost: " + weaponCosts[1, state] + "";
                }
                if (state == 3)
                {
                    UIReferenceManager.Instance.gaussInfoText.text = "Tier:" + ((state)) + "\nMaxed";
                    UIReferenceManager.Instance.gaussUpgradeButton.GetComponent<Button>().interactable = false;
                }
                PlayerPrefs.SetInt("WeaponGauss", state);
                MenuAction(3);
            }
            else if (itemCode == 2)
            {
                if (state == 1)
                {
                    UIReferenceManager.Instance.beamUpgradeButton.sprite = UIResourceManager.Instance.storedAllocations["ButtonUpgrade"];
                    UIReferenceManager.Instance.beamUpgradeButton.SetNativeSize();
                    UIReferenceManager.Instance.beamButton.interactable = true;
                }
                if (state < 3)
                {
                    UIReferenceManager.Instance.beamInfoText.text = "Tier:" + ((state)) + "\nUpgrade Cost: " + weaponCosts[2, state] + "";
                }
                if (state == 3)
                {
                    UIReferenceManager.Instance.beamInfoText.text = "Tier:" + ((state)) + "\nMaxed";
                    UIReferenceManager.Instance.beamUpgradeButton.GetComponent<Button>().interactable = false;
                }
                PlayerPrefs.SetInt("WeaponBeam", state);
                MenuAction(4);
            }
            else if (itemCode == 3)
            {
                if (state < 3)
                {
                    UIReferenceManager.Instance.rocketInfoText.text = "Tier:" + (state) + "\nUpgrade Cost: " + weaponCosts[3, state] + "";
                }
                if (state == 3)
                {
                    UIReferenceManager.Instance.rocketButton.interactable = false;
                    UIReferenceManager.Instance.rocketInfoText.text = "Tier:" + (state) + "\nMaxed";
                    UIReferenceManager.Instance.rocketUpgradeButton.GetComponent<Button>().interactable = false;  
                }
                PlayerPrefs.SetInt("WeaponRocket", state);
                MenuAction(12);
            }
            else if (itemCode == 4)
            {
                if (state == 1)
                {
                    UIReferenceManager.Instance.missileUpgradeButton.sprite = UIResourceManager.Instance.storedAllocations["ButtonUpgrade"];
                    UIReferenceManager.Instance.missileUpgradeButton.SetNativeSize();
                    UIReferenceManager.Instance.missileButton.interactable = true;
                }
                if (state < 3)
                {
                    UIReferenceManager.Instance.missileInfoText.text = "Tier:" + ((state)) + "\nUpgrade Cost: " + weaponCosts[4, state] + "";
                }
                if (state == 3)
                {
                    UIReferenceManager.Instance.missileInfoText.text = "Tier:" + ((state)) + "\nMaxed";
                    UIReferenceManager.Instance.missileUpgradeButton.GetComponent<Button>().interactable = false;
                }
                PlayerPrefs.SetInt("WeaponMissile", state);
                MenuAction(13);
            }
            else if (itemCode == 5)
            {
                if (state == 1)
                {
                    UIReferenceManager.Instance.teslaUpgradeButton.sprite = UIResourceManager.Instance.storedAllocations["ButtonUpgrade"];
                    UIReferenceManager.Instance.teslaUpgradeButton.SetNativeSize();
                    UIReferenceManager.Instance.teslaButton.interactable = true;
                }
                if (state < 3)
                {
                    UIReferenceManager.Instance.teslaInfoText.text = "Tier:" + ((state)) + "\nUpgrade Cost: " + weaponCosts[5, state] + "";
                }
                if (state == 3)
                {
                    UIReferenceManager.Instance.teslaInfoText.text = "Tier:" + ((state)) + "\nMaxed";
                    UIReferenceManager.Instance.teslaUpgradeButton.GetComponent<Button>().interactable = false;
                }
                PlayerPrefs.SetInt("WeaponTesla", state);
                MenuAction(14);
            }
            else if (itemCode == 6)
            {
                UIReferenceManager.Instance.dashUnlockButton.interactable = false;
                UIReferenceManager.Instance.dashButton.interactable = true;
                UIReferenceManager.Instance.dashInfoText.text = "Unlocked";
                PlayerPrefs.SetInt("ActiveDash", 1);
                MenuAction(21);
            }
            else if (itemCode == 7)
            {
                UIReferenceManager.Instance.warpUnlockButton.interactable = false;
                UIReferenceManager.Instance.warpButton.interactable = true;
                UIReferenceManager.Instance.warpInfoText.text = "Unlocked";
                PlayerPrefs.SetInt("ActiveWarp", 1);
                MenuAction(22);
            }
            else if (itemCode == 8)
            {
                UIReferenceManager.Instance.bombUnlockButton.interactable = false;
                UIReferenceManager.Instance.bombButton.interactable = true;
                UIReferenceManager.Instance.bombInfoText.text = "Unlocked";
                PlayerPrefs.SetInt("ActiveBomb", 1);
                MenuAction(23);
            }
            else if (itemCode == 9)
            {
                UIReferenceManager.Instance.mainMenuShipUnlockButton.SetActive(false);
                UIReferenceManager.Instance.mainMenuActiveShipIcon.GetComponent<Button>().interactable = true;
                PlayerPrefs.SetInt("Ship" + selectedShipIndex, 1);
                PlayerPrefs.SetInt("SelectedShip", selectedShipIndex);
            }
            UIReferenceManager.Instance.playerMoneyText.text = playerMoney.ToString();
            UIReferenceManager.Instance.popUpMenu.SetActive(false);
        }
    }

    void PopUpNo()
    {
        UIReferenceManager.Instance.popUpMenu.SetActive(false);
    }
}
