using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public Scene currentScene,scene2;
	void Awake () {
        Instance = this;
        //Application.LoadLevelAdditiveAsync("Background");
        SceneManager.LoadScene("Background", LoadSceneMode.Additive);
    }
	
    void Start()
    {
        //Primary weapon instantiate
        int primaryWeaponState = PlayerPrefs.GetInt("Weapon1", 0);
        if(primaryWeaponState == 0)
        {
            GameObject turretWeapon = (GameObject)Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/Weapons/WeaponTurret"), GlobalsManager.Instance.player.transform);
            turretWeapon.transform.localPosition = new Vector3(0, 0, 0);
            int turretState = PlayerPrefs.GetInt("WeaponTurret", 1);
            turretWeapon.GetComponent<Weapon>().tier = turretState;
        }
        else if (primaryWeaponState == 1)
        {
            GameObject gaussWeapon = (GameObject)Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/Weapons/WeaponGauss"), GlobalsManager.Instance.player.transform);
            gaussWeapon.transform.localPosition = new Vector3(0, 0, 0);
            int gaussState = PlayerPrefs.GetInt("WeaponGauss",0);
            gaussWeapon.GetComponent<Weapon>().tier = gaussState;
        }
        else if (primaryWeaponState == 2)
        {
            GameObject beamWeapon = (GameObject)Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/Weapons/WeaponLaser"), GlobalsManager.Instance.player.transform);
            beamWeapon.transform.localPosition = new Vector3(0, 0, -1);
            beamWeapon.transform.localRotation = Quaternion.Euler(0, 0, -90);
            int beamState = PlayerPrefs.GetInt("WeaponBeam",0);
            beamWeapon.GetComponent<Weapon>().tier = beamState;
        }
        //Secondary weapon instantiate
        int secondaryWeaponState = PlayerPrefs.GetInt("Weapon2", 0);
        if (secondaryWeaponState == 0)
        {
            GameObject rocketWeapon = (GameObject)Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/Weapons/WeaponRocket"), GlobalsManager.Instance.player.transform);
            rocketWeapon.transform.localPosition = new Vector3(0, 0, 0);
            int rocketState = PlayerPrefs.GetInt("WeaponRocket", 1);
            rocketWeapon.GetComponent<Weapon>().tier = rocketState;
        }
        else if (secondaryWeaponState == 1)
        {
            GameObject missileWeapon = (GameObject)Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/Weapons/WeaponMissile"), GlobalsManager.Instance.player.transform);
            missileWeapon.transform.localPosition = new Vector3(0, 0, 0);
            int missileState = PlayerPrefs.GetInt("WeaponMissile", 0);
            missileWeapon.GetComponent<Weapon>().tier = missileState;
        }
        else if (secondaryWeaponState == 2)
        {
            GameObject teslaWeapon = (GameObject)Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/Weapons/WeaponTesla"), GlobalsManager.Instance.player.transform);
            teslaWeapon.transform.localPosition = new Vector3(0, 0, 0);
            int teslaState = PlayerPrefs.GetInt("WeaponTesla", 0);
            teslaWeapon.GetComponent<Weapon>().tier = teslaState;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
