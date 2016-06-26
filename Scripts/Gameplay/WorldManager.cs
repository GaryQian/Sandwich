using UnityEngine;
using System.Collections;

public enum MenuType {stats, sandwich, producer, permanent, shop}

public class WorldManager : MonoBehaviour {
    public GameObject breadPrefab;
    public GameObject activeBread;
    public GameObject sauce;
    public GameplayTouchManager gtm;
    public EconomyManager em;

    public MenuType menuState = MenuType.producer;
    // Use this for initialization
    void Awake() {
        gtm = GetComponent<GameplayTouchManager>();
        em = GetComponent<EconomyManager>();
    }

	void Start () {
        activeBread = Instantiate(breadPrefab);
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void spawnBread() {
        activeBread = Instantiate(breadPrefab);
    }

    public string encodeNumber(double money) {
        return Util.encodeNumber(money);
    }
}
