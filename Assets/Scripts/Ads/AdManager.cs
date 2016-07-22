
using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {
    bool ready = false;
    public RewardBasedVideoAd rewardBasedVideo;
    // Use this for initialization
    void Start () {
        Invoke("setReady", 630f);
        Invoke("RequestInterstitial", 600f);
        Invoke("RequestRewardBasedVideo", 3f);
    }

    void setReady() {
        ready = true;
    }

    private void RequestInterstitial() {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3270795222614514/2236020819";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3270795222614514/1125887612";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        Util.wm.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        Util.wm.interstitial.LoadAd(request);
    }

    void playInterstitial() {
        if (ready && Util.wm.interstitial.IsLoaded()) {
            Util.wm.interstitial.Show();
            Invoke("RequestInterstitial", 5f);
            ready = false;
            Invoke("setReady", Util.interstitialFrequency * Random.Range(0.8f, 1.2f));
        }
    }

    void showInterstitialAd() {
        Invoke("playInterstitial", 3f);
    }

    private void RequestRewardBasedVideo() {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3270795222614514/9412707214";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3270795222614514/1889440416";
#else
        string adUnitId = "unexpected_platform";
#endif

        rewardBasedVideo = RewardBasedVideoAd.Instance;

        AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideo.LoadAd(request, adUnitId);
    }


    void HandleRewardBasedVideoRewarded(object sender, Reward args) {
        switch (ButtonHandler.adWatchType) {
            case AdWatchType.money:
                {
                    Util.wm.buttonHandler.HandleShowResultMoney(ShowResult.Finished);
                    break;
                }
            case AdWatchType.elixir:
                {
                    Util.wm.buttonHandler.HandleShowResultElixir(ShowResult.Finished);
                    break;
                }
            case AdWatchType.x2boost:
                {
                    Util.wm.buttonHandler.HandleShowResultx2(ShowResult.Finished);
                    break;
                }
            case AdWatchType.other: break;
        }
        RequestRewardBasedVideo();
    }


    void OnEnable() {
        ButtonHandler.BuyMcdandwich += showInterstitialAd;
        ButtonHandler.BuySandwichCity += showInterstitialAd;
        ButtonHandler.BuyBreadCloning += showInterstitialAd;
        ButtonHandler.BuySandwocracy += showInterstitialAd;
        ButtonHandler.BuySandriaLaw += showInterstitialAd;
        ButtonHandler.BuySandwichPlanet += showInterstitialAd;
        ButtonHandler.BuyHumanExtermination += showInterstitialAd;
        ButtonHandler.BuySandwichFleet += showInterstitialAd;
        ButtonHandler.BuyEnslaveAliens += showInterstitialAd;
        ButtonHandler.BuyDeathSandwich += showInterstitialAd;
        ButtonHandler.BuySandwichGalaxy += showInterstitialAd;

        // has rewarded the user.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
    }

    void OnDisable() {
        ButtonHandler.BuyMcdandwich -= showInterstitialAd;
        ButtonHandler.BuySandwichCity -= showInterstitialAd;
        ButtonHandler.BuyBreadCloning -= showInterstitialAd;
        ButtonHandler.BuySandwocracy -= showInterstitialAd;
        ButtonHandler.BuySandriaLaw -= showInterstitialAd;
        ButtonHandler.BuySandwichPlanet -= showInterstitialAd;
        ButtonHandler.BuyHumanExtermination -= showInterstitialAd;
        ButtonHandler.BuySandwichFleet -= showInterstitialAd;
        ButtonHandler.BuyEnslaveAliens -= showInterstitialAd;
        ButtonHandler.BuyDeathSandwich -= showInterstitialAd;
        ButtonHandler.BuySandwichGalaxy -= showInterstitialAd;
    }
}
