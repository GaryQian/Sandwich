using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public enum MenuType {stats, sandwich, producer, permanent, shop}

public class WorldManager : MonoBehaviour {
    //ADVERT VARS
    public string gameIDAndroid;
    public string gameIDiOS;
    public string zoneID;
    public double adWatchTime;
    //
    public GameObject breadPrefab;
    public GameObject activeBread;
    public GameObject sauce;
    public GameplayTouchManager gtm;
    public EconomyManager em;
    public ButtonHandler buttonHandler;
    public StoryManager sm;

    

    public bool muted = false;
    public int playthroughCount = 0;

    public Animator shopGlowAnimator;

    public MenuType menuState = MenuType.producer;
    // Use this for initialization
    void Awake() {
        gtm = GetComponent<GameplayTouchManager>();
        em = GetComponent<EconomyManager>();
        buttonHandler = GetComponent<ButtonHandler>();
        sm = GetComponent<StoryManager>();

        Util.wm = this;
        setupUtil();
    }

	void Start () {
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
    }

    void setupUtil() {
        Util.screenToCanvasRatio = Screen.height / 1920f;
    }
	
	// Update is called once per frame
	void Update () {
        if (adWatchTime > 0) {
            adWatchTime -= Time.deltaTime;
        }
	}

    public void spawnBread() {
        activeBread = Instantiate(breadPrefab);
    }

    public string encodeNumber(double money) {
        return Util.encodeNumber(money);
    }

    public void checkAdTimer() {
        if (adWatchTime <= 0 && em.gameTime > 400f) {
            shopGlowAnimator.SetTrigger("Pulse");
        }
        else {
            shopGlowAnimator.SetTrigger("Off");
        }
    }
}
