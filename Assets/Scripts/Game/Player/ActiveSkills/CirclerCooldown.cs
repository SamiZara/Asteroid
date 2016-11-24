using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CirclerCooldown : MonoBehaviour
{

    public Image cooldownSprite;
    void Start()
    {

    }

    void OnEnable()
    {
        cooldownSprite.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownSprite.fillAmount -= 1 / GlobalsManager.Instance.activeSkillCooldown * Time.deltaTime;
        if (cooldownSprite.fillAmount <= 0)
        {
            GlobalsManager.Instance.activateSkillButton.interactable = true;
            gameObject.SetActive(false);
        }
    }
}
