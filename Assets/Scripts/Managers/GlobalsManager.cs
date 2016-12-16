using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GlobalsManager : MonoBehaviour {
    public static GlobalsManager Instance;
    public Vector3 screenPos;
    public float asteroidSpeed,activeSkillCooldown;
    public GameObject player,soundParent,gameOverScreen,gamePauseScreen,rewardButtonDimmer,screenCover;
    public CirclerCooldown circlerCooldown;
    public Image activeSkillIcon;
    public AudioSource thrusterSound,asteroidExplosionSound,playerExplosionSound;
    public Text gameScoreText,gameOverScoreText,gameOverMoneyText,gameOverAsteroidsText,gameOverSpecialAsteroidText,videoRewardText,asteroidCountText,scoreMultiplier,waveText;
    public PlayerController playerController;
    public Button activateSkillButton,videoRewardButton;
    public Slider comboTimer;
    // Use this for initialization
    void Awake () {
        Instance = this;
        screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
