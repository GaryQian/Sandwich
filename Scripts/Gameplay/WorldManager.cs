using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;

public enum MenuType {stats, sandwich, producer, permanent, shop}

public class WorldManager : MonoBehaviour {
    //ADVERT VARS
    public string gameIDAndroid;
    public string gameIDiOS;
    public string zoneID;
    public double adWatchTimeMoney;
    public double adWatchTimeElixir;
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
    public double x3Time;
    public double x7Time;
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
    public TabManager tabManager;
    public GameObject canvas;
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

    public Animator shopGlowAnimator;

    public MenuType menuState = MenuType.producer;

    /// <summary>
    /// EVENTS
    /// </summary>
    /// 
    public delegate void EconStatus();
    public static event EconStatus Reached500;
    public static event EconStatus Reached50;
    //


    private bool scorePostFailed = false;

    // Use this for initialization
    void Awake() {
        gtm = GetComponent<GameplayTouchManager>();
        em = GetComponent<EconomyManager>();
        buttonHandler = GetComponent<ButtonHandler>();
        //tabManager = GetComponent<TabManager>();
        //sm = GetComponent<StoryManager>();
        tutorialManager = GetComponent<TutorialManager>();

        music = GetComponent<AudioSource>();

        Util.wm = this;
        setupUtil();

    }

	void Start () {
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
        

        muteButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-(Screen.width / Util.screenToCanvasRatio / 2f) + 40.5f , -40.5f, 0);
        musicMuteButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-(Screen.width / Util.screenToCanvasRatio / 2f) + 40.5f, -105.5f, 0);

        boostMultiplierText.GetComponent<RectTransform>().anchoredPosition = new Vector3((Screen.width / Util.screenToCanvasRatio / 2f) - 15f, -50f, 0);
        boostTimer.GetComponent<RectTransform>().anchoredPosition = new Vector3((Screen.width / Util.screenToCanvasRatio / 2f) - 18f, -125f, 0);

        InvokeRepeating("checkAdTimer", 10f, 10f);
        InvokeRepeating("everySecond", 0, 1f);
        InvokeRepeating("every5", 5.5f, 10f);
        Invoke("spawnSandWitch", UnityEngine.Random.Range(Util.sandWitchDelay * 0.75f, Util.sandWitchDelay * 1.25f));

        Bread.updateLabel();

        loadIAP();

        canvas.GetComponent<Canvas>().sortingGridNormalizedSize = 5;

        if (Application.platform == RuntimePlatform.WindowsEditor) Util.godmode = true;
    }

    public void processOffline() {
        double timeElapsed = GetComponent<WebDateTime>().GetCurrentTime().Subtract(lastTime).TotalSeconds;
        if (timeElapsed > 0) {
            adWatchTimeElixir -= timeElapsed / timeScaleDivisor;
            adWatchTimeMoney -= timeElapsed / timeScaleDivisor;

            em.nurseryPop += em.rate * em.reproductionRate * timeElapsed / 100f;
        }
    }

    public void setupGPGS() {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();


        //PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();


        //LEADERBOARDS:

        try {
            Social.localUser.Authenticate((bool success) => {
                // handle success or failure
                if (success) postScore();
            });
        }
        catch (Exception e) {
            Debug.LogError("ERROR AUTHENTICATION: " + e.Message);
        }
    }

    public void postScore() {
        if (!hasCheated) {
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
        }
    }

    void everySecond() {
        gtm.knife.GetComponent<Knife>().setupKnifeType();
        x3Time--;
        x7Time--;
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
        else {
            x7Multiplier = 1f;
            x3Multiplier = 1f;
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

        em.nurseryPop += em.rate * em.reproductionRate / 100f;
    }

    void every5() {
        if (em.totalMoney < 1E+18f) {
            if (em.money >= 1E+6f) Social.ReportProgress("CgkI1rDm6sMKEAIQBA", 100.0f, (bool success) => { });
            if (em.money >= 1E+9f) Social.ReportProgress("CgkI1rDm6sMKEAIQBQ", 100.0f, (bool success) => { });
            if (em.money >= 1E+12f) Social.ReportProgress("CgkI1rDm6sMKEAIQBw", 100.0f, (bool success) => { });
            if (em.money >= 1E+15f) Social.ReportProgress("CgkI1rDm6sMKEAIQBg", 100.0f, (bool success) => { });

            if (em.totalSwipes >= 100) Social.ReportProgress("CgkI1rDm6sMKEAIQCQ", 100.0f, (bool success) => { }); //sandwich chef
        }
        else if (em.totalMoney < 1E+30f) {
            if (em.money >= 1E+18f) Social.ReportProgress("CgkI1rDm6sMKEAIQCA", 100.0f, (bool success) => { });
            if (em.money >= 1E+21f) Social.ReportProgress("CgkI1rDm6sMKEAIQCg", 100.0f, (bool success) => { });
            if (em.money >= 1E+24f) Social.ReportProgress("CgkI1rDm6sMKEAIQCw", 100.0f, (bool success) => { });
            if (em.money >= 1E+27f) Social.ReportProgress("CgkI1rDm6sMKEAIQDw", 100.0f, (bool success) => { });

            if (em.totalSwipes >= 100) Social.ReportProgress("CgkI1rDm6sMKEAIQCQ", 100.0f, (bool success) => { }); //sandwich chef
            if (em.humanExterminationCount >= 1) Social.ReportProgress("CgkI1rDm6sMKEAIQAQ", 100.0f, (bool success) => { }); //New World Order
            if (em.flyingSandwichMonsterCount >= 1) Social.ReportProgress("CgkI1rDm6sMKEAIQAw", 100.0f, (bool success) => { }); //All Hail
        }
        else {
            if (em.money >= 1E+30f) Social.ReportProgress("CgkI1rDm6sMKEAIQEA", 100.0f, (bool success) => { });
            if (em.money >= 1E+33f) Social.ReportProgress("CgkI1rDm6sMKEAIQEQ", 100.0f, (bool success) => { });
            if (em.money >= 1E+36f) Social.ReportProgress("CgkI1rDm6sMKEAIQEg", 100.0f, (bool success) => { });
            if (em.money >= 1E+39d) Social.ReportProgress("CgkI1rDm6sMKEAIQEw", 100.0f, (bool success) => { });
            if (em.money >= 1E+32d) Social.ReportProgress("CgkI1rDm6sMKEAIQFA", 100.0f, (bool success) => { });
            if (em.money >= 1E+35d) Social.ReportProgress("CgkI1rDm6sMKEAIQFQ", 100.0f, (bool success) => { });
            if (em.money >= 1E+38d) Social.ReportProgress("CgkI1rDm6sMKEAIQFg", 100.0f, (bool success) => { });
            if (em.money >= 1E+41d) Social.ReportProgress("CgkI1rDm6sMKEAIQFw", 100.0f, (bool success) => { });

            if (em.flyingSandwichMonsterCount >= 1) Social.ReportProgress("CgkI1rDm6sMKEAIQAw", 100.0f, (bool success) => { }); //All Hail
        }
    }

    public void initializeBGMusic() {
        buttonHandler.toggleMute();
        buttonHandler.toggleMute();

        buttonHandler.toggleMusicMute();
        buttonHandler.toggleMusicMute();
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
        if (adWatchTimeMoney > 0) {
            adWatchTimeMoney -= Time.deltaTime;
        }
        if (adWatchTimeElixir > 0) {
            adWatchTimeElixir -= Time.deltaTime;
        }
        Util.even = !Util.even;
    }

    public void spawnBread() {
        activeBread = Instantiate(breadPrefab);
    }

    public string encodeNumber(double money) {
        return Util.encodeNumber(money);
    }

    public void checkAdTimer() {
        if ((adWatchTimeMoney <= 0 || adWatchTimeElixir <= 0) && em.gameTime > 400f) {
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
        data.x3Time = x3Time;
        data.x7Time = x7Time;

        data.hasCheated = hasCheated;

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

            hasCheated = data.hasCheated;

            saveVersion();
        }
    }

    void OnApplicationQuit() {
        em.save();
    }
}

[Serializable]
public class Version {
    public int version;
}


[Serializable]
public class IAPData {
    public bool knifeCollectionPurchased;
    public int knifeID;
    public double x3Time;
    public double x7Time;

    public bool hasCheated;

    public SaberColor saberColor;
}