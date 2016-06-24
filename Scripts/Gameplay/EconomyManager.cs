using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class EconomyManager : MonoBehaviour {
    public double money = 0;
    public double rate = 0;
    public double sandwichValue = 1f;
    public double swipeRate = 1f;
    public int spreadCount = 1;
    public double totalTime = 0f;
    public double gameTime = 0f;
    public double sps = 0;

    public float updateRate;
    public float saveRate;

    public float combo = 0;
    public float comboDecayRate;
    public float x2threshold;
    public float x5threshold;
    public float x10threshold;
    public float multiplier = 1f;

    private MoneyText moneyText;
    // Use this for initialization
    void Start() {
        //FileStream file = File.Open(Application.persistentDataPath + "/gamedata.datxc", FileMode.Open);
        load();
        InvokeRepeating("processIncome", 0.1f, updateRate);
        InvokeRepeating("save", saveRate, saveRate);

        moneyText = GameObject.Find("MoneyText").GetComponent<MoneyText>();
    }

    // Update is called once per frame
    void Update() {
        if (combo > 0) {
            combo -= comboDecayRate * combo * Time.deltaTime;
        }
    }

    void processIncome() {
        money += rate * sandwichValue * updateRate * multiplier;
        totalTime += updateRate;
        gameTime += updateRate;


        displayMoney();
    }

    void swipe() {
        combo += 1f;
        checkCombo();
        money += sandwichValue * swipeRate * multiplier;
    }

    void displayMoney() {
        moneyText.updateMoney(money);
    }

    void recalculate() {
        rate = 0f;
        sandwichValue = 1f;

        //calculate rate and value data.
        displayMoney();
    }

    

    void checkCombo() {
        if (combo > x2threshold) {
            multiplier = 2f;
            if (combo > x5threshold) {
                multiplier = 5f;
                if (combo > x10threshold) {
                    multiplier = 10f;
                }
            }
            Invoke("checkCombo", 0.15f);
        }
        else {
            multiplier = 1f;
        }
    }

    void load() {
        if (File.Exists(Application.persistentDataPath + "/gamedata.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamedata.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            money = data.money;
            totalTime = data.totalTime;
            gameTime = data.gameTime;
            spreadCount = data.spreadCount;

        }
    }

    void save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamedata.dat");

        SaveData data = new SaveData();

        data.money = money;
        data.gameTime = gameTime;
        data.totalTime = totalTime;
        data.spreadCount = spreadCount;

        bf.Serialize(file, data);
        file.Close();
    }
}

[Serializable]
public class SaveData {
    public double money = 0;
    public int spreadCount = 1;
    public double totalTime = 0f;
    public double gameTime = 0f;
}