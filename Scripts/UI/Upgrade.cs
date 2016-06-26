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
        icon = transform.FindChild("Icon").gameObject;
        title = transform.FindChild("Title").gameObject;
        costText = transform.FindChild("CostText").gameObject;
        switch (type) {
            case UpgradeType.producer:
                counterText = transform.FindChild("CounterText").gameObject;
                statsText = transform.FindChild("StatsText").gameObject;
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
