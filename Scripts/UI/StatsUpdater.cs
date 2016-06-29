using UnityEngine;
using System.Collections;

public class StatsUpdater : MonoBehaviour {


    void Awake() {

    }
	// Use this for initialization
	void Start () {
        //InvokeRepeating("updateStats", 0, 0.3f);
	}

    // Update is called once per frame
    public void updateStats() {
        Debug.LogError("Updating Stats!");
    }

    void OnEnable() {
        InvokeRepeating("updateStats", 0, 0.3f);
    }

    void OnDisable() {
        CancelInvoke("updateStats");
    }
}
