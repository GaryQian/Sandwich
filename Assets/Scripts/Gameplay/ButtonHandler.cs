﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
#if UNITY_ANDROID
using GooglePlayGames;
#elif UNITY_IOS
using UnityEngine.SocialPlatforms.GameCenter;
#endif
using UnityEngine.SocialPlatforms;

public enum AdWatchType { money, elixir, x2boost, other }

public class ButtonHandler : MonoBehaviour {

    public EconomyManager em;
    private WorldManager wm;
    public GameObject red;

    public GameObject muteButton;
    public GameObject musicMuteButton;
    public Sprite muteOn;
    public Sprite muteOff;
    public Sprite musicMuteOn;
    public Sprite musicMuteOff;
    public AudioClip kaching;
    public AudioClip build;
    public AudioClip witch;
    public AudioClip evolveSound;
    public AudioClip click;
    public AudioClip bagDrop;

    public GameObject knifePanel;

    private Upgrade up;

    public GameObject canvasNotificationTextPrefab;


    public static int buyCount = 1;

    //EVENTS
    public delegate void BuyItem();

    public static event BuyItem BuySandwichCart;
    public static event BuyItem BuyDeli;
    public static event BuyItem BuyAutochef;
    public static event BuyItem BuyMcdandwich;
    public static event BuyItem BuySandwichCity;
    public static event BuyItem BuyBreadCloning;
    public static event BuyItem BuySandwocracy;
    public static event BuyItem BuySandriaLaw;
    public static event BuyItem BuySandwichPlanet;
    public static event BuyItem BuyHumanExtermination;
    public static event BuyItem BuySandwichFleet;
    public static event BuyItem BuyEnslaveAliens;
    public static event BuyItem BuyDeathSandwich;
    public static event BuyItem BuySandwichGalaxy;
    public static event BuyItem BuyFlyingSandwichMonster;

    public static event BuyItem BuySauce;
    public static event BuyItem BuyBread;
    public static event BuyItem BuyEvolution;
    public static event BuyItem BuySandwichReproduction;


    public static AdWatchType adWatchType = AdWatchType.other;

    //

    void Awake() {
        em = GetComponent<EconomyManager>();
        wm = GetComponent<WorldManager>();
    }

    void notEnough() {
        red.SetActive(true);
        red.GetComponent<Animator>().SetTrigger("Flash");
        Invoke("disableRed", 0.467f);
        if (!Util.wm.muted) Util.wm.fullAudioSource.PlayOneShot(click);
    }

    void disableRed() {
        red.SetActive(false);
    }

    public void cheatx2() {
        if (Util.godmode) {
            double num = em.money;
            em.money *= 2f;
            em.totalMoney += num;
            
        }
        //wm.playthroughCount++;
        //em.moneyText.updateColor();
    }

    public void playKaching() {
        if (!Util.wm.muted) {
            wm.halfAudioSource.PlayOneShot(kaching);
            //wm.fullAudioSource.PlayOneShot(build);
        }
        CancelInvoke("saveTask");
        Invoke("saveTask", 1f);
    }

    private void saveTask() {
        em.save();
    }

    /// KnifePanel View
    ///
    public void viewKnifePanel() {
        Instantiate(knifePanel);
        if (!Util.wm.muted) Util.wm.fullAudioSource.PlayOneShot(bagDrop);
    }



    /// <summary>
    /// ELIXIR Toaster Vision
    /// </summary>
    public void buyToasterVision() {
        if (em.elixir >= toasterVisionCost()) {
            em.elixir -= toasterVisionCost();
            em.toasterVisionLevel++;
            em.recalculate();
            em.updateElixirUpgrades();
            if (!Util.wm.muted) wm.fullAudioSource.PlayOneShot(evolveSound);
            if (BuyEvolution != null) BuyEvolution();
        }
        else {

        }
    }
    public int toasterVisionCost() {
        return (int)Mathf.Floor(Util.toasterVisionBase * Mathf.Pow(Util.toasterVisionScale, em.toasterVisionLevel) + em.toasterVisionLevel);
    }

    /// <summary>
    /// ELIXIR Communal Minds
    /// </summary>
    public void buyCommunalMind() {
        if (em.elixir >= communalMindCost()) {
            em.elixir -= communalMindCost();
            em.communalMindLevel++;
            em.recalculate();
            em.updateElixirUpgrades();
            if (!Util.wm.muted) wm.fullAudioSource.PlayOneShot(evolveSound);
            if (BuyEvolution != null) BuyEvolution();
        }
        else {

        }
    }
    public int communalMindCost() {
        return (int)Mathf.Floor(Util.communalMindBase * Mathf.Pow(Util.communalMindScale, em.communalMindLevel) + em.communalMindLevel);
    }

    /// <summary>
    /// ELIXIR Dexterous Hands
    /// </summary>
    public void buyDexterousHands() {
        if (em.elixir >= dexterousHandsCost()) {
            em.elixir -= dexterousHandsCost();
            em.dexterousHandsLevel++;
            em.recalculate();
            em.updateElixirUpgrades();
            if (!Util.wm.muted) wm.fullAudioSource.PlayOneShot(evolveSound);
            if (BuyEvolution != null) BuyEvolution();
        }
        else {

        }
    }
    public int dexterousHandsCost() {
        return (int)Mathf.Floor(Util.dexterousHandsBase * Mathf.Pow(Util.dexterousHandsScale, em.dexterousHandsLevel) + em.dexterousHandsLevel);
    }

    /// <summary>
    /// TIME MACHINE STUFF
    /// </summary>
    public void buyFlux() {
        if (em.money >= Util.timeMachineCost) {
            em.spend(Util.timeMachineCost);
            wm.sm.buyFlux();
            playKaching();
        }
        else {
            notEnough();
        }
    }
    public void buyBreadclear() {
        if (em.money >= Util.timeMachineCost) {
            em.spend(Util.timeMachineCost);
            wm.sm.buyBreadclear();
            playKaching();
        }
        else {
            notEnough();
        }
    }
    public void buySandtanium() {
        if (em.money >= Util.timeMachineCost) {
            em.spend(Util.timeMachineCost);
            wm.sm.buySandtanium();
            playKaching();
        }
        else {
            notEnough();
        }
    }

    /// <summary>
    /// MUTE
    /// </summary>
    public void toggleMute() {
        wm.muted = !wm.muted;
        if (wm.muted) {
            muteButton.GetComponent<Image>().sprite = muteOn;
        }
        else {
            muteButton.GetComponent<Image>().sprite = muteOff;
        }
    }

    /// <summary>
    /// Music MUTE
    /// </summary>
    public void toggleMusicMute() {
        wm.musicMuted = !wm.musicMuted;
        if (wm.musicMuted) {
            musicMuteButton.GetComponent<Image>().sprite = musicMuteOn;
            wm.music.Pause();

        }
        else {
            musicMuteButton.GetComponent<Image>().sprite = musicMuteOff;
            wm.music.Play();
        }
    }

    ///
    /// LEADERBOARD UI
    /// 

    public void showLeaderboard() {
        if (Social.localUser.authenticated) {
            wm.postScore();
            Social.ShowLeaderboardUI();
            
        }
        else {
            wm.authenticate();
            wm.postScore();
            Social.ShowLeaderboardUI();
            
        }
    }
    ///
    /// Achievements UI
    public void showAchievements() {
        if (Social.localUser.authenticated) {
            Social.ShowAchievementsUI();
        }
        else {
            wm.authenticate();
            Social.ShowAchievementsUI();
        }
    }

    ///
    /// Facebook UI
    /// 
    public void showFB() {
        Application.OpenURL("http://facebook.com/sandwichorelse");
    }

    ///
    ///BatterySaver Mode
    ///

    public void toggleBatterySaver() {
        if (!wm.batterySaver) {
            Application.targetFrameRate = 30;
            wm.batterySaverButton.GetComponent<Image>().color = new Color(0, 1f, 0);
        }
        else {
            Application.targetFrameRate = 50;
            wm.batterySaverButton.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        }
        wm.batterySaver = !wm.batterySaver;
    }

    /// <summary>
    /// Sand Witch
    /// </summary>
    public void sandWitchClick() {
        double num = Util.sandWitchCurrentPercentage * em.money + Util.sandWitchTotalPercentage * em.totalMoney;
        em.money += num;
        wm.sandWitchesClicked++;
        GameObject obj = Instantiate(canvasNotificationTextPrefab);
        obj.GetComponent<CanvasNotificationText>().setup("+$" + Util.encodeNumber(num), wm.sandWitch.GetComponent<RectTransform>().anchoredPosition, new Color(0, 1f, 0), 70, 100);
        Destroy(wm.sandWitch);
        if (!Util.wm.muted) wm.fullAudioSource.PlayOneShot(witch);
    }

    /// <summary>
    /// WATCH AD Money BUTTON
    /// </summary>
    public void watchAdMoney() {
        if (wm.adWatchTimeMoney <= 0) {
            adWatchType = AdWatchType.money;
            if (false && (Util.even && wm.adm.rewardBasedVideo.IsLoaded()) || (!Util.even && !Advertisement.IsReady() && wm.adm.rewardBasedVideo.IsLoaded())) {
                Debug.Log("Showing AdMob rewarded video");
                wm.adm.rewardBasedVideo.Show();
            }
            else if (Advertisement.IsReady()) {
                ShowOptions options = new ShowOptions();
                options.resultCallback = HandleShowResultMoney;

                Advertisement.Show(wm.zoneID, options);
            }
        }
        else {
            em.list.transform.FindChild("AdForMoney").transform.FindChild("TimerText").GetComponent<Animator>().SetTrigger("Flash");
        }
    }

    public void HandleShowResultMoney(ShowResult result) {
        switch (result) {
            case ShowResult.Finished:
                //if (Util.adMoneyCooldown <= 0) {
                    Debug.Log("Video completed. Rewarded $" + adValue());
                    double num = adValue();
                    em.money += num;
                    wm.adWatchTimeMoney = Util.adMoneyCooldown;
                    em.save();
                //}
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
        return (em.totalMoney * Util.adRewardTotalPercentage) + (em.getSandwichValue() * Util.adRewardSwipes * em.swipeRate) + (em.getSandwichValue() * em.rate * Util.adRewardTime) + (Util.em.money * Util.adRewardCurrentPercentage);
    }

    /// <summary>
    /// WATCH AD Elixir BUTTON
    /// </summary>
    public void watchAdElixir() {
        if (wm.adWatchTimeElixir <= 0) {
            adWatchType = AdWatchType.elixir;
            if (false && (Util.even && wm.adm.rewardBasedVideo.IsLoaded()) || (!Util.even && !Advertisement.IsReady() && wm.adm.rewardBasedVideo.IsLoaded())) {
                Debug.Log("Showing AdMob rewarded video");
                wm.adm.rewardBasedVideo.Show();
            }
            else if (Advertisement.IsReady()) {
                ShowOptions options = new ShowOptions();
                options.resultCallback = HandleShowResultElixir;

                Advertisement.Show(wm.zoneID, options);
            }
        }
        else {
            em.list.transform.FindChild("AdForElixir").transform.FindChild("TimerText").GetComponent<Animator>().SetTrigger("Flash");
        }
    }

    public void HandleShowResultElixir(ShowResult result) {
        switch (result) {
            case ShowResult.Finished:
                Debug.Log("Video completed. Rewarded 1 elixir");
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
    /// WATCH AD x2
    /// </summary>
    public void watchAdx2() {
        if (wm.adWatchTimex2 <= 0 && wm.x3Time <= 0 && wm.x7Time <= 0) {
            adWatchType = AdWatchType.x2boost;
            if (false && (Util.even && wm.adm.rewardBasedVideo.IsLoaded()) || (!Util.even && !Advertisement.IsReady() && wm.adm.rewardBasedVideo.IsLoaded())) {
                Debug.Log("Showing AdMob rewarded video");
                wm.adm.rewardBasedVideo.Show();
                
            }
            else if (Advertisement.IsReady()) {
                ShowOptions options = new ShowOptions();
                options.resultCallback = HandleShowResultx2;

                Advertisement.Show(wm.zoneID, options);
            }
        }
        else {
            em.list.transform.FindChild("AdForx2").transform.FindChild("TimerText").GetComponent<Animator>().SetTrigger("Flash");
        }
    }

    public void HandleShowResultx2(ShowResult result) {
        switch (result) {
            case ShowResult.Finished:
                //if (Util.adMoneyCooldown <= 0) {
                Debug.Log("Video completed. Rewarded $" + adValue());
                wm.adWatchTimex2 = Util.adx2Cooldown;
                wm.x2Time = Util.adx2Time;
                //}
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
            em.list.transform.FindChild("Value").transform.FindChild("SandwichValueText").GetComponent<Text>().text = "$" + Util.encodeNumber(em.getSandwichValue()) + " each &";
            Bread.updateButton();
            if (BuySauce != null) BuySauce();
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
            em.list.transform.FindChild("Value").transform.FindChild("SandwichValueText").GetComponent<Text>().text = "$" + Util.encodeNumber(em.getSandwichValue()) + " each &";
            if (BuyBread != null) BuyBread();
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
    /// SANDWICH REPRODUCTION
    /// </summary>
    public void upgradeSandwichReproduction() {
        if (em.money >= swReproductionCost()) {
            em.spend(swReproductionCost());
            em.reproductionRate++;

            updateSandwichReproduction();
            playKaching();
            if (BuySandwichReproduction != null) BuySandwichReproduction();
        }
        else {
            notEnough();
        }
    }
    public double swReproductionCost() {
        return Util.sandwichReproductionBase * System.Math.Pow(Util.sandwichReproductionScale, em.reproductionRate);
    }
    public void updateSandwichReproduction() {
        up = em.list.transform.FindChild("SandwichReproduction").GetComponent<Upgrade>();
        up.updateCounter("" + (int)(em.reproductionRate + 0.0001f) + "%");
        up.updateStats("Make " + (int)(1.0001f + em.reproductionRate) + "%\nProduction even\nWith App Closed");
        up.updateCost(swReproductionCost());

    }


    /// <summary>
    /// SANDWICH CART
    /// </summary>
    public void buySandwichCart() {
        if (em.money >= sandwichCartCost()) {
            em.spend(sandwichCartCost());
            em.sandwichCartCount += buyCount;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings += buyCount;
            //wm.tutorialManager.removeYellowArrow();
            playKaching();

            if (BuySandwichCart != null) BuySandwichCart();
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
            em.deliCount += buyCount;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings += buyCount;
            playKaching();
            if (BuyDeli != null) BuyDeli();
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
            em.autochefCount += buyCount;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings += buyCount;
            playKaching();
            if (BuyAutochef != null) BuyAutochef();
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
            em.mcdandwichCount += buyCount;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings += buyCount;
            playKaching();
            if (BuyMcdandwich != null) BuyMcdandwich();
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
            em.sandwichCityCount += buyCount;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings += buyCount;
            playKaching();
            if (BuySandwichCity != null) BuySandwichCity();
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
            em.breadCloningCount += buyCount;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings += buyCount;
            playKaching();
            if (BuyBreadCloning != null) BuyBreadCloning();
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
            em.sandwocracyCount += buyCount;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings += buyCount;
            playKaching();
            if (BuySandwocracy != null) BuySandwocracy();
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
            em.sandriaLawCount += buyCount;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings += buyCount;
            playKaching();
            if (BuySandriaLaw != null) BuySandriaLaw();
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
            em.sandwichPlanetCount += buyCount;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings += buyCount;
            playKaching();
            if (BuySandwichPlanet != null) BuySandwichPlanet();
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
            em.humanExterminationCount += buyCount;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings += buyCount;
            playKaching();
            if (BuyHumanExtermination != null) BuyHumanExtermination();
        }
        else {
            notEnough();
        }
    }
    double humanExterminationCost() {
        return em.list.transform.FindChild("HumanExtermination").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.humanExterminationCount);
    }


    /// <summary>
    /// Sandwich Fleet
    /// </summary>
    public void buySandwichFleet() {
        if (em.money >= sandwichFleetCost()) {
            em.spend(sandwichFleetCost());
            em.sandwichFleetCount += buyCount;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings += buyCount;
            playKaching();
            if (BuySandwichFleet != null) BuySandwichFleet();
        }
        else {
            notEnough();
        }
    }
    double sandwichFleetCost() {
        return em.list.transform.FindChild("SandwichFleet").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.sandwichFleetCount);
    }

    /// <summary>
    /// Enslave Aliens
    /// </summary>
    public void buyEnslaveAliens() {
        if (em.money >= enslaveAliensCost()) {
            em.spend(enslaveAliensCost());
            em.enslaveAliensCount += buyCount;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings += buyCount;
            playKaching();
            if (BuyEnslaveAliens != null) BuyEnslaveAliens();
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
            em.deathSandwichCount += buyCount;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings += buyCount;
            playKaching();
            if (BuyDeathSandwich != null) BuyDeathSandwich();
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
            em.sandwichGalaxyCount += buyCount;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings += buyCount;
            playKaching();
            if (BuySandwichGalaxy != null) BuySandwichGalaxy();
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
            em.flyingSandwichMonsterCount += buyCount;
            em.recalculate();
            em.updateProducerMenuCounters();
            em.buildings += buyCount;
            playKaching();
            if (BuyFlyingSandwichMonster != null) BuyFlyingSandwichMonster();
        }
        else {
            notEnough();
        }
    }
    double flyingSandwichMonsterCost() {
        return em.list.transform.FindChild("FlyingSandwichMonster").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.flyingSandwichMonsterCount);
    }



}
