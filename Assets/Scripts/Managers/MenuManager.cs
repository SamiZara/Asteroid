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
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icon-HomMissile", "HomMissileIcon");
        UIResourceManager.Instance.AllocateAndStore("UI/Icons/icon-TeslaGun", "TeslaShieldIcon");
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
            UIReferenceManager.Instance.mainMenuSecondaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["TeslaGunIcon"];
            UIReferenceManager.Instance.mainMenuSecondaryWeapon.SetNativeSize();
            UIReferenceManager.Instance.mainMenuSecondaryWeaponText.text = "Tesla Shield";
        }
    }

    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("PlayerMoney", 1000000);
        Instace = this;
        UIReferenceManager.Instance.playerMoneyText.text = PlayerPrefs.GetInt("PlayerMoney", 0).ToString()+"$";

        weaponCosts = new int[3,3];
        weaponCosts[0, 0] = 0;
        weaponCosts[0, 1] = 50;
        weaponCosts[0, 2] = 200;
        weaponCosts[1, 0] = 75;
        weaponCosts[1, 1] = 150;
        weaponCosts[1, 2] = 450;
        weaponCosts[2, 0] = 100;
        weaponCosts[2, 1] = 200;
        weaponCosts[2, 2] = 650;
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
                int gausState = PlayerPrefs.GetInt("WeaponGauss", 0);
                if (gausState > 0)
                {
                    UIReferenceManager.Instance.gaussButton.interactable = true;
                    UIReferenceManager.Instance.gaussUpgradeButton.sprite = UIResourceManager.Instance.storedAllocations["ButtonUpgrade"];
                    UIReferenceManager.Instance.gaussUpgradeButton.SetNativeSize();
                    if(gausState < 3)
                        UIReferenceManager.Instance.gaussInfoText.text = "Tier:" + (gausState) + "\nUpgrade Cost: " + weaponCosts[1, gausState] + "";
                    else if (gausState >= 3)
                    {
                        UIReferenceManager.Instance.gaussUpgradeButton.GetComponent<Button>().interactable = false;
                        UIReferenceManager.Instance.gaussInfoText.text = "Tier:" + (gausState) + "\nMaxed";
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
                break;
            case 2:
                UIReferenceManager.Instance.iconSelectedWeapon.localPosition = new Vector3(-305,117,0);
                UIReferenceManager.Instance.iconActiveWeapon.sprite = UIResourceManager.Instance.storedAllocations["TurretIcon"];
                UIReferenceManager.Instance.mainMenuPrimaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["TurretIcon"];
                UIReferenceManager.Instance.mainMenuPrimaryWeapon.SetNativeSize();
                PlayerPrefs.SetInt("Weapon1", 0);
                break;
            case 3:
                UIReferenceManager.Instance.iconSelectedWeapon.localPosition = new Vector3(0, 117, 0);
                UIReferenceManager.Instance.iconActiveWeapon.sprite = UIResourceManager.Instance.storedAllocations["GaussIcon"];
                UIReferenceManager.Instance.mainMenuPrimaryWeapon.sprite = UIResourceManager.Instance.storedAllocations["GaussIcon"];
                UIReferenceManager.Instance.mainMenuPrimaryWeapon.SetNativeSize();
                PlayerPrefs.SetInt("Weapon1", 1);
                break;
            case 4:
                UIReferenceManager.Instance.iconSelectedWeapon.localPosition = new Vector3(305, 117, 0);
                UIReferenceManager.Instance.iconActiveWeapon.sprite = UIResourceManager.Instance.storedAllocations["BeamIcon"];
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
                PlayerPrefs.SetInt("WeaponTurret", state );
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
            UIReferenceManager.Instance.playerMoneyText.text = playerMoney.ToString() + "$";
            UIReferenceManager.Instance.popUpMenu.SetActive(false);
        }
    }

    void PopUpNo()
    {
        UIReferenceManager.Instance.popUpMenu.SetActive(false);
    }
}
