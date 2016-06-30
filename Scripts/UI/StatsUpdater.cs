using UnityEngine;
using System.Collections;

public class StatsUpdater : MonoBehaviour {


    void Awake() {

    }
	// Use this for initialization
	void Start () {
        
	}

    // Update is called once per frame
    public void updateStats() {
        
    }

    void OnEnable() {
        InvokeRepeating("updateStats", 0, 0.3f);
    }

    void OnDisable() {
        CancelInvoke("updateStats");
    }
}
