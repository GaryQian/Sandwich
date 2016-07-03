﻿using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using System;

public enum MenuType {stats, sandwich, producer, permanent, shop}

public class WorldManager : MonoBehaviour {
    //ADVERT VARS
    public string gameIDAndroid;
    public string gameIDiOS;
    public string zoneID;
    public double adWatchTimeMoney;
    public double adWatchTimeElixir;
    //
    public GameObject breadPrefab;
    public GameObject activeBread;
    public GameObject sauce;
    public GameObject sandWitchPrefab;
    public GameObject sandWitch;

    public GameplayTouchManager gtm;
    public EconomyManager em;
    public ButtonHandler buttonHandler;
    public StoryManager sm;
    public GameObject canvas;
    public TutorialManager tutorialManager;

    public DateTime lastTime;
    public float timeScaleDivisor;

    public bool muted = false;
    public AudioSource audio;
    public int playthroughCount = 0;

    public Animator shopGlowAnimator;

    public MenuType menuState = MenuType.producer;
    // Use this for initialization
    void Awake() {
        gtm = GetComponent<GameplayTouchManager>();
        em = GetComponent<EconomyManager>();
        buttonHandler = GetComponent<ButtonHandler>();
        sm = GetComponent<StoryManager>();
        tutorialManager = GetComponent<TutorialManager>();

        double timeElapsed = DateTime.Now.Subtract(lastTime).TotalSeconds;
        adWatchTimeElixir -= timeElapsed / timeScaleDivisor;
        adWatchTimeMoney -= timeElapsed / timeScaleDivisor;

        audio = GetComponent<AudioSource>();

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
        InvokeRepeating("checkAdTimer", 10f, 10f);

        GameObject.Find("MuteButton").GetComponent<RectTransform>().anchoredPosition = new Vector3(-(Screen.width / Util.screenToCanvasRatio / 2f) + 40.5f , -40.5f, 0);

        Invoke("spawnSandWitch", UnityEngine.Random.Range(Util.sandWitchDelay * 0.75f, Util.sandWitchDelay * 1.25f));
    }

    public void initializeBGMusic() {
        if (muted) {
            audio.Pause();
        }
        else {
            audio.Play();
        }
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

    
}
