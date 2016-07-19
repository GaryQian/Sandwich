
using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour {
    bool ready = false;
	// Use this for initialization
	void Start () {
        Invoke("setReady", 630f);
        Invoke("RequestInterstitial", 600f);
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

    void showAd() {
        Invoke("playInterstitial", 3f);
    }


    void OnEnable() {
        ButtonHandler.BuyMcdandwich += showAd;
        ButtonHandler.BuySandwichCity += showAd;
        ButtonHandler.BuyBreadCloning += showAd;
        ButtonHandler.BuySandwocracy += showAd;
        ButtonHandler.BuySandriaLaw += showAd;
        ButtonHandler.BuySandwichPlanet += showAd;
        ButtonHandler.BuyHumanExtermination += showAd;
        ButtonHandler.BuySandwichFleet += showAd;
        ButtonHandler.BuyEnslaveAliens += showAd;
        ButtonHandler.BuyDeathSandwich += showAd;
        ButtonHandler.BuySandwichGalaxy += showAd;
    }

    void OnDisable() {
        ButtonHandler.BuyMcdandwich -= showAd;
        ButtonHandler.BuySandwichCity -= showAd;
        ButtonHandler.BuyBreadCloning -= showAd;
        ButtonHandler.BuySandwocracy -= showAd;
        ButtonHandler.BuySandriaLaw -= showAd;
        ButtonHandler.BuySandwichPlanet -= showAd;
        ButtonHandler.BuyHumanExtermination -= showAd;
        ButtonHandler.BuySandwichFleet -= showAd;
        ButtonHandler.BuyEnslaveAliens -= showAd;
        ButtonHandler.BuyDeathSandwich -= showAd;
        ButtonHandler.BuySandwichGalaxy -= showAd;
    }
}
