using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum UpgradeType {sandwich, producer, ad, iap, permanent, sharpenKnifes, other}

public class Upgrade : MonoBehaviour {
    public double baseCost;

    public UpgradeType type;
    public ProducerType producerType;

    public GameObject icon;
    public GameObject title;
    public GameObject counterText;
    public GameObject nameText;
    public GameObject costText;
    public GameObject statsText;
    public GameObject buyButtonText;

    string normalTitle;

    public double cost;

    void Awake() {
        //wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        //icon = transform.FindChild("Icon").gameObject;
        title = transform.FindChild("Title").gameObject;
        costText = transform.FindChild("CostText").gameObject;
        switch (type) {
            case UpgradeType.producer:
                counterText = transform.FindChild("CounterText").gameObject;
                statsText = transform.FindChild("StatsText").gameObject;
                normalTitle = title.GetComponent<Text>().text;
                break;
            case UpgradeType.sandwich:
                statsText = transform.FindChild("StatsText").gameObject;
                nameText = transform.FindChild("NameText").gameObject;
                icon = transform.FindChild("Icon").gameObject;
                break;
            case UpgradeType.sharpenKnifes:
                counterText = transform.FindChild("CounterText").gameObject;
                statsText = transform.FindChild("StatsText").gameObject;
                break;
        }
    }
	// Use this for initialization
	void Start () {
	    
	}

    public void setupProducerUpgrade(double num, double rate, double baseCost) {
        this.baseCost = baseCost;
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

    public void updateCounter(string str) {
        counterText.GetComponent<Text>().text = str;
    }

    public void updateStats(string str) {
        statsText.GetComponent<Text>().text = str;
    }

    public void updateTitle(string str) {
        title.GetComponent<Text>().text = str;
    }

    void updateTitleQ() {
        updateTitle("???");
    }

    public void updateName(string str) {
        nameText.GetComponent<Text>().text = str;
    }

    public void updateIcon(Sprite s) {
        icon.GetComponent<Image>().sprite = s;
    }
    //sandwichCart, deli, autochef, mcdandwich, sandwichCity, breadCloning, sandwocracy, sandriaLaw, sandwichPlanet, humanExtermination, enslaveAliens, deathSandwich, sandwichGalaxy, flyingSandwichMonster
    public void showProducerTitleCheck() {
        if (type == UpgradeType.producer) {
            switch (producerType) {
                case ProducerType.sandwichCity:
                    if (Util.em.deliCount == 0) updateTitleQ();
                    else updateTitle(normalTitle);
                    break;
                case ProducerType.breadCloning:
                    if (Util.em.autochefCount == 0) updateTitleQ();
                    else updateTitle(normalTitle);
                    break;
                case ProducerType.sandwocracy:
                    if (Util.em.mcdandwichCount == 0) updateTitleQ();
                    else updateTitle(normalTitle);
                    break;
                case ProducerType.sandriaLaw:
                    if (Util.em.sandwichCityCount == 0) updateTitleQ();
                    else updateTitle(normalTitle);
                    break;
                case ProducerType.sandwichPlanet:
                    if (Util.em.breadCloningCount == 0) updateTitleQ();
                    else updateTitle(normalTitle);
                    break;
                case ProducerType.humanExtermination:
                    if (Util.em.sandwocracyCount == 0) updateTitleQ();
                    else updateTitle(normalTitle);
                    break;
                case ProducerType.enslaveAliens:
                    if (Util.em.sandriaLawCount == 0) updateTitleQ();
                    else updateTitle(normalTitle);
                    break;
                case ProducerType.deathSandwich:
                    if (Util.em.sandwichPlanetCount == 0) updateTitleQ();
                    else updateTitle(normalTitle);
                    break;
                case ProducerType.sandwichGalaxy:
                    if (Util.em.humanExterminationCount == 0) updateTitleQ();
                    else updateTitle(normalTitle);
                    break;
                case ProducerType.flyingSandwichMonster:
                    if (Util.em.enslaveAliensCount == 0) updateTitleQ();
                    else updateTitle(normalTitle);
                    break;
            }
        }
    }

    void OnEnable() {
        if (type == UpgradeType.producer) {
            InvokeRepeating("showProducerTitleCheck", 0, 1f);
        }
    }

    void OnDisable() {
        CancelInvoke("showProducerTitleCheck");
    }
    
}
