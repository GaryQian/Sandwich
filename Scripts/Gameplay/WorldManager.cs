using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {
    public GameObject breadPrefab;
    public GameObject activeBread;
	// Use this for initialization
	void Start () {
        activeBread = Instantiate(breadPrefab);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void spawnBread() {
        activeBread = Instantiate(breadPrefab);
    }
}
