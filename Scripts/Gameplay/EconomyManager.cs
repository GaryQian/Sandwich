﻿using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public enum ProducerType { sandwichCart, deli, autochef, mcdandwich, sandwichCity, breadCloning, sandwocracy, sandriaLaw, sandwichPlanet, humanExtermination, enslaveAliens, deathSandwich, sandwichGalaxy, flyingSandwichMonster }

[Serializable]
public class EconomyManager : MonoBehaviour {
    public double money = 0;
    public double totalMoney = 0; //total money made so far this game.
    public double lifetimeMoney = 0; //money made ever
    public long elixir = 0;
    public long totalElixir = 0; //elixir made ever
    public double rate = 1f; //number of sandwiches per second
    public double sandwichValue = 1f; //val of each sandwich
    public double swipeRate = 1f; //numhber of sandwiches made per swipe
    public float knifeVamp = 0; //amount of the total cps gained on each swipe
    public int knifeCount = 1;
    public int sauceID; //which is the current spread
    public int breadID = 0;
    public double totalTime = 0f;
    public double gameTime = 0f;
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
    bool x2shown;
    bool x3shown;
    bool x5shown;
    public int multiplier = 1;
    public int prevMultiplier = 1;
    public MultiplierGlow multiplierGlow;

    private MoneyText moneyText;
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
    public int enslaveAliensCount = 0;
    public int deathSandwichCount = 0;
    public int sandwichGalaxyCount = 0;
    public int flyingSandwichMonsterCount = 0;

    public int toasterVisionLevel = 0;
    public int communalMindLevel = 0;
    public int dexterousHandsLevel = 1;

    public GameObject canvasNotificationTextPrefab;

    public GameObject list; //the list of upgrades


    
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
        money += rate * sandwichValue * updateRate;
        totalMoney += rate * sandwichValue * updateRate;
        sandwichesMade += rate * updateRate;
        totalTime += updateRate;
        gameTime += updateRate;
        even = !even;
        displayMoney();
        Util.money = money;
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
        money += num;
        totalMoney += num;
    }

    public void swipe() {
        combo += 1f;
        checkCombo();
        double num = sandwichValue * swipeRate + sps * knifeVamp;
        money += num;
        totalMoney += num;
        sandwichesMade += swipeRate;
        totalSwipes++;
        GameObject text = (GameObject)Instantiate(NotificationTextPrefab);
        text.GetComponent<NotificationText>().setup("+$" + Util.encodeNumber(num), wm.activeBread.GetComponent<Bread>().finalPos + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)));

        wm.gtm.knife.GetComponent<Knife>().hasSauce = false;
        wm.activeBread.GetComponent<Bread>().finish();
    }

    public void displayMoney() {
        moneyText.updateMoney(money);
    }

    public void updateLabels() {
        rateText.updateRate(sps);
        sandwichValueText.updateValue(sandwichValue);
    }

    public void updateProducerMenuCounters() {
        if (wm.menuState == MenuType.producer) {
            list.transform.FindChild("Rate").transform.FindChild("ProductionRateText").GetComponent<Text>().text = Util.encodeNumber(rate) + " &/s";
            list.transform.FindChild("SandwichCart").GetComponent<Upgrade>().setupProducerUpgrade(sandwichCartCount, Util.sandwichCartRate, Util.sandwichCartBase);
            list.transform.FindChild("Deli").GetComponent<Upgrade>().setupProducerUpgrade(deliCount, Util.deliRate, Util.deliBase);
            list.transform.FindChild("Autochef9k").GetComponent<Upgrade>().setupProducerUpgrade(autochefCount, Util.autochefRate, Util.autochefBase);
            list.transform.FindChild("McDandwich").GetComponent<Upgrade>().setupProducerUpgrade(mcdandwichCount, Util.mcdandwichRate, Util.mcdandwichBase);
            list.transform.FindChild("SandwichCity").GetComponent<Upgrade>().setupProducerUpgrade(sandwichCityCount, Util.sandwichCityRate, Util.sandwichCityBase);
            list.transform.FindChild("BreadCloning").GetComponent<Upgrade>().setupProducerUpgrade(breadCloningCount, Util.breadCloningRate, Util.breadCloningBase);
            list.transform.FindChild("Sandwocracy").GetComponent<Upgrade>().setupProducerUpgrade(sandwocracyCount, Util.sandwocracyRate, Util.sandwocracyBase);
            list.transform.FindChild("SandriaLaw").GetComponent<Upgrade>().setupProducerUpgrade(sandriaLawCount, Util.sandriaLawRate, Util.sandriaLawBase);
            list.transform.FindChild("SandwichPlanet").GetComponent<Upgrade>().setupProducerUpgrade(sandwichPlanetCount, Util.sandwichPlanetRate, Util.sandwichPlanetBase);
            list.transform.FindChild("HumanExtermination").GetComponent<Upgrade>().setupProducerUpgrade(humanExterminationCount, Util.humanExterminationRate, Util.humanExterminationBase);
            list.transform.FindChild("EnslaveAliens").GetComponent<Upgrade>().setupProducerUpgrade(enslaveAliensCount, Util.enslaveAliensRate, Util.enslaveAliensBase);
            list.transform.FindChild("DeathSandwich").GetComponent<Upgrade>().setupProducerUpgrade(deathSandwichCount, Util.deathSandwichRate, Util.deathSandwichBase);
            list.transform.FindChild("SandwichGalaxy").GetComponent<Upgrade>().setupProducerUpgrade(sandwichGalaxyCount, Util.sandwichGalaxyRate, Util.sandwichGalaxyBase);
            list.transform.FindChild("FlyingSandwichMonster").GetComponent<Upgrade>().setupProducerUpgrade(flyingSandwichMonsterCount, Util.flyingSandwichMonsterRate, Util.flyingSandwichMonsterBase);


        }
    }

    public void recalculate() {
        rate = getRate();
        sandwichValue = getSandwichValue(sauceID, breadID);

        

        sandwichValue *= multiplier;
        sps = rate * sandwichValue;
        updateLabels();
        displayMoney();
    }

    public double getSandwichValue(int i, int j) {
        return Mathf.Pow(2f, i - 1 + j);
    }

    public double getSandwichBaseValue(int i) {
        return Mathf.Pow(2f, i - 1);
    }

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
        num += enslaveAliensCount * Util.enslaveAliensRate;
        num += deathSandwichCount * Util.deathSandwichRate;
        num += sandwichGalaxyCount * Util.sandwichGalaxyRate;
        num += flyingSandwichMonsterCount * Util.flyingSandwichMonsterRate;
        return num;
    }

    

    void checkCombo() {
        int initMult = multiplier;
        prevMultiplier = multiplier;
        if (combo > x2threshold) {
            multiplier = 2;
            multiplierGlow.show();
            if (combo > x3threshold) {
                multiplier = 3;
                if (combo > x5threshold) {
                    multiplier = 5;
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
        GameObject text = (GameObject)Instantiate(NotificationTextPrefab);
        text.GetComponent<NotificationText>().setup("x" + (int)multiplier, wm.activeBread.GetComponent<Bread>().finalPos + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.1f, 0.1f)), new Color(1f, 1f * (1f - ((multiplier - 2f) / 2f)), 0), (int)(Screen.height * 0.08f), 0.7f);
    }


    void load() {
        if (File.Exists(Application.persistentDataPath + "/gamedata.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamedata.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            money = data.money;
            elixir = data.elixir;
            totalMoney = data.totalMoney;
            lifetimeMoney = data.lifetimeMoney;
            rate = data.rate;
            swipeRate = data.swipeRate;
            knifeVamp = data.knifeVamp;
            knifeCount = data.knifeCount;
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
            enslaveAliensCount = data.enslaveAliensCount;
            deathSandwichCount = data.deathSandwichCount;
            sandwichGalaxyCount = data.sandwichGalaxyCount;
            flyingSandwichMonsterCount = data.flyingSandwichMonsterCount;


            wm.adWatchTimeMoney = data.adWatchTimeMoney;
            wm.adWatchTimeElixir = data.adWatchTimeElixir;
            wm.muted = data.muted;
            Util.muted = wm.muted;

            wm.sm.storyProgress = data.storyProgress;
            wm.sm.timeMachineDone = data.timeMachineDone;
            wm.sm.hasFlux = data.hasFlux;
            wm.sm.hasBreadclear = data.hasBreadclear;
            wm.sm.hasSandtanium = data.hasSandtanium;

            wm.sandWitchesClicked = data.sandWitchesClicked;
            wm.playthroughCount = data.playthroughCount;
        }
    }

    public void save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamedata.dat");

        SaveData data = new SaveData();

        data.money = money;
        data.elixir = elixir;
        data.totalMoney = totalMoney;
        data.lifetimeMoney = lifetimeMoney;
        data.rate = rate;
        data.swipeRate = swipeRate;
        data.knifeVamp = knifeVamp;
        data.knifeCount = knifeCount;
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
        data.enslaveAliensCount = enslaveAliensCount;
        data.deathSandwichCount = deathSandwichCount;
        data.sandwichGalaxyCount = sandwichGalaxyCount;
        data.flyingSandwichMonsterCount = flyingSandwichMonsterCount;

        data.adWatchTimeMoney = wm.adWatchTimeMoney;
        data.adWatchTimeElixir = wm.adWatchTimeElixir;
        data.muted = wm.muted;

        data.storyProgress = wm.sm.storyProgress;
        data.timeMachineDone = wm.sm.timeMachineDone;
        data.hasFlux = wm.sm.hasFlux;
        data.hasBreadclear = wm.sm.hasBreadclear;
        data.hasSandtanium = wm.sm.hasSandtanium;

        data.sandWitchesClicked = wm.sandWitchesClicked;
        data.playthroughCount = wm.playthroughCount;

        bf.Serialize(file, data);
        file.Close();
    }
}

[Serializable]
public class SaveData {
    public double money;
    public long elixir;
    public double totalMoney;
    public double lifetimeMoney;
    public double rate;
    public double swipeRate;
    public float knifeVamp;
    public int knifeCount;
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
    public int enslaveAliensCount;
    public int deathSandwichCount;
    public int sandwichGalaxyCount;
    public int flyingSandwichMonsterCount;


    public double adWatchTimeMoney;
    public double adWatchTimeElixir;
    public bool muted;

    public int storyProgress;
    public bool timeMachineDone;
    public bool hasFlux;
    public bool hasBreadclear;
    public bool hasSandtanium;

    public int sandWitchesClicked;
    public int playthroughCount;
}