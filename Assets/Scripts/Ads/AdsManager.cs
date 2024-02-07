using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsShowListener
{
#if UNITY_ANDROID
    string gameId = "5377764";
#else
    string gameId = "5377765";
#endif
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameId);
    }

    public void PlayAd()
    {
        Advertisement.Show("Interstitial_Android");
    }
    public void PlayRewardedAd()
    {
        Advertisement.Show("Rewarded_Android");
    }
    public void ShowBanner()
    {
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Show("Banner_Android");
    }

    //IEnumerator RepeatShowBanner()
    //{
    //    yield return new WaitForSeconds(1);
    //    ShowBanner();
    //}

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        //throw new System.NotImplementedException();
        Debug.Log("ERROR: " + message);

    }

    public void OnUnityAdsShowStart(string placementId)
    {
        //throw new System.NotImplementedException();
        Debug.Log("AD STARTED");

    }

    public void OnUnityAdsShowClick(string placementId)
    {
        //throw new System.NotImplementedException();
        Debug.Log("AD IS READY");

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        //throw new System.NotImplementedException();
        if (placementId == "Rewarded_Android" && showCompletionState == UnityAdsShowCompletionState.COMPLETED) 
        {
            Debug.Log("PLAYER SHOULD BE REWARDED!");
        }
    }
}
