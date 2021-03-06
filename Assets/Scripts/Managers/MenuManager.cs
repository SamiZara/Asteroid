﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MenuManager : MonoBehaviour
{

    public static MenuManager Instace;
    private int itemCode, selectedShipIndex;//Decides which item is being bought for popup menu
    private int[,] weaponCosts;
    private int[] activeSkillCosts;
    private int[] shipCosts;
    private int popUpItemCost,turretState,gaussState,plasmaOrbState,rocketState,missileState,teslaState;
    private int TurretState
    {
        get
        {
            return turretState;
        }
        set
        {
            turretState = value;
            if (turretState > 0)
            {
                UIReferenceManager.Instance.turretButton.interactable = true;
                UIReferenceManager.Instance.turretUpgradeButton.sprite = UIResourceManager.Instance.storedAllocations["ButtonUpgrade"];
                UIReferenceManager.Instance.turretUpgradeButton.SetNativeSize();
                if (turretState < 6)
                {
                    UIReferenceManager.Instance.turretInfoText.text = "Tier:" + (turretState) + "\nUpgrade Cost:" + weaponCosts[0, turretState] + "";
                }
                else if (turretState >= 6)
                {
                    UIReferenceManager.Instance.turretUpgradeButton.GetComponent<Button>().interactable = false;
                    UIReferenceManager.Instance.turretInfoText.text = "Tier:" + (turretState) + "\nMaxed";
                }
            }
        }
    }
    private int GaussState
    {
        get
        {
            return gaussState;
        }
        set
        {
            gaussState = value;
            if (gaussState > 0)
            {
                UIReferenceManager.Instance.gaussButton.interactable = true;
                UIReferenceManager.Instance.gaussUpgradeButton.sprite = UIResourceManager.Instance.storedAllocations["ButtonUpgrade"];
                UIReferenceManager.Instance.gaussUpgradeButton.SetNativeSize();
                if (gaussState < 6)
                    UIReferenceManager.Instance.gaussInfoText.text = "Tier:" + (gaussState) + "\nUpgrade Cost:" + weaponCosts[1, gaussState] + "";
                else if (gaussState >= 6)
                {
                    UIReferenceManager.Instance.gaussUpgradeButton.GetComponent<Button>().interactable = false;
                    UIReferenceManager.Instance.gaussInfoText.text = "Tier:" + (gaussState) + "\nMaxed";
                }
            }
        }
    }
    private int PlasmaOrbState
    {
        get
        {
            return plasmaOrbState;
        }
        set
        {
            plasmaOrbState = value;
            if (plasmaOrbState > 0)
            {
                UIReferenceManager.Instance.plasmaOrbButton.interactable = true;
                UIReferenceManager.Instance.plasmaOrbUpgradeButton.sprite = UIResourceManager.Instance.storedAllocations["ButtonUpgrade"];
                UIReferenceManager.Instance.plasmaOrbUpgradeButton.SetNativeSize();
                if (plasmaOrbState < 6)
                    UIReferenceManager.Instance.plasmaOrbInfoText.text = "Tier:" + (plasmaOrbState) + "\nUpgrade Cost:" + weaponCosts[2, plasmaOrbState] + "";
                if (plasmaOrbState >= 6)
                {
                    UIReferenceManager.Instance.plasmaOrbUpgradeButton.GetComponent<Button>().interactable = false;
                    UIReferenceManager.Instance.plasmaOrbInfoText.text = "Tier:" + (plasmaOrbState) + "\nMaxed";
                }
            }
        }
    }
    private int RocketState
    {
        get
        {
            return rocketState;
        }
        set
        {
            rocketState = value;
            if (rocketState > 0)
            {
                UIReferenceManager.Instance.rocketButton.interactable = true;
                UIReferenceManager.Instance.rocketUpgradeButton.sprite = UIResourceManager.Instance.storedAllocations["ButtonUpgrade"];
                UIReferenceManager.Instance.rocketUpgradeButton.SetNativeSize();
                if (rocketState < 6)
                {
                    UIReferenceManager.Instance.rocketInfoText.text = "Tier:" + (rocketState) + "\nUpgrade Cost:" + weaponCosts[4, rocketState] + "";
                }
                else if (rocketState >= 6)
                {
                    UIReferenceManager.Instance.rocketUpgradeButton.GetComponent<Button>().interactable = false;
                    UIReferenceManager.Instance.rocketInfoText.text = "Tier:" + (rocketState) + "\nMaxed";
                }
            }
        }
    }
    private int MissileState
    {
        get
        {
            return missileState;
        }
        set
        {
            missileState = value;
            if (missileState > 0)
            {
                UIReferenceManager.Instance.missileButton.interactable = true;
                UIReferenceManager.Instance.missileUpgradeButton.sprite = UIResourceManager.Instance.storedAllocations["ButtonUpgrade"];
                UIReferenceManager.Instance.missileUpgradeButton.SetNativeSize();
                if (missileState < 6)
                    UIReferenceManager.Instance.missileInfoText.text = "Tier:" + (missileState) + "\nUpgrade Cost:" + weaponCosts[3, missileState] + "";
                else if (missileState >= 6)
                {
                    UIReferenceManager.Instance.missileUpgradeButton.GetComponent<Button>().interactable = false;
                    UIReferenceManager.Instance.missileInfoText.text = "Tier:" + (missileState) + "\nMaxed";
                }
            }
        }
    }
    private int TeslaState
    {
        get
        {
            return teslaState;
        }
        set
        {
            teslaState = value;
            if (teslaState > 0)
            {
                UIReferenceManager.Instance.teslaButton.interactable = true;
                UIReferenceManager.Instance.teslaUpgradeButton.sprite = UIResourceManager.Instance.storedAllocations["ButtonUpgrade"];
                UIReferenceManager.Instance.teslaUpgradeButton.SetNativeSize();
                if (teslaState < 6)
                    UIReferenceManager.Instance.teslaInfoText.text = "Tier:" + (teslaState) + "\nUpgrade Cost:" + weaponCosts[5, teslaState] + "";
                if (teslaState >= 6)
                {
                    UIReferenceManager.Instance.teslaUpgradeButton.GetComponent<Button>().interactable = false;
                    UIReferenceManager.Instance.teslaInfoText.text = "Tier:" + (teslaState) + "\nMaxed";
                }
            }
        }
    }

    void Start()
    {
        //Version
        PlayerPrefs.SetString("Version-OS", "1-Android");
        //Allocation
        UIResourceManager.Instance.AllocateAndStore("UI/Buttons/bt-upgrade", "ButtonUpgrade");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icon-Turret", "TurretIcon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icon-GaussGun", "GaussIcon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icons-weapons-plasma", "PlasmaOrbIcon");
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
            UIReferenceManager.Instance.mainMenuPrimaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["PlasmaOrbIcon"];
            UIReferenceManager.Instance.mainMenuPrimaryWeapon.SetNativeSize();
            UIReferenceManager.Instance.mainMenuPrimaryWeaponText.text = "Plasma Orb";
        }
        //Secondary Weapon     
        if (PlayerPrefs.GetInt("Weapon2", 0) == 0)
        {
            UIReferenceManager.Instance.mainMenuSecondaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["MissileIcon"];
            UIReferenceManager.Instance.mainMenuSecondaryWeapon.SetNativeSize();
            UIReferenceManager.Instance.mainMenuSecondaryWeaponText.text = "Homming";
        }
        else if (PlayerPrefs.GetInt("Weapon2", 0) == 1)
        {
            UIReferenceManager.Instance.mainMenuSecondaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["RocketIcon"];
            UIReferenceManager.Instance.mainMenuSecondaryWeapon.SetNativeSize();
            UIReferenceManager.Instance.mainMenuSecondaryWeaponText.text = "Rocket";
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
        if (PlayerPrefs.GetInt("Sound", 1) == 0)
        {
            UIReferenceManager.Instance.soundOffButton.interactable = false;
            UIReferenceManager.Instance.soundOnButton.interactable = true;
        }
    }

    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("PlayerMoney", 1000000);
        Instace = this;
        UIReferenceManager.Instance.playerMoneyText.text = PlayerPrefs.GetInt("PlayerMoney", 0).ToString();
        //Weapons
        weaponCosts = new int[6, 6];
        weaponCosts[0, 0] = 0;
        weaponCosts[0, 1] = 50;
        weaponCosts[0, 2] = 200;
        weaponCosts[0, 3] = 500;
        weaponCosts[0, 4] = 1250;
        weaponCosts[0, 5] = 3250;
        weaponCosts[1, 0] = 75;
        weaponCosts[1, 1] = 150;
        weaponCosts[1, 2] = 450;
        weaponCosts[1, 3] = 1100;
        weaponCosts[1, 4] = 2750;
        weaponCosts[1, 5] = 6750;
        weaponCosts[2, 0] = 100;
        weaponCosts[2, 1] = 200;
        weaponCosts[2, 2] = 650;
        weaponCosts[2, 3] = 1600;
        weaponCosts[2, 4] = 4050;
        weaponCosts[2, 5] = 10000;
        weaponCosts[3, 0] = 0;
        weaponCosts[3, 1] = 100;
        weaponCosts[3, 2] = 250;
        weaponCosts[3, 3] = 625;
        weaponCosts[3, 4] = 1550;
        weaponCosts[3, 5] = 3900;
        weaponCosts[4, 0] = 100;
        weaponCosts[4, 1] = 175;
        weaponCosts[4, 2] = 500;
        weaponCosts[4, 3] = 1200;
        weaponCosts[4, 4] = 3000;
        weaponCosts[4, 5] = 7500;
        weaponCosts[5, 0] = 125;
        weaponCosts[5, 1] = 250;
        weaponCosts[5, 2] = 750;
        weaponCosts[5, 3] = 1875;
        weaponCosts[5, 4] = 4650;
        weaponCosts[5, 5] = 12000;
        //Actives
        activeSkillCosts = new int[4];
        activeSkillCosts[0] = 0;
        activeSkillCosts[1] = 150;
        activeSkillCosts[2] = 250;
        activeSkillCosts[3] = 400;
        //Ships
        shipCosts = new int[2];
        shipCosts[0] = 2000;
        shipCosts[1] = 5000;
    }

    public void MenuAction(int option)
    {
        switch (option)
        {
            case 0:
                SceneManager.LoadScene("Game");
                UIReferenceManager.Instance.screenCover.SetActive(true);
                break;
            case 1:
                UIReferenceManager.Instance.windowsWeapon1.SetActive(true);
                TurretState = PlayerPrefs.GetInt("WeaponTurret", 1);
                GaussState = PlayerPrefs.GetInt("WeaponGauss", 0);
                PlasmaOrbState = PlayerPrefs.GetInt("WeaponPlasmaOrb", 0);
                MenuAction(PlayerPrefs.GetInt("Weapon1", 0) + 2);
                int secondaryWeaponState = PlayerPrefs.GetInt("Weapon2", 0);
                if (secondaryWeaponState == 0)
                {
                    UIReferenceManager.Instance.iconSecondaryActiveWeapon1.sprite = UIResourceManager.Instance.storedAllocations["MissileIcon"];     
                }
                else if (secondaryWeaponState == 1)
                {
                    UIReferenceManager.Instance.iconSecondaryActiveWeapon1.sprite = UIResourceManager.Instance.storedAllocations["RocketIcon"];
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
                UIReferenceManager.Instance.iconActiveWeapon1.sprite = UIResourceManager.Instance.storedAllocations["PlasmaOrbIcon"];
                UIReferenceManager.Instance.iconActiveWeapon1.SetNativeSize();
                UIReferenceManager.Instance.mainMenuPrimaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["PlasmaOrbIcon"];
                UIReferenceManager.Instance.mainMenuPrimaryWeapon.SetNativeSize();
                UIReferenceManager.Instance.mainMenuPrimaryWeaponText.text = "Plasma Orb";
                PlayerPrefs.SetInt("Weapon1", 2);
                break;
            case 5:
                UIReferenceManager.Instance.windowsWeapon1.SetActive(false);
                break;
            case 6:
                itemCode = 0;
                //int state = PlayerPrefs.GetInt("WeaponTurret", 1);
                ShowPopUp("Turret Gun Tier " + (TurretState + 1), weaponCosts[itemCode, TurretState].ToString());
                break;
            case 7:
                itemCode = 1;
                //state = PlayerPrefs.GetInt("WeaponGauss", 0);
                ShowPopUp("Gauss Gun Tier " + (GaussState + 1), weaponCosts[itemCode, GaussState].ToString());
                break;
            case 8:
                itemCode = 2;
                //state = PlayerPrefs.GetInt("WeaponBeam", 0);
                ShowPopUp("Plasma Orb Tier " + (PlasmaOrbState + 1), weaponCosts[itemCode, PlasmaOrbState].ToString());
                break;
            case 9:
                PopUpYes();
                break;
            case 10:
                PopUpNo();
                break;
            case 11:
                UIReferenceManager.Instance.windowsWeapon2.SetActive(true);
                MissileState = PlayerPrefs.GetInt("WeaponMissile", 1);
                RocketState = PlayerPrefs.GetInt("WeaponRocket", 0); 
                TeslaState = PlayerPrefs.GetInt("WeaponTesla", 0);
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
                    UIReferenceManager.Instance.iconPrimaryActiveWeapon2.sprite = UIResourceManager.Instance.storedAllocations["PlasmaOrbIcon"];
                }
                UIReferenceManager.Instance.iconPrimaryActiveWeapon2.SetNativeSize();
                break;
            case 12:
                UIReferenceManager.Instance.selectedWeapon2Icon.localPosition = new Vector3(-305, 117, 0);
                UIReferenceManager.Instance.iconActiveWeapon2.sprite = UIResourceManager.Instance.storedAllocations["MissileIcon"];
                UIReferenceManager.Instance.mainMenuSecondaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["MissileIcon"];
                UIReferenceManager.Instance.mainMenuSecondaryWeapon.SetNativeSize();
                UIReferenceManager.Instance.mainMenuSecondaryWeaponText.text = "Missile";
                PlayerPrefs.SetInt("Weapon2", 0);
                break;
            case 13:
                UIReferenceManager.Instance.selectedWeapon2Icon.localPosition = new Vector3(0, 117, 0);
                UIReferenceManager.Instance.iconActiveWeapon2.sprite = UIResourceManager.Instance.storedAllocations["RocketIcon"];
                UIReferenceManager.Instance.mainMenuSecondaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["RocketIcon"];
                UIReferenceManager.Instance.mainMenuSecondaryWeapon.SetNativeSize();
                UIReferenceManager.Instance.mainMenuSecondaryWeaponText.text = "Rocket";
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
                //state = PlayerPrefs.GetInt("WeaponMissile", 1);
                ShowPopUp("Homing Missile Tier " + (MissileState + 1), weaponCosts[itemCode, MissileState].ToString());
                break;
            case 17:
                itemCode = 4;
                //state = PlayerPrefs.GetInt("WeaponRocket", 0);
                ShowPopUp("Rocket Tier " + (RocketState + 1), weaponCosts[itemCode, RocketState].ToString());
                break;
            case 18:
                itemCode = 5;
                //state = PlayerPrefs.GetInt("WeaponTesla", 0);
                ShowPopUp("Tesla Tier " + (TeslaState + 1), weaponCosts[itemCode, TeslaState].ToString());
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
                    PlayerPrefs.SetInt("SelectedShip", selectedShipIndex);
                }
                break;
            case 30:
                itemCode = 9;
                if (selectedShipIndex == 1)
                    ShowPopUp("Viper best mobility in the universe", shipCosts[0].ToString());
                else if (selectedShipIndex == 2)
                    ShowPopUp("Orion fastest ship in the universe", shipCosts[1].ToString());
                break;
            case 31:
                UIReferenceManager.Instance.soundOffButton.interactable = false;
                UIReferenceManager.Instance.soundOnButton.interactable = true;
                PlayerPrefs.SetInt("Sound", 0);
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
            case 35:
#if UNITY_ANDROID
                LeaderboardManager.Instance.ShowLeaderboard();
#endif
                break;
        }
    }

    void ShowPopUp(string item, string cost)
    {
        UIReferenceManager.Instance.popUpMenu.SetActive(true);
        UIReferenceManager.Instance.popUpMenuText.text = "Do you want to buy " + item + " for " + cost + "?";
        popUpItemCost = Convert.ToInt32(cost);
        int playerMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
        if (playerMoney < Convert.ToInt32(cost))
        {
            UIReferenceManager.Instance.popUpYesButton.interactable = false;
        }
        else
        {
            UIReferenceManager.Instance.popUpYesButton.interactable = true;
        }

    }

    void PopUpYes()
    {
        int playerMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
        //Checking money
        if (playerMoney >= popUpItemCost)
        {
            playerMoney -= popUpItemCost;
            PlayerPrefs.SetInt("PlayerMoney", playerMoney); 
            if (itemCode == 0)
            {
                TurretState += 1;
                PlayerPrefs.SetInt("WeaponTurret", TurretState);
                MenuAction(2);
            }
            else if (itemCode == 1)
            {
                GaussState += 1;
                PlayerPrefs.SetInt("WeaponGauss", GaussState);
                MenuAction(3);
            }
            else if (itemCode == 2)
            {
                PlasmaOrbState += 1;
                PlayerPrefs.SetInt("WeaponPlasmaOrb", PlasmaOrbState);
                MenuAction(4);
            }  
            else if (itemCode == 3)
            {
                MissileState += 1;
                PlayerPrefs.SetInt("WeaponMissile", MissileState);
                MenuAction(12);
            }
            else if (itemCode == 4)
            {
                RocketState += 1;
                PlayerPrefs.SetInt("WeaponRocket", RocketState);
                MenuAction(13);
            }
            else if (itemCode == 5)
            {
                TeslaState += 1;
                PlayerPrefs.SetInt("WeaponTesla", TeslaState);
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
