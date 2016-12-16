using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public bool isGameOver;
    public float score,money,scoreMultiplier;
    public int normalAsteroidDestroyCount, specialAsteroidDestroyCount;
    public float Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            GlobalsManager.Instance.gameScoreText.text = ((int)value).ToString();
        }
    }
    public float ScoreMultiplier
    {
        get
        {
            return scoreMultiplier;
        }
        set
        {
            scoreMultiplier = value;
            GlobalsManager.Instance.scoreMultiplier.text = "x" + scoreMultiplier.ToString("0.0");
        }
    }

    public bool isSoundOn = false;

	void Awake () {
        Instance = this;
        //Sound
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            isSoundOn = true;
        //PlayerMoney
        //Background
        SceneManager.LoadScene("Background", LoadSceneMode.Additive);
    }
	
    void Start()
    {     
        //Ship
        int shipState = PlayerPrefs.GetInt("SelectedShip", 0);
        if(shipState == 0)
        {
            GlobalsManager.Instance.player = Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/PlayerShips/PlayerChargerShip"));
            GlobalsManager.Instance.playerController = GlobalsManager.Instance.player.GetComponent<PlayerController>();
        }
        else if (shipState == 1)
        {
            GlobalsManager.Instance.player = Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/PlayerShips/PlayerViperShip"));
            GlobalsManager.Instance.playerController = GlobalsManager.Instance.player.GetComponent<PlayerController>();
        }
        else if (shipState == 2)
        {
            GlobalsManager.Instance.player = Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/PlayerShips/PlayerOrionShip"));
            GlobalsManager.Instance.playerController = GlobalsManager.Instance.player.GetComponent<PlayerController>();
        }
        //Primary weapon instantiate
        int primaryWeaponState = PlayerPrefs.GetInt("Weapon1", 0);
        if(primaryWeaponState == 0)
        {
            GameObject turretWeapon = Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/Weapons/WeaponTurret"), GlobalsManager.Instance.player.transform);
            turretWeapon.transform.localPosition = new Vector3(0, 0, 0);
            int turretState = PlayerPrefs.GetInt("WeaponTurret", 1);
            turretWeapon.GetComponent<Weapon>().tier = turretState;
        }
        else if (primaryWeaponState == 1)
        {
            GameObject gaussWeapon = Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/Weapons/WeaponGauss"), GlobalsManager.Instance.player.transform);
            gaussWeapon.transform.localPosition = new Vector3(0, 0, 0);
            int gaussState = PlayerPrefs.GetInt("WeaponGauss",0);
            gaussWeapon.GetComponent<Weapon>().tier = gaussState;
        }
        else if (primaryWeaponState == 2)
        {
            GameObject plasmaOrbWeapon = Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/Weapons/WeaponPlasmaOrb"), GlobalsManager.Instance.player.transform);
            plasmaOrbWeapon.transform.localPosition = new Vector3(0, 0, -1);
            plasmaOrbWeapon.transform.localRotation = Quaternion.Euler(0, 0, -90);
            int plasmaOrbState = PlayerPrefs.GetInt("WeaponPlasmaOrb",0);
            plasmaOrbWeapon.GetComponent<Weapon>().tier = plasmaOrbState;
        }
        //Secondary weapon instantiate
        int secondaryWeaponState = PlayerPrefs.GetInt("Weapon2", 0);
        if (secondaryWeaponState == 0)
        {
            GameObject missileWeapon = Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/Weapons/WeaponMissile"), GlobalsManager.Instance.player.transform);
            missileWeapon.transform.localPosition = new Vector3(0, 0, 0);
            int missileState = PlayerPrefs.GetInt("WeaponMissile", 1);
            missileWeapon.GetComponent<Weapon>().tier = missileState;
        }
        else if (secondaryWeaponState == 1)
        {
            GameObject rocketWeapon = Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/Weapons/WeaponRocket"), GlobalsManager.Instance.player.transform);
            rocketWeapon.transform.localPosition = new Vector3(0, 0, 0);
            int rocketState = PlayerPrefs.GetInt("WeaponRocket", 0);
            rocketWeapon.GetComponent<Weapon>().tier = rocketState;
        }
        else if (secondaryWeaponState == 2)
        {
            GameObject teslaWeapon = Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/Weapons/WeaponTesla"), GlobalsManager.Instance.player.transform);
            teslaWeapon.transform.localPosition = new Vector3(0, 0, 0);
            int teslaState = PlayerPrefs.GetInt("WeaponTesla", 0);
            teslaWeapon.GetComponent<Weapon>().tier = teslaState;
        }
        //ActiveSkills
        int activeSkillState = PlayerPrefs.GetInt("ActiveSkill", 0);
        if(activeSkillState == 0)
        {
            GameObject shieldActive = Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/ActiveSkills/ImmunityShield"), GlobalsManager.Instance.player.transform);
            shieldActive.transform.localPosition = new Vector3(0, 0, 0);
            GlobalsManager.Instance.player.GetComponent<PlayerController>().activeSkill = shieldActive;
            GlobalsManager.Instance.activeSkillCooldown = Constants.SHIELD_COOLDOWN;
            GlobalsManager.Instance.activeSkillIcon.sprite = ResourceManager.Instance.AllocateAndDumpImage("UI/Icons/icons-active-im-shield");
        }
        else if(activeSkillState == 1)
        {
            GameObject dashActive = Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/ActiveSkills/Dash"), GlobalsManager.Instance.player.transform);
            dashActive.transform.localPosition = new Vector3(0, 0, 0);
            GlobalsManager.Instance.player.GetComponent<PlayerController>().activeSkill = dashActive;
            GlobalsManager.Instance.activeSkillCooldown = Constants.DASH_COOLDOWN;
            GlobalsManager.Instance.activeSkillIcon.sprite = ResourceManager.Instance.AllocateAndDumpImage("UI/Icons/icons-active-dash");
        }
        else if(activeSkillState == 2)
        {
            GameObject timeWarpActive = Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/ActiveSkills/TimeWarpBubble"), GlobalsManager.Instance.player.transform);
            timeWarpActive.transform.localPosition = new Vector3(0, 0, 0);
            GlobalsManager.Instance.player.GetComponent<PlayerController>().activeSkill = timeWarpActive;
            GlobalsManager.Instance.activeSkillCooldown = Constants.TIME_WARP_COOLDOWN;
            GlobalsManager.Instance.activeSkillIcon.sprite = ResourceManager.Instance.AllocateAndDumpImage("UI/Icons/icons-active-time-warp");
        }
        else if(activeSkillState == 3)
        {
            GameObject bigBombActive = Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/ActiveSkills/BigBomb"), GlobalsManager.Instance.player.transform);
            bigBombActive.transform.localPosition = new Vector3(0, 0, 0);
            GlobalsManager.Instance.player.GetComponent<PlayerController>().activeSkill = bigBombActive;
            GlobalsManager.Instance.activeSkillCooldown = Constants.BIG_BOMB_COOLDOWN;
            GlobalsManager.Instance.activeSkillIcon.sprite = ResourceManager.Instance.AllocateAndDumpImage("UI/Icons/icons-active-bomb");
        }
        GlobalsManager.Instance.activeSkillIcon.SetNativeSize();
    }

    public void GameOver()
    {
        isGameOver = true;
        StartCoroutine(GameUIManager.Instance.GameOverSequence());
    }
	
    public void GainMoneyFromVideo()
    {
        int playerMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
        PlayerPrefs.SetInt("PlayerMoney", (int)(playerMoney + money));
    }
}
