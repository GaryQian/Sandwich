using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum UpgradeType {sandwich, producer, ad, iap, permanent}

public class Upgrade : MonoBehaviour {
    public double baseCost;

    public UpgradeType type;
    public GameObject icon;
    public GameObject title;
    public GameObject counterText;
    public GameObject nameText;
    public GameObject costText;
    public GameObject statsText;
    public GameObject buyButtonText;

    public double cost;

    //private WorldManager wm;

    void Awake() {
        //wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        icon = transform.FindChild("Icon").gameObject;
        title = transform.FindChild("Title").gameObject;
        costText = transform.FindChild("CostText").gameObject;
        switch (type) {
            case UpgradeType.producer:
                counterText = transform.FindChild("CounterText").gameObject;
                statsText = transform.FindChild("StatsText").gameObject;
                break;
            case UpgradeType.sandwich:
                statsText = transform.FindChild("StatsText").gameObject;
                nameText = transform.FindChild("NameText").gameObject;
                break;
        }
    }
	// Use this for initialization
	void Start () {
	    
	}

    public void setupProducerUpgrade(double num, double rate) {
        updateCost(baseCost * Mathf.Pow(Util.pScale, (float)num));
        updateCounter(num);
        updateStats("+" + Util.encodeNumber(rate) + " &/s");
    }

    public void updateCost(double cost) {
        costText.GetComponent<Text>().text = "$" + Util.encodeNumber(cost);
        this.cost = cost;
    }

    public void updateCounter(double num) {
        counterText.GetComponent<Text>().text = Util.encodeNumberInteger((int)num);
    }

    public void updateStats(string str) {
        statsText.GetComponent<Text>().text = str;
    }

    public void updateTitle(string str) {
        title.GetComponent<Text>().text = str;
    }

    public void updateName(string str) {
        nameText.GetComponent<Text>().text = str;
    }

    public void updateIcon(Sprite s) {
        icon.GetComponent<Image>().sprite = s;
    }
    
}
