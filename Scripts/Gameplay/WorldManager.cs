using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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
    public int version = 3;
    public int savedVersion = -1;
    //


    //IAP
    public bool knifeCollectionPurchased = false;
    public int knifeID = 0;
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


    // Use this for initialization
    void Awake() {
        gtm = GetComponent<GameplayTouchManager>();
        em = GetComponent<EconomyManager>();
        buttonHandler = GetComponent<ButtonHandler>();
        //tabManager = GetComponent<TabManager>();
        //sm = GetComponent<StoryManager>();
        tutorialManager = GetComponent<TutorialManager>();

        double timeElapsed = DateTime.Now.Subtract(lastTime).TotalSeconds;
        adWatchTimeElixir -= timeElapsed / timeScaleDivisor;
        adWatchTimeMoney -= timeElapsed / timeScaleDivisor;

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
        Invoke("spawnSandWitch", UnityEngine.Random.Range(Util.sandWitchDelay * 0.75f, Util.sandWitchDelay * 1.25f));

        Bread.updateLabel();

        loadIAP();
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
        if (x3Time < 0) x3Time = 0;
        if (x7Time < 0) x7Time = 0;
        data.x3Time = x3Time;
        data.x7Time = x7Time;

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
            x3Time = data.x3Time;
            x7Time = data.x7Time;

            saveVersion();
        }
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
}