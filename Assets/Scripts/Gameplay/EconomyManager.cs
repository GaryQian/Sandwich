﻿using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public enum ProducerType { sandwichCart, deli, autochef, mcdandwich, sandwichCity, breadCloning, sandwocracy, sandriaLaw, sandwichPlanet, humanExtermination, sandwichFleet, enslaveAliens, deathSandwich, sandwichGalaxy, flyingSandwichMonster }

[Serializable]
public class EconomyManager : MonoBehaviour {
    public bool isLoaded = false;

    public double money = 0;
    public double totalMoney = 0; //total money made so far this game.
    public double lifetimeMoney = 0; //money made ever
    public long elixir = 0;
    public long totalElixir = 0; //elixir made ever
    public double rate = 1f; //number of sandwiches per second
    public double sandwichValue = 1f; //val of each sandwich
    public double swipeRate = 1f;
    public float knifeVamp = 0; //amount of the total cps gained on each swipe
    public float reproductionRate = 0;
    public double nurseryPop = 0;
    public double maxBabyPop = 0;
    public int sauceID; //which is the current spread
    public int breadID = 0;
    public double totalTime = 0f;
    public double gameTime = 0f;
    public double sessionTime = 0f;
    public double sps; //sandwiches per second
    public double sandwichesMade;
    public double lifetimeSandwichesMade;
    public int totalSwipes = 0;
    public int lifetimeSwipes = 0;

    public float updateRate;
    private bool even;
    public float saveRate;

    public float combo = 0;
    public float comboDecayRate;
    public float x2threshold;
    public float x3threshold;
    public float x5threshold;
    bool x2shown = false;
    bool x3shown = false;
    bool x5shown = false;
    public int multiplier = 1;
    public MultiplierGlow multiplierGlow;

    public MoneyText moneyText;
    private RateText rateText;
    private SandwichValueText sandwichValueText;
    private WorldManager wm;
    public GameObject NotificationTextPrefab;

    public int buildings = 0;
    public int lifetimeBuildings = 0;
    public int sandwichCartCount = 0;
    public int deliCount = 0;
    public int autochefCount = 0;
    public int mcdandwichCount = 0;
    public int sandwichCityCount = 0;
    public int breadCloningCount = 0;
    public int sandwocracyCount = 0;
    public int sandriaLawCount = 0;
    public int sandwichPlanetCount = 0;
    public int humanExterminationCount = 0;
    public int sandwichFleetCount = 0;
    public int enslaveAliensCount = 0;
    public int deathSandwichCount = 0;
    public int sandwichGalaxyCount = 0;
    public int flyingSandwichMonsterCount = 0;

    public int toasterVisionLevel = 0;
    public double toasterVisionBonus = 1f;
    public int communalMindLevel = 0;
    public double communalMindBonus = 1f;
    public int dexterousHandsLevel = 1;

    public GameObject canvasNotificationTextPrefab;

    public GameObject list; //the list of upgrades

    
    /// <summary>
    /// Events
    /// </summary>
    public delegate void EconomyChanged();
    public static event EconomyChanged MoneyChanged;


    void Awake() {
        Util.em = this;
        wm = GetComponent<WorldManager>();
        moneyText = GameObject.Find("MoneyText").GetComponent<MoneyText>();
        rateText = GameObject.Find("RateText").GetComponent<RateText>();
        sandwichValueText = GameObject.Find("SandwichValueText").GetComponent<SandwichValueText>();
        list = GameObject.Find("Producer");
        multiplierGlow = GameObject.Find("MultiplierGlow").GetComponent<MultiplierGlow>();
    }

    void Start() {
        load();
        recalculate();
        updateProducerMenuCounters();
        wm.sauce.GetComponent<Sauce>().update();
        InvokeRepeating("processIncome", 0.1f, updateRate);
        InvokeRepeating("save", saveRate, saveRate);
        even = true;
        x2shown = false;
        x3shown = false;
        x5shown = false;
        wm.checkAdTimer();

        wm.initializeBGMusic();
        moneyText.updateColor();

        Util.x10BuyCostScale = calculateCostScale();
        maxBabyPop = Util.em.rate * Util.em.reproductionRate * Util.maxBabyTime / 100f;
        wm.setupGPGS();
        wm.processOffline();
        wm.spawnAlert();

        if (totalTime > 90f) Destroy(wm.overlay);

        wm.buttonHandler.toggleBatterySaver();
        wm.buttonHandler.toggleBatterySaver();
    }

    // Update is called once per frame
    void Update() {
        if (combo > 0) {
            combo -= comboDecayRate * (combo + 1f) * Time.deltaTime;
            if (combo < x2threshold - 1f) {
                x2shown = false;
                x3shown = false;
                x5shown = false;
            }
        }
    }

    void processIncome() {
        income(rate * sandwichValue * updateRate);
        sandwichesMade += rate * updateRate;
        totalTime += updateRate;
        gameTime += updateRate;
        sessionTime += updateRate;
        even = !even;
        displayMoney();
    }

    public bool spend(double num) {
        if (num <= money) {
            money -= num;
            displayMoney();
            GameObject obj = Instantiate(canvasNotificationTextPrefab);
            obj.GetComponent<CanvasNotificationText>().setup("-$" + Util.encodeNumber(num), new Vector2(0, 800f), new Color(1f, 0, 0), 60, -100);
            return true;
        }
        return false;
    }

    public void income(double num) {
        num = num * toasterVisionBonus * wm.x2Multiplier * wm.x3Multiplier * wm.x7Multiplier;
        money += num;
        totalMoney += num;
    }

    float calculateCostScale() {
        return 1f + Mathf.Pow(Util.pScale, 1f)
            + Mathf.Pow(Util.pScale, 2f)
            + Mathf.Pow(Util.pScale, 3f)
            + Mathf.Pow(Util.pScale, 4f)
            + Mathf.Pow(Util.pScale, 5f)
            + Mathf.Pow(Util.pScale, 6f)
            + Mathf.Pow(Util.pScale, 7f)
            + Mathf.Pow(Util.pScale, 8f)
            + Mathf.Pow(Util.pScale, 9f);
    }

    public void swipe() {
        combo += 1f;
        checkCombo();
        double num = sandwichValue * swipeRate + sps * knifeVamp;
        income(num);
        sandwichesMade += swipeRate;
        totalSwipes++;
        GameObject obj = Instantiate(canvasNotificationTextPrefab);
        obj.GetComponent<CanvasNotificationText>().setup("+$" + Util.encodeNumber(num * toasterVisionBonus * wm.x2Multiplier * wm.x3Multiplier * wm.x7Multiplier), new Vector2(250f + UnityEngine.Random.Range(-75f, 75f), -650f), new Color(1f, 1f, 1f), 80, 400f);

        wm.gtm.knife.GetComponent<Knife>().hasSauce = false;
        wm.activeBread.GetComponent<Bread>().finish();
    }

    public void displayMoney() {
        moneyText.updateMoney(money);
    }

    public void updateLabels() {
        rateText.updateRate(sps * toasterVisionBonus * wm.x2Multiplier * wm.x3Multiplier * wm.x7Multiplier);
        sandwichValueText.updateValue(sandwichValue * toasterVisionBonus * wm.x2Multiplier * wm.x3Multiplier * wm.x7Multiplier);
    }

    public void updateProducerMenuCounters() {
        if (wm.menuState == MenuType.producer) {
            list.transform.FindChild("Rate").transform.FindChild("ProductionRateText").GetComponent<Text>().text = Util.encodeNumber(rate) + " &/s";
            list.transform.FindChild("SandwichCart").GetComponent<Upgrade>().setupProducerUpgrade(sandwichCartCount, Util.sandwichCartRate * communalMindBonus, Util.sandwichCartBase * Util.buyNumberScale);
            list.transform.FindChild("Deli").GetComponent<Upgrade>().setupProducerUpgrade(deliCount, Util.deliRate * communalMindBonus, Util.deliBase * Util.buyNumberScale);
            list.transform.FindChild("Autochef9k").GetComponent<Upgrade>().setupProducerUpgrade(autochefCount, Util.autochefRate * communalMindBonus, Util.autochefBase * Util.buyNumberScale);
            list.transform.FindChild("McDandwich").GetComponent<Upgrade>().setupProducerUpgrade(mcdandwichCount, Util.mcdandwichRate * communalMindBonus, Util.mcdandwichBase * Util.buyNumberScale);
            list.transform.FindChild("SandwichCity").GetComponent<Upgrade>().setupProducerUpgrade(sandwichCityCount, Util.sandwichCityRate * communalMindBonus, Util.sandwichCityBase * Util.buyNumberScale);
            list.transform.FindChild("BreadCloning").GetComponent<Upgrade>().setupProducerUpgrade(breadCloningCount, Util.breadCloningRate * communalMindBonus, Util.breadCloningBase * Util.buyNumberScale);
            list.transform.FindChild("Sandwocracy").GetComponent<Upgrade>().setupProducerUpgrade(sandwocracyCount, Util.sandwocracyRate * communalMindBonus, Util.sandwocracyBase * Util.buyNumberScale);
            list.transform.FindChild("SandriaLaw").GetComponent<Upgrade>().setupProducerUpgrade(sandriaLawCount, Util.sandriaLawRate * communalMindBonus, Util.sandriaLawBase * Util.buyNumberScale);
            list.transform.FindChild("SandwichPlanet").GetComponent<Upgrade>().setupProducerUpgrade(sandwichPlanetCount, Util.sandwichPlanetRate * communalMindBonus, Util.sandwichPlanetBase * Util.buyNumberScale);
            list.transform.FindChild("HumanExtermination").GetComponent<Upgrade>().setupProducerUpgrade(humanExterminationCount, Util.humanExterminationRate * communalMindBonus, Util.humanExterminationBase * Util.buyNumberScale);
            list.transform.FindChild("SandwichFleet").GetComponent<Upgrade>().setupProducerUpgrade(sandwichFleetCount, Util.sandwichFleetRate * communalMindBonus, Util.sandwichFleetBase * Util.buyNumberScale);
            list.transform.FindChild("EnslaveAliens").GetComponent<Upgrade>().setupProducerUpgrade(enslaveAliensCount, Util.enslaveAliensRate * communalMindBonus, Util.enslaveAliensBase * Util.buyNumberScale);
            list.transform.FindChild("DeathSandwich").GetComponent<Upgrade>().setupProducerUpgrade(deathSandwichCount, Util.deathSandwichRate * communalMindBonus, Util.deathSandwichBase * Util.buyNumberScale);
            list.transform.FindChild("SandwichGalaxy").GetComponent<Upgrade>().setupProducerUpgrade(sandwichGalaxyCount, Util.sandwichGalaxyRate * communalMindBonus, Util.sandwichGalaxyBase * Util.buyNumberScale);
            list.transform.FindChild("FlyingSandwichMonster").GetComponent<Upgrade>().setupProducerUpgrade(flyingSandwichMonsterCount, Util.flyingSandwichMonsterRate * communalMindBonus, Util.flyingSandwichMonsterBase * Util.buyNumberScale);


        }
    }

    public void updateElixirUpgrades() {
        if (wm.menuState == MenuType.permanent) {
            list.transform.FindChild("ToasterVision").GetComponent<Upgrade>().setupElixirUpgrade(wm.buttonHandler.toasterVisionCost(), "+" + Util.encodeNumberInteger(10 * toasterVisionLevel) + "%", "+" + Util.encodeNumberInteger(10 * toasterVisionLevel + 10) + "% All\nIncome");
            list.transform.FindChild("CommunalMind").GetComponent<Upgrade>().setupElixirUpgrade(wm.buttonHandler.communalMindCost(), "+" + Util.encodeNumberInteger(communalMindLevel) + "%", "+" + Util.encodeNumberInteger(1 * communalMindLevel + 1) + "% Production\nPer 50 Buildings");
            list.transform.FindChild("DexterousHands").GetComponent<Upgrade>().setupElixirUpgrade(wm.buttonHandler.dexterousHandsCost(), Util.encodeNumberInteger((int)getDexterousHandsBonus(dexterousHandsLevel)) + "&", Util.encodeNumberInteger((int)getDexterousHandsBonus(dexterousHandsLevel + 1)) + "& Per\nSwipe");

        }
    }

    public void recalculate() {
        communalMindBonus = 1f + (buildings / 50f) * (0.01f * communalMindLevel);
        rate = getRate();
        sandwichValue = getSandwichValue(sauceID, breadID);

        toasterVisionBonus = 1f + 0.1f * toasterVisionLevel;
        swipeRate = getDexterousHandsBonus(dexterousHandsLevel);
        sandwichValue *= multiplier;
        sps = rate * sandwichValue;
        updateLabels();
        displayMoney();
    }

    double getDexterousHandsBonus(int lvl) {
        return (lvl * (lvl + 1)) / 2f;
    }

    public double getSandwichValue(int i, int j) {
        //return Mathf.Pow(Mathf.Pow(16f + 16f * Mathf.Pow(j * 1.5f, 1f/3f), 1f/4f), i - 1);
        return Mathf.Pow(2f, i - 1 + j * 1.1f);
    }

    public double getSandwichValueOld(int i, int j) {
        return Mathf.Pow(2f, i - 1 + j);
    }

    /*public double getSandwichBaseValue(int i) {
        return Mathf.Pow(2f, i - 1);
    }*/

    public double getSandwichValue() {
        return getSandwichValue(sauceID, breadID);
    }

    public double getRate() {
        double num = 0;
        num += sandwichCartCount * Util.sandwichCartRate;
        num += deliCount * Util.deliRate;
        num += autochefCount * Util.autochefRate;
        num += mcdandwichCount * Util.mcdandwichRate;
        num += sandwichCityCount * Util.sandwichCityRate;
        num += breadCloningCount * Util.breadCloningRate;
        num += sandwocracyCount * Util.sandwocracyRate;
        num += sandriaLawCount * Util.sandriaLawRate;
        num += sandwichPlanetCount * Util.sandwichPlanetRate;
        num += humanExterminationCount * Util.humanExterminationRate;
        num += sandwichFleetCount * Util.sandwichFleetRate;
        num += enslaveAliensCount * Util.enslaveAliensRate;
        num += deathSandwichCount * Util.deathSandwichRate;
        num += sandwichGalaxyCount * Util.sandwichGalaxyRate;
        num += flyingSandwichMonsterCount * Util.flyingSandwichMonsterRate;
        return num * communalMindBonus;
    }

    

    void checkCombo() {
        
        int initMult = multiplier;
        if (combo > x2threshold) {
            multiplier = 2;
            multiplierGlow.show();
            if (combo > x3threshold) {
                multiplier = 3;
                if (combo > x5threshold) {
                    //multiplier = 5;
                }
            }
            Invoke("checkCombo", 0.5f);
            //spawn combo notification
            if (initMult < multiplier) {

                if (multiplier == 2 && !x2shown) {
                    showMultiplier();
                    x2shown = true;
                }
                else if (multiplier == 3 && !x3shown) {
                    showMultiplier();
                    x3shown = true;
                }
                else if (multiplier == 5 && !x5shown) {
                    showMultiplier();
                    x5shown = true;
                }
            }
            //
        }
        else {
            multiplier = 1;
        }
        recalculate();
    }

    void showMultiplier() {
        GameObject obj = Instantiate(canvasNotificationTextPrefab);
        obj.GetComponent<CanvasNotificationText>().setup("x" + (int)multiplier, new Vector2(250f + UnityEngine.Random.Range(-75f, 75f), -650f), new Color(1f, 1f, 0), 150, 500f);
    }


    void load() {
        isLoaded = true;
        wm.loadVersion();
        if (File.Exists(Application.persistentDataPath + "/gamedata.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamedata.dat", FileMode.Open);
            SaveData data = null;
            try {
                data = (SaveData)bf.Deserialize(file);
            }
            catch (Exception e) {
                Debug.LogError("Failed to load gamedata: " + e.Message);
            }
            file.Close();

            money = data.money;
            elixir = data.elixir;
            totalElixir = data.totalElixir;
            totalMoney = data.totalMoney;
            lifetimeMoney = data.lifetimeMoney;
            rate = data.rate;
            knifeVamp = data.knifeVamp;
            reproductionRate = data.reproductionRate;
            nurseryPop = data.nurseryPop;
            totalTime = data.totalTime;
            gameTime = data.gameTime;
            sauceID = data.sauceID;
            breadID = data.breadID;
            sandwichesMade = data.sandwichesMade;
            lifetimeSandwichesMade = data.lifetimeSandwichesMade;
            

            totalSwipes = data.totalSwipes;
            lifetimeSwipes = data.lifetimeSwipes;

            buildings = data.buildings;
            lifetimeBuildings = data.lifetimeBuildings;
            sandwichCartCount = data.sandwichCartCount;
            deliCount = data.deliCount;
            autochefCount = data.autochefCount;
            mcdandwichCount = data.mcdandwichCount;
            sandwichCityCount = data.sandwichCityCount;
            breadCloningCount = data.breadCloningCount;
            sandwocracyCount = data.sandwocracyCount;
            sandriaLawCount = data.sandriaLawCount;
            sandwichPlanetCount = data.sandwichPlanetCount;
            humanExterminationCount = data.humanExterminationCount;
            sandwichFleetCount = data.sandwichFleetCount;
            enslaveAliensCount = data.enslaveAliensCount;
            deathSandwichCount = data.deathSandwichCount;
            sandwichGalaxyCount = data.sandwichGalaxyCount;
            flyingSandwichMonsterCount = data.flyingSandwichMonsterCount;

            toasterVisionLevel = data.toasterVisionLevel;
            communalMindLevel = data.communalMindLevel;
            dexterousHandsLevel = data.dexterousHandsLevel;


            wm.adWatchTimeMoney = data.adWatchTimeMoney;
            wm.adWatchTimeElixir = data.adWatchTimeElixir;
            wm.adWatchTimex2 = data.adWatchTimex2;

            wm.sm.storyProgress = data.storyProgress;
            wm.sm.timeMachineDone = data.timeMachineDone;
            wm.sm.hasFlux = data.hasFlux;
            wm.sm.hasBreadclear = data.hasBreadclear;
            wm.sm.hasSandtanium = data.hasSandtanium;

            wm.sandWitchesClicked = data.sandWitchesClicked;
            wm.playthroughCount = data.playthroughCount;

            wm.loadTime();
        }
    }

    public void save() {
        if (!isLoaded) load();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamedata.dat");

        SaveData data = new SaveData();

        data.money = money;
        data.elixir = elixir;
        data.totalElixir = totalElixir;
        data.totalMoney = totalMoney;
        data.lifetimeMoney = lifetimeMoney;
        data.rate = rate;
        data.knifeVamp = knifeVamp;
        data.reproductionRate = reproductionRate;
        data.nurseryPop = nurseryPop;
        data.gameTime = gameTime;
        data.totalTime = totalTime;
        data.sauceID = sauceID;
        data.breadID = breadID;
        data.sandwichesMade = sandwichesMade;
        data.lifetimeSandwichesMade = lifetimeSandwichesMade;

        data.totalSwipes = totalSwipes;
        data.lifetimeSwipes = lifetimeSwipes;

        data.buildings = buildings;
        data.lifetimeBuildings = lifetimeBuildings;
        data.sandwichCartCount = sandwichCartCount;
        data.deliCount = deliCount;
        data.autochefCount = autochefCount;
        data.mcdandwichCount = mcdandwichCount;
        data.sandwichCityCount = sandwichCityCount;
        data.breadCloningCount = breadCloningCount;
        data.sandwocracyCount = sandwocracyCount;
        data.sandriaLawCount = sandriaLawCount;
        data.sandwichPlanetCount = sandwichPlanetCount;
        data.humanExterminationCount = humanExterminationCount;
        data.sandwichFleetCount = sandwichFleetCount;
        data.enslaveAliensCount = enslaveAliensCount;
        data.deathSandwichCount = deathSandwichCount;
        data.sandwichGalaxyCount = sandwichGalaxyCount;
        data.flyingSandwichMonsterCount = flyingSandwichMonsterCount;

        data.toasterVisionLevel = toasterVisionLevel;
        data.communalMindLevel = communalMindLevel;
        data.dexterousHandsLevel = dexterousHandsLevel;

        data.adWatchTimeMoney = wm.adWatchTimeMoney;
        data.adWatchTimeElixir = wm.adWatchTimeElixir;
        data.adWatchTimex2 = wm.adWatchTimex2;
        

        data.storyProgress = wm.sm.storyProgress;
        data.timeMachineDone = wm.sm.timeMachineDone;
        data.hasFlux = wm.sm.hasFlux;
        data.hasBreadclear = wm.sm.hasBreadclear;
        data.hasSandtanium = wm.sm.hasSandtanium;

        data.sandWitchesClicked = wm.sandWitchesClicked;
        data.playthroughCount = wm.playthroughCount;

        bf.Serialize(file, data);
        file.Close();

        wm.saveSettings();

        wm.saveIAP();
    }
}

[Serializable]
public class SaveData {
    public double money;
    public long elixir;
    public long totalElixir;
    public double totalMoney;
    public double lifetimeMoney;
    public double rate;
    public double swipeRate;
    public float knifeVamp;
    public float reproductionRate;
    public double nurseryPop;
    public int sauceID;
    public int breadID;
    public double totalTime;
    public double gameTime;
    public double sandwichesMade;
    public double lifetimeSandwichesMade;

    public int totalSwipes;
    public int lifetimeSwipes;

    public int buildings;
    public int lifetimeBuildings;
    public int sandwichCartCount;
    public int deliCount;
    public int autochefCount;
    public int mcdandwichCount;
    public int sandwichCityCount;
    public int breadCloningCount;
    public int sandwocracyCount;
    public int sandriaLawCount;
    public int sandwichPlanetCount;
    public int humanExterminationCount;
    public int sandwichFleetCount;
    public int enslaveAliensCount;
    public int deathSandwichCount;
    public int sandwichGalaxyCount;
    public int flyingSandwichMonsterCount;

    public int toasterVisionLevel;
    public int communalMindLevel;
    public int dexterousHandsLevel;


    public double adWatchTimeMoney;
    public double adWatchTimeElixir;
    public double adWatchTimex2;

    public int storyProgress;
    public bool timeMachineDone;
    public bool hasFlux;
    public bool hasBreadclear;
    public bool hasSandtanium;

    public int sandWitchesClicked;
    public int playthroughCount;
}