using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public static MenuManager Instace;
    private int itemCode;//Decides which item is being bought for popup menu
    private int[,] weaponCosts;


    void Start()
    {
        
        UIResourceManager.Instance.AllocateAndStore("UI/Buttons/bt-upgrade", "ButtonUpgrade");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icon-Turret", "TurretIcon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icon-GaussGun", "GaussIcon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icon-Beam", "BeamIcon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icon-Rocket", "RocketIcon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icon-HomMissile", "MissileIcon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icon-TeslaGun", "TeslaIcon");
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
            UIReferenceManager.Instance.mainMenuSecondaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["BeamIcon"];
            UIReferenceManager.Instance.mainMenuSecondaryWeapon.SetNativeSize();
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
            UIReferenceManager.Instance.mainMenuSecondaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["HomMissileIcon"];
            UIReferenceManager.Instance.mainMenuSecondaryWeapon.SetNativeSize();
            UIReferenceManager.Instance.mainMenuSecondaryWeaponText.text = "Homming Missile";
        }
        else if (PlayerPrefs.GetInt("Weapon2", 0) == 2)
        {
            UIReferenceManager.Instance.mainMenuSecondaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["TeslaIcon"];
            UIReferenceManager.Instance.mainMenuSecondaryWeapon.SetNativeSize();
            UIReferenceManager.Instance.mainMenuSecondaryWeaponText.text = "Tesla Shield";
        }
    }

    void Awake()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("PlayerMoney", 1000000);
        Instace = this;
        UIReferenceManager.Instance.playerMoneyText.text = PlayerPrefs.GetInt("PlayerMoney", 0).ToString()+"$";

        weaponCosts = new int[6,3];
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
                if(turretState != 0 && turretState < 3)
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
                    if(gaussState < 3)
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
                    if(beamState < 3)
                        UIReferenceManager.Instance.beamInfoText.text = "Tier:" + (beamState) + "\nUpgrade Cost: " + weaponCosts[2, beamState] + "";
                    if (beamState >= 3)
                    {
                        UIReferenceManager.Instance.beamUpgradeButton.GetComponent<Button>().interactable = false;
                        UIReferenceManager.Instance.beamInfoText.text = "Tier:" + (beamState) + "\nMaxed";
                    }
                }
                MenuAction(PlayerPrefs.GetInt("Weapon1", 0) + 2);
                int secondaryWeaponState = PlayerPrefs.GetInt("Weapon2", 0);
                if(secondaryWeaponState == 0)
                {
                    UIReferenceManager.Instance.iconSecondaryActiveWeapon1.sprite = UIResourceManager.Instance.storedAllocations["RocketIcon"];
                }
                else if(secondaryWeaponState == 1)
                {
                    UIReferenceManager.Instance.iconSecondaryActiveWeapon1.sprite = UIResourceManager.Instance.storedAllocations["MissileIcon"];
                }
                else if(secondaryWeaponState == 2)
                {
                    UIReferenceManager.Instance.iconSecondaryActiveWeapon1.sprite = UIResourceManager.Instance.storedAllocations["TeslaIcon"];
                }
                UIReferenceManager.Instance.iconSecondaryActiveWeapon1.SetNativeSize();
                break;
            case 2:
                UIReferenceManager.Instance.iconSelectedWeapon1.localPosition = new Vector3(-305,117,0);
                UIReferenceManager.Instance.iconActiveWeapon1.sprite = UIResourceManager.Instance.storedAllocations["TurretIcon"];
                UIReferenceManager.Instance.iconActiveWeapon1.SetNativeSize();
                UIReferenceManager.Instance.mainMenuPrimaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["TurretIcon"];
                UIReferenceManager.Instance.mainMenuPrimaryWeapon.SetNativeSize();
                PlayerPrefs.SetInt("Weapon1", 0);
                break;
            case 3:
                UIReferenceManager.Instance.iconSelectedWeapon1.localPosition = new Vector3(0, 117, 0);
                UIReferenceManager.Instance.iconActiveWeapon1.sprite = UIResourceManager.Instance.storedAllocations["GaussIcon"];
                UIReferenceManager.Instance.iconActiveWeapon1.SetNativeSize();
                UIReferenceManager.Instance.mainMenuPrimaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["GaussIcon"];
                UIReferenceManager.Instance.mainMenuPrimaryWeapon.SetNativeSize();
                PlayerPrefs.SetInt("Weapon1", 1);
                break;
            case 4:
                UIReferenceManager.Instance.iconSelectedWeapon1.localPosition = new Vector3(305, 117, 0);
                UIReferenceManager.Instance.iconActiveWeapon1.sprite = UIResourceManager.Instance.storedAllocations["BeamIcon"];
                UIReferenceManager.Instance.iconActiveWeapon1.SetNativeSize();
                UIReferenceManager.Instance.mainMenuPrimaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["BeamIcon"];
                UIReferenceManager.Instance.mainMenuPrimaryWeapon.SetNativeSize();
                PlayerPrefs.SetInt("Weapon1", 2);
                break;
            case 5:
                UIReferenceManager.Instance.windowsWeapon1.SetActive(false);
                break;
            case 6:
                itemCode = 0;
                int state = PlayerPrefs.GetInt("WeaponTurret", 1);
                ShowPopUp("Turret Gun Tier "+ (state + 1), weaponCosts[itemCode, state].ToString(), 0);      
                break;
            case 7:
                itemCode = 1;
                state = PlayerPrefs.GetInt("WeaponGauss", 0);
                ShowPopUp("Gauss Gun Tier " + (state + 1), weaponCosts[itemCode, state].ToString(), 0);
                break;
            case 8:
                itemCode = 2;
                state = PlayerPrefs.GetInt("WeaponBeam", 0);
                ShowPopUp("Beam Tier " + (state + 1), weaponCosts[itemCode, state].ToString(), 0);
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
                UIReferenceManager.Instance.iconSelectedWeapon2.localPosition = new Vector3(-305, 117, 0);
                UIReferenceManager.Instance.iconActiveWeapon2.sprite = UIResourceManager.Instance.storedAllocations["RocketIcon"];
                UIReferenceManager.Instance.mainMenuSecondaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["RocketIcon"];
                UIReferenceManager.Instance.mainMenuSecondaryWeapon.SetNativeSize();
                PlayerPrefs.SetInt("Weapon2", 0);
                break;
            case 13:
                UIReferenceManager.Instance.iconSelectedWeapon2.localPosition = new Vector3(0, 117, 0);
                UIReferenceManager.Instance.iconActiveWeapon2.sprite = UIResourceManager.Instance.storedAllocations["MissileIcon"];
                UIReferenceManager.Instance.mainMenuSecondaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["MissileIcon"];
                UIReferenceManager.Instance.mainMenuSecondaryWeapon.SetNativeSize();
                PlayerPrefs.SetInt("Weapon2", 1);
                break;
            case 14:
                UIReferenceManager.Instance.iconSelectedWeapon2.localPosition = new Vector3(305, 117, 0);
                UIReferenceManager.Instance.iconActiveWeapon2.sprite = UIResourceManager.Instance.storedAllocations["TeslaIcon"];
                UIReferenceManager.Instance.mainMenuSecondaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["TeslaIcon"];
                UIReferenceManager.Instance.mainMenuSecondaryWeapon.SetNativeSize();
                PlayerPrefs.SetInt("Weapon2", 2);
                break;
            case 15:
                UIReferenceManager.Instance.windowsWeapon2.SetActive(false);
                break;
            case 16:
                itemCode = 3;
                state = PlayerPrefs.GetInt("WeaponRocket", 1);
                ShowPopUp("Rocket Gun Tier " + (state + 1), weaponCosts[itemCode, state].ToString(), 0);
                break;
            case 17:
                itemCode = 4;
                state = PlayerPrefs.GetInt("WeaponMissile", 0);
                ShowPopUp("Homming Missile Tier " + (state + 1), weaponCosts[itemCode, state].ToString(), 0);
                break;
            case 18:
                itemCode = 5;
                state = PlayerPrefs.GetInt("WeaponTesla", 0);
                ShowPopUp("Beam Tier " + (state + 1), weaponCosts[itemCode, state].ToString(), 0);
                break;
        }
    }

    void ShowPopUp(string item,string cost,int itemCode)
    {
        UIReferenceManager.Instance.popUpMenu.SetActive(true);
        UIReferenceManager.Instance.popUpMenuText.text = "Do you want to buy "+item+" for "+cost+"?";
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
        //Deciding cost
        cost = weaponCosts[itemCode, state];
        //Checking money
        if (playerMoney >= cost)
        {
            playerMoney -= cost;
            PlayerPrefs.SetInt("PlayerMoney", playerMoney);
            state += 1;
            if (itemCode == 0)
            { 
                if(state < 3)
                {
                    UIReferenceManager.Instance.turretInfoText.text = "Tier:"+(state)+"\nUpgrade Cost: "+weaponCosts[0,state]+"";
                }
                if(state  == 3)
                {
                    UIReferenceManager.Instance.turretButton.interactable = false;
                    UIReferenceManager.Instance.turretInfoText.text = "Tier:"+(state)+"\nMaxed";
                    UIReferenceManager.Instance.turretUpgradeButton.GetComponent<Button>().interactable = false;
                }
                PlayerPrefs.SetInt("WeaponTurret", state);
            }
            else if (itemCode == 1)
            {
                if (state == 1)
                {
                    UIReferenceManager.Instance.gaussUpgradeButton.sprite = UIResourceManager.Instance.storedAllocations["ButtonUpgrade"];
                    UIReferenceManager.Instance.gaussUpgradeButton.SetNativeSize();
                    UIReferenceManager.Instance.gaussButton.interactable = true;
                }
                if(state < 3)
                {
                    UIReferenceManager.Instance.gaussInfoText.text = "Tier:" + ((state)) + "\nUpgrade Cost: " + weaponCosts[1, state] + "";
                }
                if(state == 3)
                {
                    UIReferenceManager.Instance.gaussInfoText.text = "Tier:" + ((state)) + "\nMaxed";
                    UIReferenceManager.Instance.gaussUpgradeButton.GetComponent<Button>().interactable = false;
                }
                PlayerPrefs.SetInt("WeaponGauss", state );
            }
            else if (itemCode == 2)
            {
                if (state == 1)
                {
                    UIReferenceManager.Instance.beamUpgradeButton.sprite = UIResourceManager.Instance.storedAllocations["ButtonUpgrade"];
                    UIReferenceManager.Instance.beamUpgradeButton.SetNativeSize();
                    UIReferenceManager.Instance.beamButton.interactable = true;
                }
                if (state  < 3)
                {
                    UIReferenceManager.Instance.beamInfoText.text = "Tier:" + ((state)) + "\nUpgrade Cost: " + weaponCosts[2, state] + "";
                }
                if (state == 3)
                {
                    UIReferenceManager.Instance.beamInfoText.text = "Tier:" + ((state)) + "\nMaxed";
                    UIReferenceManager.Instance.beamUpgradeButton.GetComponent<Button>().interactable = false;
                }
                PlayerPrefs.SetInt("WeaponBeam", state);
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
                    PlayerPrefs.SetInt("WeaponRocket", state);
                }
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
            }
            UIReferenceManager.Instance.playerMoneyText.text = playerMoney.ToString() + "$";
            UIReferenceManager.Instance.popUpMenu.SetActive(false);
        }
    }

    void PopUpNo()
    {
        UIReferenceManager.Instance.popUpMenu.SetActive(false);
    }
}
