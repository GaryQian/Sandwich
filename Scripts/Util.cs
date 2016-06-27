using UnityEngine;
using System.Collections;

public class Util {
    public static double money;
    public static float pScale = 1.5f;
    public static float sandwichCartRate = 0.5f;
    public static float deliRate = 10f;
    public static string encodeNumber(double money) {
        int numSize = 3;
        while (money / Mathf.Pow(10f, numSize) > 1f) {
            numSize += 3;
        }
        string suffix = "";
        switch (numSize) {
            case 3: suffix = ""; break;
            case 6: suffix = "k"; money = money / Mathf.Pow(10f, numSize - 3); break;
            case 9: suffix = "m"; money = money / Mathf.Pow(10f, numSize - 3); break;
            case 12: suffix = "b"; money = money / Mathf.Pow(10f, numSize - 3); break;
            case 15: suffix = "t"; money = money / Mathf.Pow(10f, numSize - 3); break;
            case 18: suffix = "qd"; money = money / Mathf.Pow(10f, numSize - 3); break;
            case 21: suffix = "qt"; money = money / Mathf.Pow(10f, numSize - 3); break;
            case 24: suffix = "sx"; money = money / Mathf.Pow(10f, numSize - 3); break;
            case 27: suffix = "sp"; money = money / Mathf.Pow(10f, numSize - 3); break;
            case 30: suffix = "oc"; money = money / Mathf.Pow(10f, numSize - 3); break;
            case 33: suffix = "nn"; money = money / Mathf.Pow(10f, numSize - 3); break;
            case 36: suffix = "dc"; money = money / Mathf.Pow(10f, numSize - 3); break;
            case 39: suffix = "ud"; money = money / Mathf.Pow(10f, numSize - 3); break;
            case 42: suffix = "dd"; money = money / Mathf.Pow(10f, numSize - 3); break;
            case 45: suffix = "td"; money = money / Mathf.Pow(10f, numSize - 3); break;
            case 48: suffix = "qtd"; money = money / Mathf.Pow(10f, numSize - 3); break;
        }
        if (money < 100f) {
            return string.Format("{0:N2}", money) + suffix;
        }
        else {
            return string.Format("{0:N1}", money) + suffix;
        }
    }

    public static string encodeNumberInteger(int num) {
        return string.Format("{0:0}", num);
    }
}
