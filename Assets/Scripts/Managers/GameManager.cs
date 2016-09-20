using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public Scene currentScene,scene2;
	void Awake () {
        Instance = this;
        //Application.LoadLevelAdditiveAsync("Background");
        //SceneManager.LoadScene("Background", LoadSceneMode.Additive);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
