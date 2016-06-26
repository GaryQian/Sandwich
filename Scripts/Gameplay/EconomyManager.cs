﻿using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class EconomyManager : MonoBehaviour {
    public double money = 0;
    public double rate = 1f; //number of sandwiches per second
    public double sandwichValue = 1f; //val of each sandwich
    public double swipeRate = 1f; //numhber of sandwiches made per swipe
    public int spreadCount = 1; //which is the current spread
    public double totalTime = 0f;
    public double gameTime = 0f;
    public double sps; //sandwiches per second

    public float updateRate;
    private bool even;
    public float saveRate;

    public float combo = 0;
    public float comboDecayRate;
    public float x2threshold;
    public float x5threshold;
    public float x10threshold;
    bool x2shown;
    bool x5shown;
    bool x10shown;
    public int multiplier = 1;
    public int prevMultiplier = 1;

    private MoneyText moneyText;
    private RateText rateText;
    private SandwichValueText sandwichValueText;
    private WorldManager wm;

    public GameObject NotificationTextPrefab;


    private int totalSwipes = 0;

    void Awake() {
        wm = GetComponent<WorldManager>();
        moneyText = GameObject.Find("MoneyText").GetComponent<MoneyText>();
        rateText = GameObject.Find("RateText").GetComponent<RateText>();
        sandwichValueText = GameObject.Find("SandwichValueText").GetComponent<SandwichValueText>();
    }

    void Start() {
        load();
        recalculate();
        InvokeRepeating("processIncome", 0.1f, updateRate);
        InvokeRepeating("save", saveRate, saveRate);
        even = true;
        x2shown = false;
        x5shown = false;
        x10shown = false;

    }

    // Update is called once per frame
    void Update() {
        if (combo > 0) {
            combo -= comboDecayRate * (combo + 1f) * Time.deltaTime;
            if (combo < x2threshold - 1f) {
                x2shown = false;
                x5shown = false;
                x10shown = false;
            }
        }
    }

    void processIncome() {
        money += rate * sandwichValue * updateRate;
        totalTime += updateRate;
        gameTime += updateRate;
        even = !even;
        displayMoney();
    }

    public void swipe() {
        combo += 1f;
        checkCombo();
        money += sandwichValue * swipeRate;
        totalSwipes++;
        GameObject text = (GameObject)Instantiate(NotificationTextPrefab);
        text.GetComponent<NotificationText>().setup("+$" + wm.encodeNumber(sandwichValue * swipeRate), wm.activeBread.GetComponent<Bread>().finalPos + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)));

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

    public void recalculate() {
        rate = 1f;
        sandwichValue = 1f;

        sandwichValue *= multiplier;
        sps = rate * sandwichValue;
        updateLabels();
        displayMoney();
    }

    

    void checkCombo() {
        int initMult = multiplier;
        prevMultiplier = multiplier;
        if (combo > x2threshold) {
            multiplier = 2;
            if (combo > x5threshold) {
                multiplier = 5;
                if (combo > x10threshold) {
                    multiplier = 10;
                }
            }
            Invoke("checkCombo", 0.3f);
            //spawn combo notification
            if (initMult < multiplier) {

                if (multiplier == 2 && !x2shown) {
                    showMultiplier();
                    x2shown = true;
                }
                else if (multiplier == 5 && !x5shown) {
                    showMultiplier();
                    x5shown = true;
                }
                else if (multiplier == 10 && !x10shown) {
                    showMultiplier();
                    x10shown = true;
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
        text.GetComponent<NotificationText>().setup("x" + (int)multiplier, wm.activeBread.GetComponent<Bread>().finalPos + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.1f, 0.1f)), new Color(1f, 1f, 0), (int)(Screen.height * 0.06f), 0.2f);
    }

    void multiplierDecay() {
        switch (multiplier) {
            case 2: multiplier = 1; break;
            case 5: multiplier = 2; break;
            case 10: multiplier = 5; break;
            case 1: multiplier = 1; break;
        }
        recalculate();
    }

    void load() {
        if (File.Exists(Application.persistentDataPath + "/gamedata.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamedata.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            money = data.money;
            rate = data.rate;
            swipeRate = data.swipeRate;
            totalTime = data.totalTime;
            gameTime = data.gameTime;
            spreadCount = data.spreadCount;
            

            totalSwipes = data.totalSwipes;

        }
    }

    void save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamedata.dat");

        SaveData data = new SaveData();

        data.money = money;
        data.rate = rate;
        data.swipeRate = swipeRate;
        data.gameTime = gameTime;
        data.totalTime = totalTime;
        data.spreadCount = spreadCount;

        data.totalSwipes = totalSwipes;

        bf.Serialize(file, data);
        file.Close();
    }
}

[Serializable]
public class SaveData {
    public double money = 0;
    public double rate = 0;
    public double swipeRate = 1f;
    public int spreadCount = 1;
    public double totalTime = 0f;
    public double gameTime = 0f;

    public int totalSwipes = 0;
}