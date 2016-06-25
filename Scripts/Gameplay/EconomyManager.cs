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
    private WorldManager wm;

    public GameObject NotificationTextPrefab;

    void Awake() {
        wm = GetComponent<WorldManager>();
    }

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
            combo -= comboDecayRate * (combo + 1f) * Time.deltaTime;
        }
    }

    void processIncome() {
        money += rate * sandwichValue * updateRate * multiplier;
        totalTime += updateRate;
        gameTime += updateRate;


        displayMoney();
    }

    public void swipe() {
        combo += 1f;
        checkCombo();
        money += sandwichValue * swipeRate * multiplier;
        
        GameObject text = (GameObject)Instantiate(NotificationTextPrefab);
        text.GetComponent<NotificationText>().setup("+" + encodeNumber(sandwichValue * swipeRate * multiplier), wm.activeBread.transform.position + new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-0.2f, 0.2f)));
        Debug.LogError("SWIPE!");
    }

    public void displayMoney() {
        moneyText.updateMoney(money);
    }

    public void recalculate() {
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
            Invoke("checkCombo", 0.2f);
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

    string encodeNumber(double money) {
        int numSize = 4;
        while (money / Mathf.Pow(10f, numSize) > 1f) {
            numSize += 3;
        }
        string suffix = "";
        switch (numSize) {
            case 4: suffix = ""; break;
            case 7: suffix = "k"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 10: suffix = "m"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 13: suffix = "b"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 16: suffix = "t"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 19: suffix = "qd"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 22: suffix = "qt"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 25: suffix = "sx"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 28: suffix = "sp"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 31: suffix = "oc"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 34: suffix = "nn"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 37: suffix = "dc"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 40: suffix = "ud"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 43: suffix = "dd"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 46: suffix = "td"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 49: suffix = "qtd"; money = money / Mathf.Pow(10f, numSize - 4); break;
        }
        if (money < 100f) {
            return "$" + string.Format("{0:0.0}", money) + suffix;
        }
        else {
            return "$" + string.Format("{0:0.0}", money) + suffix;
        }
    }
}

[Serializable]
public class SaveData {
    public double money = 0;
    public int spreadCount = 1;
    public double totalTime = 0f;
    public double gameTime = 0f;
}