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
        int primaryWeaponState = PlayerPrefs.GetInt("Weapon1", 0);
        if(primaryWeaponState == 0)
        {
            GameObject turretWeapon = (GameObject)Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/Weapons/WeaponTurret"), GlobalsManager.Instance.player.transform);
            turretWeapon.transform.localPosition = new Vector3(0, 0, 0);
            int turretState = PlayerPrefs.GetInt("WeaponTurret", 0);
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
            GameObject beamWeapon = (GameObject)Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/Weapons/WeaponBeam"), GlobalsManager.Instance.player.transform);
            beamWeapon.transform.localPosition = new Vector3(0, 0, 0);
            int beamState = PlayerPrefs.GetInt("WeaponBeam",0);
            beamWeapon.GetComponent<Weapon>().tier = beamState;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
