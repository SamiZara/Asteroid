using UnityEngine;
using System.Collections;

public class ImmunityShield : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        GlobalsManager.Instance.player.GetComponent<PlayerController>().isImmune = true;
        StartCoroutine(DelayedDisable());
    }

    void OnDisable()
    {
        GlobalsManager.Instance.player.GetComponent<PlayerController>().isImmune = false;
    }

    IEnumerator DelayedDisable()
    {
        yield return new WaitForSeconds(Constants.IMMUNE_SHIELD_TIME);
        gameObject.SetActive(false);
    }
}
