using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class ButtonHandler : MonoBehaviour {

    public EconomyManager em;
    private WorldManager wm;
    public GameObject red;

    void Awake() {
        em = GetComponent<EconomyManager>();
        wm = GetComponent<WorldManager>();
    }
	// Use this for initialization
	void Start () {
	
	}

    void notEnough() {
        red.SetActive(true);
        red.GetComponent<Animator>().SetTrigger("Flash");
        Invoke("disableRed", 0.267f);
    }

    void disableRed() {
        red.SetActive(false);
    }

    public void watchAd() {
        if (wm.adWatchTime <= 0) {
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResult;

            Advertisement.Show(wm.zoneID, options);
        }
        else {
            em.list.transform.FindChild("AdForMoney").transform.FindChild("TimerText").GetComponent<Animator>().SetTrigger("Flash");
        }
    }

    private void HandleShowResult(ShowResult result) {
        switch (result) {
            case ShowResult.Finished:
                Debug.Log("Video completed. Rewarded $" + adValue());
                em.money += adValue();
                wm.adWatchTime = Util.adCooldown;
                break;
            case ShowResult.Skipped:
                Debug.LogWarning("Video was skipped.");
                break;
            case ShowResult.Failed:
                Debug.LogError("Video failed to show.");
                break;
        }
    }

    public double adValue() {
        return (em.totalMoney * Util.adRewardTotalPercentage) + em.getSandwichValue(em.sauceID) * Util.adRewardSwipes * em.swipeRate + em.sandwichValue * em.rate * Util.adRewardTime + Util.money * Util.adRewardCurrentPercentage;
    }

    /// <summary>
    /// SAUCE UPGRADE
    /// </summary>
    public void upgradeSauce() {
        if (em.money >= sauceCost()) {
            em.spend(sauceCost());
            em.sauceID++;
            wm.sauce.GetComponent<Sauce>().update();
            wm.em.recalculate();
            em.list.transform.FindChild("Value").transform.FindChild("SandwichValueText").GetComponent<Text>().text = "Sandwich Value: $" + Util.encodeNumber(em.sandwichValue) + " each";
        }
        else {
            notEnough();
        }
    }
    public double sauceCost() {
        return Util.sauceBaseCost * Mathf.Pow((float)Util.sauceScale, em.sauceID - 1);
    }


    /// <summary>
    /// SANDWICH CART
    /// </summary>
    public void buySandwichCart() {
        if (em.money >= sandwichCartCost()) {
            em.spend(sandwichCartCost());
            em.sandwichCartCount++;
            em.recalculate();
            em.updateProducerMenuCounters();
        }
        else {
            notEnough();
        }
    }
    double sandwichCartCost() {
        return em.list.transform.FindChild("SandwichCart").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.sandwichCartCount);
    }

    /// <summary>
    /// DELI
    /// </summary>
    public void buyDeli() {
        if (em.money >= deliCost()) {
            em.spend(deliCost());
            em.deliCount++;
            em.recalculate();
            em.updateProducerMenuCounters();
        }
        else {
            notEnough();
        }
    }
    double deliCost() {
        return em.list.transform.FindChild("Deli").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.deliCount);
    }

    /// <summary>
    /// AUTOCHEF 9k
    /// </summary>
    public void buyAutochef() {
        if (em.money >= autochefCost()) {
            em.spend(autochefCost());
            em.autochefCount++;
            em.recalculate();
            em.updateProducerMenuCounters();
        }
        else {
            notEnough();
        }
    }
    double autochefCost() {
        return em.list.transform.FindChild("Autochef9k").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.autochefCount);
    }


    /// <summary>
    /// McDandwich
    /// </summary>
    public void buyMcdandwich() {
        if (em.money >= mcdandwichCost()) {
            em.spend(mcdandwichCost());
            em.mcdandwichCount++;
            em.recalculate();
            em.updateProducerMenuCounters();
        }
        else {
            notEnough();
        }
    }
    double mcdandwichCost() {
        return em.list.transform.FindChild("McDandwich").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.mcdandwichCount);
    }

    /// <summary>
    /// SandwichCity
    /// </summary>
    public void buySandwichCity() {
        if (em.money >= sandwichCityCost()) {
            em.spend(sandwichCityCost());
            em.sandwichCityCount++;
            em.recalculate();
            em.updateProducerMenuCounters();
        }
        else {
            notEnough();
        }
    }
    double sandwichCityCost() {
        return em.list.transform.FindChild("SandwichCity").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.sandwichCityCount);
    }

}
