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
    public AudioClip kaching;

    private Upgrade up;

    public GameObject canvasNotificationTextPrefab;

    void Awake() {
        em = GetComponent<EconomyManager>();
        wm = GetComponent<WorldManager>();
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
        double num = em.money;
        em.money *= 2f;
        em.totalMoney += num;
    }

    public void playKaching() {
        Util.wm.audio.PlayOneShot(kaching);
    }

    /// <summary>
    /// TIME MACHINE STUFF
    /// </summary>
    public void buyFlux() {
        if (em.money >= Util.timeMachineCost) {
            em.spend(Util.timeMachineCost);
            wm.sm.buyFlux();
        }
        else {
            notEnough();
        }
    }
    public void buyBreadclear() {
        if (em.money >= Util.timeMachineCost) {
            em.spend(Util.timeMachineCost);
            wm.sm.buyBreadclear();
        }
        else {
            notEnough();
        }
    }
    public void buySandtanium() {
        if (em.money >= Util.timeMachineCost) {
            em.spend(Util.timeMachineCost);
            wm.sm.buySandtanium();
        }
        else {
            notEnough();
        }
    }

    public void toggleMute() {
        wm.muted = !wm.muted;
        Util.muted = wm.muted;
        if (wm.muted) {
            muteButton.GetComponent<Image>().sprite = muteOn;
            wm.audio.Pause();

        }
        else {
            muteButton.GetComponent<Image>().sprite = muteOff;
            wm.audio.Play();
        }
    }

    public void sandWitchClick() {
        double num = Util.sandWitchCurrentPercentage * em.money + Util.sandWitchTotalPercentage * em.totalMoney;
        em.money += num;
        em.totalMoney += num;
        GameObject obj = Instantiate(canvasNotificationTextPrefab);
        obj.GetComponent<CanvasNotificationText>().setup("+$" + Util.encodeNumber(num), wm.sandWitch.GetComponent<RectTransform>().anchoredPosition, new Color(0, 1f, 0), 60, 100);
        Destroy(wm.sandWitch);
    }

    /// <summary>
    /// WATCH AD Money BUTTON
    /// </summary>
    public void watchAdMoney() {
        if (wm.adWatchTimeMoney <= 0) {
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResultMoney;

            Advertisement.Show(wm.zoneID, options);
        }
        else {
            em.list.transform.FindChild("AdForMoney").transform.FindChild("TimerText").GetComponent<Animator>().SetTrigger("Flash");
        }
    }

    private void HandleShowResultMoney(ShowResult result) {
        switch (result) {
            case ShowResult.Finished:
                Debug.Log("Video completed. Rewarded $" + adValue());
                double num = adValue();
                em.money += num;
                //em.totalMoney += num;
                wm.adWatchTimeMoney = Util.adMoneyCooldown;
                em.save();
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
        return (em.totalMoney * Util.adRewardTotalPercentage) + em.getSandwichValue() * Util.adRewardSwipes * em.swipeRate + em.getSandwichValue() * em.rate * Util.adRewardTime + Util.money * Util.adRewardCurrentPercentage;
    }

    /// <summary>
    /// WATCH AD Elixir BUTTON
    /// </summary>
    public void watchAdElixir() {
        if (wm.adWatchTimeElixir <= 0) {
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResultElixir;

            Advertisement.Show(wm.zoneID, options);
        }
        else {
            em.list.transform.FindChild("AdForElixir").transform.FindChild("TimerText").GetComponent<Animator>().SetTrigger("Flash");
        }
    }

    private void HandleShowResultElixir(ShowResult result) {
        switch (result) {
            case ShowResult.Finished:
                Debug.Log("Video completed. Rewarded $" + adValue());
                em.elixir += 1;
                wm.adWatchTimeElixir = Util.adElixirCooldown;
                em.save();
                break;
            case ShowResult.Skipped:
                Debug.LogWarning("Video was skipped.");
                break;
            case ShowResult.Failed:
                Debug.LogError("Video failed to show.");
                break;
        }
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
            playKaching();
            em.list.transform.FindChild("Value").transform.FindChild("SandwichValueText").GetComponent<Text>().text = "$" + Util.encodeNumber(em.sandwichValue) + " each &";
        }
        else {
            notEnough();
        }
    }
    public double sauceCost() {
        return Util.sauceBaseCost * Mathf.Pow((float)Util.sauceScale, em.sauceID - 1);
    }

    /// <summary>
    /// BREAD UPGRADE
    /// </summary>
    public void upgradeBread() {
        if (em.money >= breadCost()) {
            em.spend(breadCost());
            em.breadID++;
            Bread.updateButton();
            wm.em.recalculate();
            playKaching();
            em.list.transform.FindChild("Value").transform.FindChild("SandwichValueText").GetComponent<Text>().text = "$" + Util.encodeNumber(em.sandwichValue) + " each &";
        }
        else {
            notEnough();
        }
    }
    public double breadCost() {
        return Bread.cost(em.breadID);
    }

    /// <summary>
    /// SHARPEN KNIVES
    /// </summary>
    public void sharpenKnives() {
        if (em.money >= sharpenKnivesCost()) {
            em.spend(sharpenKnivesCost());
            em.knifeVamp += Util.knifeVampRate;
            updateSharpenKnives();
            playKaching();
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
            em.buildings++;
            wm.tutorialManager.removeYellowArrow();
            playKaching();
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
            em.buildings++;
            playKaching();
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
            em.buildings++;
            playKaching();
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
            em.buildings++;
            playKaching();
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
            em.buildings++;
            playKaching();
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
            em.buildings++;
            playKaching();
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
            em.buildings++;
            playKaching();
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
            em.buildings++;
            playKaching();
        }
        else {
            notEnough();
        }
    }
    double sandriaLawCost() {
        return em.list.transform.FindChild("SandriaLaw").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.sandriaLawCount);
    }


    /// <summary>
    /// Sandwich Planet
    /// </summary>
    public void buySandwichPlanet() {
        if (em.money >= sandwichPlanetCost()) {
            em.spend(sandwichPlanetCost());
            em.sandwichPlanetCount++;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings++;
            playKaching();
        }
        else {
            notEnough();
        }
    }
    double sandwichPlanetCost() {
        return em.list.transform.FindChild("SandwichPlanet").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.sandwichPlanetCount);
    }

    /// <summary>
    /// Human Extermination
    /// </summary>
    public void buyHumanExtermination() {
        if (em.money >= humanExterminationCost()) {
            em.spend(humanExterminationCost());
            em.humanExterminationCount++;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings++;
            playKaching();
        }
        else {
            notEnough();
        }
    }
    double humanExterminationCost() {
        return em.list.transform.FindChild("HumanExtermination").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.humanExterminationCount);
    }

    /// <summary>
    /// Enslave Aliens
    /// </summary>
    public void buyEnslaveAliens() {
        if (em.money >= enslaveAliensCost()) {
            em.spend(enslaveAliensCost());
            em.enslaveAliensCount++;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings++;
            playKaching();
        }
        else {
            notEnough();
        }
    }
    double enslaveAliensCost() {
        return em.list.transform.FindChild("EnslaveAliens").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.enslaveAliensCount);
    }

    /// <summary>
    /// Deathsandwich
    /// </summary>
    public void buyDeathSandwich() {
        if (em.money >= deathSandwichCost()) {
            em.spend(deathSandwichCost());
            em.deathSandwichCount++;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings++;
            playKaching();
        }
        else {
            notEnough();
        }
    }
    double deathSandwichCost() {
        return em.list.transform.FindChild("DeathSandwich").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.deathSandwichCount);
    }

    /// <summary>
    /// Sandwich Galaxy
    /// </summary>
    public void buySandwichGalaxy() {
        if (em.money >= sandwichGalaxyCost()) {
            em.spend(sandwichGalaxyCost());
            em.sandwichGalaxyCount++;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings++;
            playKaching();
        }
        else {
            notEnough();
        }
    }
    double sandwichGalaxyCost() {
        return em.list.transform.FindChild("SandwichGalaxy").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.sandwichGalaxyCount);
    }

    /// <summary>
    /// Flying Sandwich Monster
    /// </summary>
    public void buyFlyingSandwichMonster() {
        if (em.money >= flyingSandwichMonsterCost()) {
            em.spend(flyingSandwichMonsterCost());
            em.flyingSandwichMonsterCount++;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings++;
            playKaching();
        }
        else {
            notEnough();
        }
    }
    double flyingSandwichMonsterCost() {
        return em.list.transform.FindChild("FlyingSandwichMonster").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.flyingSandwichMonsterCount);
    }



}
