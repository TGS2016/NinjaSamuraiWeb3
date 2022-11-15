using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdEvent : MonoBehaviour
{

    public static AdEvent insta;
    public string fulliOS, fullAndroid;
    public string fulliOSReward, fullAndroidReward;
    private InterstitialAd interstitial;
    private AdRequest adRequest;

    private RewardedAd rewardedAd;

    public static int rewardtype = 0;

    void Awake()
    {
        //PlayerPrefs.DeleteAll ();
        if (insta == null)
        {
            insta = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //	#if UNITY_ANDROID
        // initAdmobSDK(); // admob sdk check
        //	#endif
        PlayerPrefs.SetInt("adsOn", 0); //enable ad sdk
        Debug.Log("enable ad");

#if UNITY_ANDROID
        MobileAds.Initialize("ca-app-pub-6855821001810960~9627360893");
#else
			MobileAds.Initialize("ca-app-pub-6855821001810960~1274351864");
#endif
        rewardtype = 0;
    }

    // Use this for initialization
    void Start()
    {
        PlayerPrefs.SetInt("rated", PlayerPrefs.GetInt("rated", 0) + 1);
        if ((PlayerPrefs.GetInt("rated", 0) % 5) == 0)
        {
            HomeUIManager.insta.RateScreen.SetActive(true);
        }
        else
        {
            HomeUIManager.insta.RateScreen.SetActive(false);
        }

        initializeInerstitial();
        loadRewardVideo();
    }


    public void initializeInerstitial()
    {
        if (PlayerPrefs.GetInt("Inerstitial", 1) == 1 && PlayerPrefs.GetInt("adsOn", 0) == 0)
        {
#if UNITY_ANDROID
            string adUnitId = fullAndroid;
#else
			string adUnitId = fulliOS;
#endif
            interstitial = new InterstitialAd(adUnitId);

            // Called when the ad is closed.
            interstitial.OnAdClosed += HandleOnAdClosed;

            adRequest = new AdRequest.Builder().Build();
            interstitial.LoadAd(adRequest);
            Debug.Log("Admob Request");
        }
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        initializeInerstitial();
    }

    public void showAd()
    {
        if (PlayerPrefs.GetInt("Inerstitial", 1) == 1 && PlayerPrefs.GetInt("adsOn", 0) == 0)
        {
            if (interstitial != null)
            {
                if (interstitial.IsLoaded())
                {
                    interstitial.Show();
                    Debug.Log("Admob Request Show 1");
                }
                else
                {
                    initializeInerstitial();
                }
            }
            Debug.Log("Admob Request Show 2");
        }
        Debug.Log("Admob Request Show 3");
    }

    //////// Ad Sdk //////////
    void initAdmobSDK()
    {

        string url = "";
#if UNITY_ANDROID
        url = "http://proapi.app4edu.com/api/ninja_android.php?adKey=784"; //android
#else
			url = "http://proapi.app4edu.com/api/ninja_ios.php?adKey=784"; ///ios
#endif
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest3(www));


    }

    IEnumerator WaitForRequest3(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ad sdk : " + www.text);
            if (www.text.ToString().Equals("1"))
            {
                PlayerPrefs.SetInt("adsOn", 1);//disable ad sdk
                Debug.Log("disable ad");
            }
            else if (www.text.ToString().Equals("0"))
            {
                // initializeInerstitial();
                PlayerPrefs.SetInt("adsOn", 0); //enable ad sdk
                Debug.Log("enable ad");
            }
        }
        else
        {
            initializeInerstitial();
            PlayerPrefs.SetInt("adsOn", 1); //disable ad sdk
            Debug.Log("WWW Error: " + www.error);
        }
        //www.Dispose ();
    }


    /// reward video ads
    public void loadRewardVideo()
    {

#if UNITY_ANDROID
        string adUnitId = fullAndroidReward;
#else
			string adUnitId = fulliOSReward;
#endif

        rewardedAd = new RewardedAd(adUnitId);





        // Called when the user should be rewarded for interacting with the ad.
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
    }

    public bool showVideoAds()
    {
        if (!rewardedAd.IsLoaded())
        {
            loadRewardVideo();
            return false;
        }
        else
        {
            rewardedAd.Show();
            return true;
        }

    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        loadRewardVideo();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        if (rewardtype == 1) // ad money reward
        {
            Debug.Log("Ad Played successfully");
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 50) + 50);
            HomeUIManager.insta.MoneyTxt.text = "" + PlayerPrefs.GetInt("Money", 50);
            HomeUIManager.insta.RewardPanel.SetActive(true);
        }
        else if (rewardtype == 2) // contiue game reward
        {
            UIManager.insta.AdWatched = true;
            Debug.Log("Ad Played successfully");

            Manager.State = Manager.gameState.PLAY;
            UIManager.insta.ScoreUI.SetActive(true);
            Instantiate(UIManager.insta.Ninja, UIManager.insta.Ninja.transform.position, UIManager.insta.Ninja.transform.rotation);

            UIManager.insta.IsInterstitialDisplayed = false;
            UIManager.insta.GameOverUI.SetActive(false);
        }
        else if (rewardtype == 3)
        {

        }

    }
}
