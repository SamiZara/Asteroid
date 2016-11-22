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
        }
    }

    public IEnumerator GameOverSequence()
    {
        GlobalsManager.Instance.gameOverScreen.SetActive(true);
        int score = (int)GameManager.Instance.score;
        for(int i=0;i< score; i++)
        {
            GlobalsManager.Instance.gameOverScoreText.text = (i+1).ToString();
            yield return new WaitForSeconds(0.03f);
        }
        int credits = (int)GameManager.Instance.money;
        for(int i = 0; i < credits; i++)
        {
            GlobalsManager.Instance.gameOverCreditsText.text = (i + 1).ToString();
            yield return new WaitForSeconds(0.03f);
        }
        int asteroidsDestroyed = (int)GameManager.Instance.normalAsteroidDestroyCount;
        for (int i = 0; i < asteroidsDestroyed; i++)
        {
            GlobalsManager.Instance.gameOverAsteroidsText.text = (i + 1).ToString();
            yield return new WaitForSeconds(0.03f);
        }
        int specialAsteroidDestroyed = (int)GameManager.Instance.specialAsteroidDestroyCount;
        for (int i = 0; i < specialAsteroidDestroyed; i++)
        {
            GlobalsManager.Instance.gameOverSpecialAsteroidText.text = (i + 1).ToString();
            yield return new WaitForSeconds(0.03f);
        }
        yield return null;
    }
}
