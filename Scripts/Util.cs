using UnityEngine;
using System.Collections;
using System;

public class Util {
    public static WorldManager wm;
    public static EconomyManager em;

    public static double money;
    public static double sauceBaseCost = 500f;
    public static double sauceScale = 9f;
    public static int numberOfSauces = 13;
    public static double knifeVampBaseCost = 2000000f;
    public static double knifeVampScale = 200f;
    public static float knifeVampRate = 0.04f;


    public static float pScale = 1.2f;
    public static float sandwichCartRate = 0.5f;
    public static float deliRate = 7f;
    public static float autochefRate = 70f;
    public static float mcdandwichRate = 800f;
    public static float sandwichCityRate = 9000f;
    public static float breadCloningRate = 100000f;
    public static float sandwocracyRate = 1000000f;
    public static float sandriaLawRate = 10000000f;
    public static double sandwichPlanetRate = 100000000f;
    public static double humanExterminationRate = 1000000000f;
    public static double enslaveAliensRate = 10000000000f;
    public static double deathSandwichRate = 100000000000f;
    public static double sandwichGalaxyRate = 1000000000000f;
    public static double flyingSandwichMonsterRate = 10000000000000f;

    public static float sandwichCartBase = 50f;
    public static float deliBase = 6000f;
    public static float autochefBase = 400000f;
    public static float mcdandwichBase = 20000000f;
    public static float sandwichCityBase = 1000000000f;
    public static double breadCloningBase = 2E+11f;
    public static double sandwocracyBase = 1E+13f;
    public static double sandriaLawBase = 1E+15f;
    public static double sandwichPlanetBase = 1E+17f;
    public static double humanExterminationBase = 1E+19;
    public static double enslaveAliensBase = 1E+21;
    public static double deathSandwichBase = 1E+23;
    public static double sandwichGalaxyBase = 1E+25;
    public static double flyingSandwichMonsterBase = 1E+27;




    public static float adRewardCurrentPercentage = 0.10f;
    public static float adRewardSwipes = 500f;
    public static float adRewardTime = 300f;
    public static float adRewardTotalPercentage = 0.03f;
    public static double adCooldown = 1200f;


    public static float screenToCanvasRatio;

    public static bool muted;

    public static string encodeNumber(double m) {
        double money = m;
        int numSize = 3;
        while (money / System.Math.Pow(10f, numSize) >= 1f) {
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
            case 39: suffix = "ud"; money = money / System.Math.Pow(10f, numSize - 3); break;
            case 42: suffix = "dd"; money = money / System.Math.Pow(10f, numSize - 3); break;
            case 45: suffix = "td"; money = money / System.Math.Pow(10f, numSize - 3); break;
            case 48: suffix = "qtd"; money = money / System.Math.Pow(10f, numSize - 3); break;
            case 51: suffix = "qnd"; money = money / System.Math.Pow(10f, numSize - 3); break;
            case 54: suffix = "sxd"; money = money / System.Math.Pow(10f, numSize - 3); break;
            case 57: suffix = "spd"; money = money / System.Math.Pow(10f, numSize - 3); break;
            case 60: suffix = "ocd"; money = money / System.Math.Pow(10f, numSize - 3); break;
            case 63: suffix = "nvd"; money = money / System.Math.Pow(10f, numSize - 3); break;
            case 66: suffix = "vgt"; money = money / System.Math.Pow(10f, numSize - 3); break;
            case 69: suffix = "uvt"; money = money / System.Math.Pow(10f, numSize - 3); break;
            case 72: suffix = "dvt"; money = money / System.Math.Pow(10f, numSize - 3); break;
            case 75: suffix = "tvt"; money = money / System.Math.Pow(10f, numSize - 3); break;
            case 78: suffix = "qtt"; money = money / System.Math.Pow(10f, numSize - 3); break;
            case 81: suffix = "qnt"; money = money / System.Math.Pow(10f, numSize - 3); break;
        }
        //special case
        if (m > 1000f && m < 10000f) {
            return string.Format("{0:N0}", m);
        }
        //normal cases
        if (money < 10f && numSize >= 6) {
            return string.Format("{0:N3}", money) + suffix;
        }
        else if (money < 100f) {
            return string.Format("{0:N2}", money) + suffix;
        }
        else {
            return string.Format("{0:N1}", money) + suffix;
        }
    }

    public static string encodeNumberInteger(int num) {
        return string.Format("{0:0}", num);
    }

    public static string encodeTime(double time) {
        TimeSpan t = TimeSpan.FromSeconds(time);

        string text = "";
        if (t.Days > 0) {
            text += string.Format("{0:0}Days ", t.Days);
        }
        if (t.Hours > 0) {
            text += string.Format("{0:D2}Hrs ", t.Hours);
        }
        text += string.Format("{0:D2}M {1:D2}s ", t.Minutes, t.Seconds);
        return text;
    }
}
