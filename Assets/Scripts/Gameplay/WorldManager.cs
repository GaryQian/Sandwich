using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SocialPlatforms;
using GoogleMobileAds.Api;
#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif

public enum MenuType {stats, sandwich, producer, permanent, shop}

public class WorldManager : MonoBehaviour {
    //ADVERT VARS
    public string gameIDAndroid;
    public string gameIDiOS;
    public string zoneID;
    public double adWatchTimeMoney;
    public double adWatchTimeElixir;
    public double adWatchTimex2;
    //

    //Versioning
    public int version = 9;
    public int savedVersion = -1;
    //


    //IAP
    public bool knifeCollectionPurchased = false;
    public int knifeID = 0;
    public SaberColor saberColor = SaberColor.blue;
    public int flowerColor = 40000;
    public int shoeColor = 40000;
    public double x2Time;
    public double x3Time;
    public double x7Time;
    public double x2Multiplier = 1f;
    public double x3Multiplier = 1f;
    public double x7Multiplier = 1f;
    //

    public GameObject breadPrefab;
    public GameObject activeBread;
    public GameObject sauce;
    public GameObject sandWitchPrefab;
    public GameObject sandWitch;
    public GameObject boostMultiplierText;
    public GameObject boostTimer;
    public int sandWitchesClicked = 0;

    public bool hasCheated = false;

    public GameplayTouchManager gtm;
    public EconomyManager em;
    public ButtonHandler buttonHandler;
    public StoryManager sm;
    public AchievementManager am;
    public AdManager adm;
    public TabManager tabManager;
    public GameObject canvas;
    public UpdateNursery nursery;
    public TutorialManager tutorialManager;
    public GameObject muteButton;
    public GameObject musicMuteButton;

    public DateTime lastTime;
    public float timeScaleDivisor;

    public bool muted = false;
    public bool musicMuted = false;
    public AudioSource music;
    public int playthroughCount = 0;

    public AudioSource halfAudioSource;
    public AudioSource fullAudioSource;
    public AudioSource fullAudioSource2;

    public Animator shopGlowAnimator;

    public MenuType menuState = MenuType.producer;

    public GameObject nurseryAlert;
    public GameObject nurseryAlertPrefab;

    public Util util;

    /// <summary>
    /// EVENTS
    /// </summary>
    /// 
    public delegate void EconStatus();
    public static event EconStatus Reached500;
    public static event EconStatus Reached50;
    //


    //ADS
    public InterstitialAd interstitial;

    private bool scorePostFailed = false;

    // Use this for initialization
    void Awake() {

        music = GetComponent<AudioSource>();

        Util.wm = this;
        setupUtil();

    }

	void Start () {
        Application.targetFrameRate = 30;
        
        if (Application.genuineCheckAvailable) if (!Application.genuine) hasCheated = true;

        util = new Util();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        activeBread = Instantiate(breadPrefab);
        if (Advertisement.isSupported) { // If the platform is supported,
            if (Application.platform == RuntimePlatform.Android) {
                Advertisement.Initialize(gameIDAndroid); // initialize Unity Ads.
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer) {
                Advertisement.Initialize(gameIDiOS); // initialize Unity Ads.
            }
        }
        

        muteButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-(Screen.width / Util.screenToCanvasRatio / 2f) + 75f , -75f, 0);
        musicMuteButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-(Screen.width / Util.screenToCanvasRatio / 2f) + 40.5f, -105.5f, 0);

        boostMultiplierText.GetComponent<RectTransform>().anchoredPosition = new Vector3((Screen.width / Util.screenToCanvasRatio / 2f) - 15f, -50f, 0);
        boostTimer.GetComponent<RectTransform>().anchoredPosition = new Vector3((Screen.width / Util.screenToCanvasRatio / 2f) - 18f, -125f, 0);

        InvokeRepeating("checkAdTimer", 5f, 17f);
        InvokeRepeating("everySecond", 0, 1f);
        InvokeRepeating("every5", 3f, 5f);
        InvokeRepeating("every10", 5.5f, 10f);
        Invoke("spawnSandWitch", UnityEngine.Random.Range(Util.sandWitchDelay * 0.75f, Util.sandWitchDelay * 1.25f));

        Bread.updateLabel();

        loadIAP();

        canvas.GetComponent<Canvas>().sortingGridNormalizedSize = 5;

        if (Application.platform == RuntimePlatform.WindowsEditor) Util.godmode = true;
    }

    public void processOffline() {
        DateTime dt = UnbiasedTime.Instance.Now();
        if (dt != null) {
            double timeElapsed = dt.Subtract(lastTime).TotalSeconds;
            if (timeElapsed > 0) {
                adWatchTimeElixir -= timeElapsed / timeScaleDivisor;
                adWatchTimeMoney -= timeElapsed / timeScaleDivisor;
                adWatchTimex2 -= timeElapsed / timeScaleDivisor;
                em.nurseryPop += em.rate * em.reproductionRate * timeElapsed / 100f;
                em.maxBabyPop = Util.em.rate * Util.em.reproductionRate * Util.maxBabyTime / 100f;
                if (em.nurseryPop > em.maxBabyPop && em.nurseryPop != 0) em.nurseryPop = em.maxBabyPop;
            }
        }
    }

    public void spawnAlert() {
        em.maxBabyPop = Util.em.rate * Util.em.reproductionRate * Util.maxBabyTime / 100f;
        if (menuState != MenuType.sandwich && nurseryAlert == null && em.nurseryPop == em.maxBabyPop && em.nurseryPop != 0) {
            nurseryAlert = Instantiate(nurseryAlertPrefab);
            nurseryAlert.transform.SetParent(Util.wm.canvas.transform);
            nurseryAlert.GetComponent<RectTransform>().anchoredPosition = new Vector3(-200f, -304f, 0);
            nurseryAlert.transform.localScale = new Vector3(1f, 1f, 1f);
            nurseryAlert.transform.SetAsFirstSibling();
        }
    }

    private void RequestInterstitial() {
        #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-3270795222614514/2236020819";
        #elif UNITY_IPHONE
                string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
        #else
                string adUnitId = "unexpected_platform";
        #endif

    #if UNITY_ANDROID
        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    #endif
    }

    void playInterstitial() {
        #if UNITY_ANDROID
        if (interstitial.IsLoaded()) {
            interstitial.Show();
            Invoke("RequestInterstitial", 5f);
        }
        #endif
    }


    public void setupGPGS() {
    #if UNITY_ANDROID
        //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        Debug.Log("Activating GPGS");

        //PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        //PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
        Debug.Log("GPGS Activated");
        try {
            Debug.Log("Authenticating GPGS...");
            Social.localUser.Authenticate((bool success) => {
                Debug.Log("GPGS Authenticated, Posting scores.");
                // handle success or failure
                if (success) postScore();
                Debug.Log("Scores Posted.");
            });
        }
        catch (Exception e) {
            Debug.LogError("ERROR AUTHENTICATION: " + e.Message);
        }
    #endif
    }

    public void postScore() {
        if (!hasCheated) {
        #if UNITY_ANDROID
            scorePostFailed = false;
            //total money
            Social.ReportScore((long)em.totalMoney, "CgkI1rDm6sMKEAIQDQ", (bool success) => {
                // handle success or failure
                if (!success) {
                    scorePostFailed = true;
                }
            });
            //total elixirs
            Social.ReportScore((long)em.totalElixir, "CgkI1rDm6sMKEAIQDg", (bool success) => {
                // handle success or failure
                if (!success) {
                    scorePostFailed = true;
                }
            });
            //total swipes
            Social.ReportScore((long)em.totalSwipes, "CgkI1rDm6sMKEAIQDA", (bool success) => {
                // handle success or failure
                if (!success) {
                    scorePostFailed = true;
                }
            });

            if (scorePostFailed) Invoke("postScore", 300f);
        #endif
        }
    }

    void everySecond() {
        gtm.knife.GetComponent<Knife>().setupKnifeType();
        x2Time--;
        x3Time--;
        x7Time--;
        adWatchTimeMoney--;
        adWatchTimeElixir--;
        adWatchTimex2--;
        if (x7Time > 0) {
            x7Multiplier = 7f;
            x3Multiplier = 1f;
            boostMultiplierText.SetActive(true);
            boostTimer.SetActive(true);
            boostMultiplierText.GetComponent<Text>().text = "x7";
            boostTimer.GetComponent<Text>().text = Util.encodeTimeShort(x7Time);
            em.updateLabels();
            em.multiplierGlow.show();
        }
        else if (x3Time > 0) {
            x7Multiplier = 1f;
            x3Multiplier = 3f;
            boostMultiplierText.SetActive(true);
            boostTimer.SetActive(true);
            boostMultiplierText.GetComponent<Text>().text = "x3";
            boostTimer.GetComponent<Text>().text = Util.encodeTimeShort(x3Time);
            em.updateLabels();
            em.multiplierGlow.show();
        }
        else if (x2Time > 0) {
            x7Multiplier = 1f;
            x3Multiplier = 1f;
            x2Multiplier = 2f;
            boostMultiplierText.SetActive(true);
            boostTimer.SetActive(true);
            boostMultiplierText.GetComponent<Text>().text = "x2";
            boostTimer.GetComponent<Text>().text = Util.encodeTimeShort(x2Time);
            em.updateLabels();
            em.multiplierGlow.show();
        }
        else {
            x7Multiplier = 1f;
            x3Multiplier = 1f;
            x2Multiplier = 1f;
            em.updateLabels();
            boostMultiplierText.SetActive(false);
            boostTimer.SetActive(false);
            em.updateLabels();
        }

        if (tutorialManager.tutorialActive) {
            if (Util.em.money >= 500f && Util.em.totalMoney < 1500f) {
                if (Reached500 != null) Reached500();
            }
            if (Util.em.money >= 50f && Util.em.sandwichCartCount == 0) {
                if (Reached50 != null) Reached50();
            }
            
        }

        em.maxBabyPop = Util.em.rate * Util.em.reproductionRate * Util.maxBabyTime / 100f;
        em.nurseryPop += em.rate * em.reproductionRate / 100f;
        if (em.nurseryPop > em.maxBabyPop && em.nurseryPop != 0) em.nurseryPop = em.maxBabyPop;
    }

    void every5() {

    }

    void every10() {
        if (!hasCheated) {
            am.checkMoneyAcheivements();
        }
        
        spawnAlert();
    }

    public void initializeBGMusic() {
        buttonHandler.toggleMute();
        buttonHandler.toggleMute();

        //buttonHandler.toggleMusicMute();
        //buttonHandler.toggleMusicMute();
    }

    void setupUtil() {
        Util.screenToCanvasRatio = Screen.height / 1920f;
        Util.screenCenterXCoord = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2f, 0f)).x;
        Util.worldBottomLeftPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Util.worldTopRightPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Util.worldHeight = Util.worldTopRightPos.y - Util.worldBottomLeftPos.y;
        Util.worldWidth = Util.worldTopRightPos.x - Util.worldBottomLeftPos.x;

        Util.worldNormBottomLeftPos = Camera.main.ScreenToWorldPoint(new Vector2((Screen.width - Screen.height / 1.77777f) / 2f, 0));
        Util.worldNormTopRightPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - ((Screen.width - Screen.height / 1.77777f) / 2f), Screen.height));
        Util.worldNormalizedWidth = Util.worldNormTopRightPos.x - Util.worldNormBottomLeftPos.x;
    }
	
	// Update is called once per frame
	void Update () {
        Util.even = !Util.even;
    }

    public void spawnBread() {
        activeBread = Instantiate(breadPrefab);
    }

    public string encodeNumber(double money) {
        return Util.encodeNumber(money);
    }

    public void checkAdTimer() {
        if ((adWatchTimeMoney <= 0 || adWatchTimeElixir <= 0 || adWatchTimex2 <= 0) && em.gameTime > 400f) {
            shopGlowAnimator.SetTrigger("Pulse");
        }
        else {
            shopGlowAnimator.SetTrigger("Off");
        }
    }

    public void spawnSandWitch() {
        sandWitch = Instantiate(sandWitchPrefab);
        Invoke("spawnSandWitch", UnityEngine.Random.Range(Util.sandWitchDelay * 0.75f, Util.sandWitchDelay * 1.25f));
    }

    public void saveVersion() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/version.dat");

        Version data = new Version();

        data.version = version;

        bf.Serialize(file, data);
        file.Close();
    }

    public int loadVersion() {
        if (File.Exists(Application.persistentDataPath + "/version.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/version.dat", FileMode.Open);
            Version data = (Version)bf.Deserialize(file);
            file.Close();

            savedVersion = data.version;

            saveVersion();
        }
        saveVersion();
        return savedVersion;
    }

    public void saveIAP() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/purchases.dat");

        IAPData data = new IAPData();

        data.knifeCollectionPurchased = knifeCollectionPurchased;
        data.knifeID = knifeID;
        data.saberColor = saberColor;
        if (x3Time < 0) x3Time = 0;
        if (x7Time < 0) x7Time = 0;
        if (x2Time < 0) x2Time = 0;
        data.x3Time = x3Time;
        data.x7Time = x7Time;
        data.x2Time = x2Time;

        bf.Serialize(file, data);
        file.Close();
    }

    public void loadIAP() {
        if (File.Exists(Application.persistentDataPath + "/purchases.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/purchases.dat", FileMode.Open);
            IAPData data = (IAPData)bf.Deserialize(file);
            file.Close();

            knifeCollectionPurchased = data.knifeCollectionPurchased;
            knifeID = data.knifeID;
            saberColor = data.saberColor;
            x3Time = data.x3Time;
            x7Time = data.x7Time;
            x2Time = data.x2Time;

            saveVersion();
        }
    }

    public void saveSettings() {
        lastTime = UnbiasedTime.Instance.Now();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/settings.dat");
        
        SaveSettings data = new SaveSettings();
        
        data.logoffTime = lastTime;
        data.muted = muted;
        data.musicMuted = musicMuted;
        data.hasCheated = hasCheated;

        bf.Serialize(file, data);
        file.Close();
    }

    public void loadTime() {
        lastTime = new DateTime(9998, 1, 1);
        if (File.Exists(Application.persistentDataPath + "/settings.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/settings.dat", FileMode.Open);
            SaveSettings data = null;
            try {
                data = (SaveSettings)bf.Deserialize(file);
            }
            catch { }
            file.Close();

            if (data != null) lastTime = data.logoffTime;
            muted = data.muted;
            musicMuted = data.musicMuted;
            hasCheated = data.hasCheated;

            saveVersion();
        }
    }

    void OnApplicationQuit() {
        em.save();
    }

    void OnApplicationFocus(bool pauseStatus) {
        if (pauseStatus) {
            //In forground
            processOffline();
        }
        else {
            lastTime = UnbiasedTime.Instance.Now();
        }
    }
}

[Serializable]
public class Version {
    public int version;
}

[Serializable]
public class SaveSettings {
    public DateTime logoffTime;

    public bool muted;
    public bool musicMuted;

    public bool hasCheated;
}

[Serializable]
public class IAPData {
    public bool knifeCollectionPurchased;
    public int knifeID;
    public double x3Time;
    public double x7Time;
    public double x2Time;

    public SaberColor saberColor;
}