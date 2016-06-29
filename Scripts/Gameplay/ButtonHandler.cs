using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class ButtonHandler : MonoBehaviour {

    public EconomyManager em;
    private WorldManager wm;
    public GameObject red;

    public GameObject muteButton;
    public Sprite muteOn;
    public Sprite muteOff;

    private Upgrade up;

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

    public void cheatx2() {
        em.money *= 2f;
    }

    public void toggleMute() {
        wm.muted = !wm.muted;
        Util.muted = wm.muted;
        if (wm.muted) {
            muteButton.GetComponent<Image>().sprite = muteOn;
        }
        else {
            muteButton.GetComponent<Image>().sprite = muteOff;
        }
    }

    /// <summary>
    /// WATCH AD BUTTON
    /// </summary>
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
        return (em.totalMoney * Util.adRewardTotalPercentage) + em.getSandwichValue(em.sauceID) * Util.adRewardSwipes * em.swipeRate + em.getSandwichValue(em.sauceID) * em.rate * Util.adRewardTime + Util.money * Util.adRewardCurrentPercentage;
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
    /// SHARPEN KNIVES
    /// </summary>
    public void sharpenKnives() {
        if (em.money >= sharpenKnivesCost()) {
            em.spend(sharpenKnivesCost());
            em.knifeVamp += Util.knifeVampRate;
            updateSharpenKnives();
        }
        else {
            notEnough();
        }
    }
    public double sharpenKnivesCost() {
        return Util.knifeVampBaseCost * Mathf.Pow((float)Util.knifeVampScale, (int)((em.knifeVamp / Util.knifeVampRate)));
    }
    public void updateSharpenKnives() {
        up = em.list.transform.FindChild("SharpenKnives").GetComponent<Upgrade>();
        up.updateCounter("" + (int)(wm.em.knifeVamp * 100f + 0.001f) + "%");
        up.updateStats("Swipes make\n" + (int)((wm.em.knifeVamp + Util.knifeVampRate + 0.001f) * 100f) + "% &/s");
        up.updateCost(sharpenKnivesCost());

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

    /// <summary>
    /// BreadCloning
    /// </summary>
    public void buyBreadCloning() {
        if (em.money >= breadCloningCost()) {
            em.spend(breadCloningCost());
            em.breadCloningCount++;
            em.recalculate();
            em.updateProducerMenuCounters();
        }
        else {
            notEnough();
        }
    }
    double breadCloningCost() {
        return em.list.transform.FindChild("BreadCloning").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.breadCloningCount);
    }

    /// <summary>
    /// Sandwocracy
    /// </summary>
    public void buySandwocracy() {
        if (em.money >= sandwocracyCost()) {
            em.spend(sandwocracyCost());
            em.sandwocracyCount++;
            em.recalculate();
            em.updateProducerMenuCounters();
        }
        else {
            notEnough();
        }
    }
    double sandwocracyCost() {
        return em.list.transform.FindChild("Sandwocracy").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.sandwocracyCount);
    }

    /// <summary>
    /// Sandria law
    /// </summary>
    public void buySandriaLaw() {
        if (em.money >= sandriaLawCost()) {
            em.spend(sandriaLawCost());
            em.sandriaLawCount++;
            em.recalculate();
            em.updateProducerMenuCounters();
        }
        else {
            notEnough();
        }
    }
    double sandriaLawCost() {
        return em.list.transform.FindChild("SandriaLaw").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.sandriaLawCount);
    }



}
