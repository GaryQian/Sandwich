using UnityEngine;
using System.Collections;

public enum UpgradeType {sandwich, producer, ad, iap, permanent}

public class Upgrade : MonoBehaviour {
    public UpgradeType type;
    public GameObject icon;
    public GameObject title;
    public GameObject counterText;
    public GameObject costText;
    public GameObject statsText;
    public GameObject buyButtonText;

    void Awake() {
        switch (type) {
            case UpgradeType.producer:

                break;
            case UpgradeType.sandwich:


                break;
        }
    }
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
