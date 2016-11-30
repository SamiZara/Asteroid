using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Collections;

public class LeaderboardManager : MonoBehaviour {

    public static LeaderboardManager Instance;

    #if UNITY_ANDROID
    void Awake()
    {
        Instance = this;
    }

	void Start () {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // require access to a player's Google+ social graph (usually not needed)
        .RequireGooglePlus()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success) => {
            if (success)
            {
                Debug.Log("Login success");
                Social.ReportScore(PlayerPrefs.GetInt("HighScore", 0), "CgkIwJ2wkI8fEAIQAA", (bool successScore) => {
                    if (successScore)
                        Debug.Log("Report score success");
                    else
                        Debug.Log("Report score fail");                    
                });
            }
            else
            {
                Debug.Log("Login fail");
            }
        });
    }
	
	public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }
#endif
}
