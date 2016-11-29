using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class AdManager : MonoBehaviour {

    public static AdManager Instance;
	
    void Awake()
    {
        Instance = this;
    }

    public void ShowVideo()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;
        Advertisement.Show("", options);
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log ("Video completed.");
                GameManager.Instance.GainMoneyFromVideo();
                break;
            case ShowResult.Skipped:
                Debug.LogWarning ("Video was skipped.");
                break;
            case ShowResult.Failed:
                Debug.LogError("Video failed to show.");
                break;
        }
    }
}
