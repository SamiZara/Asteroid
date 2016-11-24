using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{

    public static GameUIManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void MenuAction(int option)
    {
        switch (option)
        {
            case 0:
                SceneManager.LoadScene("Game");
                break;
            case 1:
                SceneManager.LoadScene("Menu");
                break;
            case 2:
                GlobalsManager.Instance.gamePauseScreen.SetActive(true);
                Time.timeScale = 0;
                break;
            case 3:
                GlobalsManager.Instance.gamePauseScreen.SetActive(false);
                Time.timeScale = 1;
                break;
            case 4:
                GlobalsManager.Instance.playerController.ActivateSkill();
                break;
        }
    }

    public IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(2);
        int score = (int)GameManager.Instance.score;
        int money = (int)GameManager.Instance.money;
        int asteroidsDestroyed = (int)GameManager.Instance.normalAsteroidDestroyCount;
        int specialAsteroidDestroyed = (int)GameManager.Instance.specialAsteroidDestroyCount;
        /*int score = 1500;
        int money = 1500;
        int asteroidsDestroyed = 1500;
        int specialAsteroidDestroyed = 1500;*/
        //Saving Money
        int playerMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
        PlayerPrefs.SetInt("PlayerMoney",playerMoney+money);
        //Checking highscore
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if(highScore < score)
        {
            PlayerPrefs.SetInt("HighScore",score);
            //Add Leaderboard 
        }

        GlobalsManager.Instance.gameOverScreen.SetActive(true);
        int valueGainAcceleration = 0;
        int currentValue = 0;
        for(int i=0;i< score; i++)
        {
            if (currentValue + i + valueGainAcceleration + 1 <= score)
            {
                GlobalsManager.Instance.gameOverScoreText.text = (currentValue + valueGainAcceleration + 1).ToString();
                currentValue += i + valueGainAcceleration;
            }
            else
            {
                GlobalsManager.Instance.gameOverScoreText.text = (score).ToString();
                break;
            }
            if(i % 5 == 0)
                valueGainAcceleration += 1;
            yield return new WaitForSeconds(0.03f);

        }
        valueGainAcceleration = 0;
        currentValue = 0;
        for (int i = 0; i < money; i++)
        {
            Debug.Log("fdsfds");
            if (currentValue + i + valueGainAcceleration + 1 <= money)
            {
                GlobalsManager.Instance.gameOverMoneyText.text = (currentValue + valueGainAcceleration + 1).ToString();
                currentValue += i + valueGainAcceleration;
            }
            else
            {
                GlobalsManager.Instance.gameOverMoneyText.text = (money).ToString();
                break;
            }
            if (i % 5 == 0)
                valueGainAcceleration += 1;
            yield return new WaitForSeconds(0.03f);
        }
        valueGainAcceleration = 0;
        currentValue = 0;
        for (int i = 0; i < asteroidsDestroyed; i++)
        {
            if (currentValue + i + valueGainAcceleration + 1 <= asteroidsDestroyed)
            {
                GlobalsManager.Instance.gameOverAsteroidsText.text = (currentValue + valueGainAcceleration + 1).ToString();
                currentValue += i + valueGainAcceleration;
            }
            else
            {
                GlobalsManager.Instance.gameOverAsteroidsText.text = (asteroidsDestroyed).ToString();
                break;
            }
            if (i % 5 == 0)
                valueGainAcceleration += 1;
            yield return new WaitForSeconds(0.03f);
        }
        valueGainAcceleration = 0;
        currentValue = 0;
        for (int i = 0; i < specialAsteroidDestroyed; i++)
        {
            if (currentValue + i + valueGainAcceleration + 1 <= specialAsteroidDestroyed)
            {
                GlobalsManager.Instance.gameOverSpecialAsteroidText.text = (currentValue + valueGainAcceleration + 1).ToString();
                currentValue += i + valueGainAcceleration;
            }
            else
            {
                GlobalsManager.Instance.gameOverSpecialAsteroidText.text = (specialAsteroidDestroyed).ToString();
                break;
            }
            if (i % 5 == 0)
                valueGainAcceleration += 1;
            yield return new WaitForSeconds(0.03f);
        }
        yield return null;
    }
}
