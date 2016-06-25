using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {
    public GameObject breadPrefab;
    public GameObject activeBread;
    public GameplayTouchManager gtm;
    public EconomyManager em;
	// Use this for initialization
	void Start () {
        activeBread = Instantiate(breadPrefab);
        gtm = GetComponent<GameplayTouchManager>();
        em = GetComponent<EconomyManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void spawnBread() {
        activeBread = Instantiate(breadPrefab);
    }
}
