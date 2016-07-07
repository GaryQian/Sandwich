using UnityEngine;
using System.Collections;
using System;

public class Util {
    public static WorldManager wm;
    public static EconomyManager em;

    public static double money;

    public static double sauceBaseCost = 500f;
    public static double sauceScale = 10f;
    public static double breadBaseCost = 1000000f;
    public static double breadScale = 400f;

    public static double knifeVampBaseCost = 2000000f;
    public static double knifeVampScale = 200f;
    public static float knifeVampRate = 0.04f;

    
    public static float pScale = 1.15f;

    //producer rates
    public static float sandwichCartRate = 0.5f;
    public static float deliRate = 8f;
    public static float autochefRate = 75f;
    public static float mcdandwichRate = 800f;
    public static float sandwichCityRate = 10000f;
    public static float breadCloningRate = 90000f;
    public static float sandwocracyRate = 850000f;
    public static float sandriaLawRate = 6500000f;
    public static double sandwichPlanetRate = 55000000f;
    public static double humanExterminationRate = 400000000f;
    public static double enslaveAliensRate = 3000000000f;
    public static double deathSandwichRate = 25000000000f;
    public static double sandwichGalaxyRate = 200000000000f;
    public static double flyingSandwichMonsterRate = 3500000000000f;

    //producer base costs
    public static float sandwichCartBase = 50f;
    public static float deliBase = 1200f;
    public static float autochefBase = 200000f;
    public static float mcdandwichBase = 14000000f;
    public static float sandwichCityBase = 1E+9f;
    public static double breadCloningBase = 1.3E+11f;
    public static double sandwocracyBase = 1.7E+13f;
    public static double sandriaLawBase = 2E+15f; //^15 = quadrillion
    public static double sandwichPlanetBase = 3.141E+17f;
    public static double humanExterminationBase = 4E+19;
    public static double enslaveAliensBase = 1E+22;
    public static double deathSandwichBase = 5E+24;
    public static double sandwichGalaxyBase = 1E+27;
    public static double flyingSandwichMonsterBase = 9.999E+30;


    //elixir costs
    public static float toasterVisionBase = 8f;
    public static float toasterVisionScale = 1.1f;
    public static float communalMindBase = 8f;
    public static float communalMindScale = 1.1f;
    public static float dexterousHandsBase = 5;
    public static float dexterousHandsScale = 1.1f;

    //time machine
    public static double timeMachineCost = 1E+15f;

    //elixir
    public static double elixirBaseCost = 5E+5;
    public static float elixirScale = 5f;

    //ads
    public static float adRewardCurrentPercentage = 0.10f;
    public static float adRewardSwipes = 400f;
    public static float adRewardTime = 300f;
    public static float adRewardTotalPercentage = 0.02f;
    public static double adMoneyCooldown = 1200f;
    public static double adElixirCooldown = 1800f;

    //Boosts
    public static float boostTime = 10800f;

    //sand witch
    public static float sandWitchTotalPercentage = 0.01f;
    public static float sandWitchCurrentPercentage = 0.10f;
    public static float sandWitchDelay = 200f;

    //screen ratios
    public static float screenToCanvasRatio;
    public static float screenCenterXCoord;
    public static Vector3 worldBottomLeftPos;
    public static Vector3 worldTopRightPos;
    public static float worldWidth;
    public static float worldHeight;
    public static Vector3 worldNormBottomLeftPos;
    public static Vector3 worldNormTopRightPos;
    public static float worldNormalizedWidth;

    public static bool muted;
    public static bool musicMuted;

    public static bool even;

    public static string encodeNumber(double m) {
        double money = m * 1.000001f;
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
        if (m >= 1000f && m < 10000f) {
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

    public static string encodeTimeShort(double time) {
        TimeSpan t = TimeSpan.FromSeconds(time);

        string text = "";
        if (t.Days > 0) {
            text += string.Format("{0:0}D ", t.Days);
        }
        if (t.Hours > 0) {
            text += string.Format("{0:D2}H ", t.Hours);
        }
        text += string.Format("{0:D1}M", t.Minutes);
        return text;
    }




    //Canvas scalers
    public static float convertToCanvasWidth(float ratio) {
        return 0;
    }
}
