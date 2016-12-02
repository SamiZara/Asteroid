using UnityEngine;
using System.Collections;
#if UNITY_ANDROID
using UnityEngine.Advertisements;
#endif

public class AdManager : MonoBehaviour {

    public static AdManager Instance;
#if UNITY_ANDROID
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
#endif
}
