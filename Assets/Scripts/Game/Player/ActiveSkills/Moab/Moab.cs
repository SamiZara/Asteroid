using UnityEngine;
using System.Collections;

public class Moab : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject projectile = (GameObject)Instantiate(ResourceManager.Instance.AllocateAndDump("Prefabs/ActiveSkillsProjectiles/MOAB/MoabProjectile"), transform.position+new Vector3(0,0,-1), transform.parent.rotation);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
