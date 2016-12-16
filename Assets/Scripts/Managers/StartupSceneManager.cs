using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene("Menu");
        if(PlayerPrefs.GetInt("Weapon2",0) == 1)
        {
            if(PlayerPrefs.GetInt("WeaponRocket",0) == 0)
            {
                PlayerPrefs.SetInt("WeaponRocket", 1);
            }
        }
        if(PlayerPrefs.GetInt("WeaponBeam",0) > 0)
        {
            PlayerPrefs.SetInt("WeaponPlasmaOrb", PlayerPrefs.GetInt("WeaponBeam", 0));
            PlayerPrefs.SetInt("WeaponBeam", 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
