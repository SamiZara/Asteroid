using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

    public bool canShoot = true;
    public int tier;
    public GameObject fireSoundObject,explosionSoundObject;
    public AudioSource fireSound;
    // Use this for initialization
    public void Start()
    {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            GameObject temp = (GameObject)Instantiate(fireSoundObject, new Vector3(0, 0, 0), Quaternion.identity, GlobalsManager.Instance.soundParent.transform);
            fireSound = temp.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

