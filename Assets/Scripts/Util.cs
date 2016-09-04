using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class Util {
    public static WorldManager wm;
    public static EconomyManager em;

    public static double sauceBaseCost = 500f;
    public static double sauceScale = 10f;
    public static double breadBaseCost = 1000000f;
    public static double breadScale = 400f;

    public static double knifeVampBaseCost = 2000000f;
    public static double knifeVampScale = 300f;
    public static float knifeVampRate = 0.03f;

    public static double sandwichReproductionBase = 20000f;
    public static double sandwichReproductionScale = 600000f;
    public static float maxBabyTime = 21600f;

    
    public static float pScale = 1.15f;
    public static float x10BuyCostScale;
    public static float buyNumberScale = 1f;

    //producer rates
    public static float sandwichCartRate = 0.5f;
    public static float deliRate = 8f;
    public static float autochefRate = 70f;
    public static float mcdandwichRate = 1000f;
    public static float sandwichCityRate = 10000f;
    public static float breadCloningRate = 82500f;
    public static float sandwocracyRate = 750000f;
    public static float sandriaLawRate = 6E+6f;
    public static double sandwichPlanetRate = 5.8E+7f;
    public static double humanExterminationRate = 6E+8f;
    public static double sandwichFleetRate = 8E+9f;
    public static double enslaveAliensRate = 1E+11f;
    public static double deathSandwichRate = 2E+12f;
    public static double sandwichGalaxyRate = 4E+13f;
    public static double flyingSandwichMonsterRate = 9.999E+14f;

    //producer base costs
    public static float sandwichCartBase = 25f; //100
    public static float deliBase = 1200f; //150 x1.5
    public static float autochefBase = 55550f; //740 x5
    public static float mcdandwichBase = 7777000f; //7777 x10.5
    public static float sandwichCityBase = 1.4E+9f; //108,333 x13.9
    public static double breadCloningBase = 1.414E+11f; //1.57m x14.5
    public static double sandwocracyBase = 2.5E+13f; //23.53m x14.9
    public static double sandriaLawBase = 2.7E+15f; //333.7m x14.2
    public static double sandwichPlanetBase = 3.141E+17f; //5.71b x17
    public static double humanExterminationBase = 6.666E+19; //1.333^11 x23.3
    public static double sandwichFleetBase = 1.701E+22; //3.4^12 x34
    public static double enslaveAliensBase = 2E+24; //1.666^14 x49
    public static double deathSandwichBase = 1E+27; //1^16 x60
    public static double sandwichGalaxyBase = 2.718E+30d; //7.767^17 x77
    public static double flyingSandwichMonsterBase = 9.999E+34d; //1^20 x128


    //elixir costs
    public static float toasterVisionBase = 8f;
    public static float toasterVisionScale = 1.05f;
    public static float communalMindBase = 9f;
    public static float communalMindScale = 1.08f;
    public static float dexterousHandsBase = 3;
    public static float dexterousHandsScale = 1.02f;

    //time machine
    public static double timeMachineCost = 8.8E+17f;

    //elixir
    public static double elixirBaseCost = 3E+7;
    public static float elixirScale = 6f;

    //ads
    public static float adRewardCurrentPercentage = 0.10f;
    public static float adRewardSwipes = 400f;
    public static float adRewardTime = 300f;
    public static float adRewardTotalPercentage = 0.02f;
    public static double adMoneyCooldown = 1800f;
    public static double adElixirCooldown = 1200f;

    public static float adx2Time = 180f;
    public static float adx2Cooldown = 1800f;

    public static float interstitialFrequency = 1800f;

    //Boosts
    public static float boostTime = 14400f;

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

    public static bool even;
    public static bool godmode = false;

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
            text += string.Format("{0:D1}H ", t.Hours);
        }
        text += string.Format("{0:D1}M", t.Minutes);
        if (t.Hours == 0 && t.Days == 0) {
            text += string.Format("{0:D1}S", t.Seconds);
        }
        return text;
    }




    //Canvas scalers
    public static float convertToCanvasWidth(float ratio) {
        return 0;
    }


    public static float GetBatteryLevel() {
#if UNITY_IOS
            /*UIDevice device = UIDevice.CurrentDevice();
            device.batteryMonitoringEnabled = true; // need to enable this first
            Debug.Log("Battery state: " + device.batteryState);
            Debug.Log("Battery level: " + device.batteryLevel);
            return device.batteryLevel*100;*/
        return 100f;
#elif UNITY_ANDROID
        
        if (Application.platform == RuntimePlatform.Android) {
            try {
                using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                    if (null != unityPlayer) {
                        using (AndroidJavaObject currActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
                            if (null != currActivity) {
                                using (AndroidJavaObject intentFilter = new AndroidJavaObject("android.content.IntentFilter", new object[] { "android.intent.action.BATTERY_CHANGED" })) {
                                    using (AndroidJavaObject batteryIntent = currActivity.Call<AndroidJavaObject>("registerReceiver", new object[] { null, intentFilter })) {
                                        int level = batteryIntent.Call<int>("getIntExtra", new object[] { "level", -1 });
                                        int scale = batteryIntent.Call<int>("getIntExtra", new object[] { "scale", -1 });

                                        // Error checking that probably isn't needed but I added just in case.
                                        if (level == -1 || scale == -1) {
                                            return 50f;
                                        }
                                        return ((float)level / (float)scale) * 100.0f;
                                    }

                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex) {
                Debug.Log("Failed to get battery: " + ex.Message);
            }
        }

        return 100f;
#endif
    }


    /*public static DateTime GetNISTDate(bool futureDefault) {
        Debug.Log("Fetching Time from NIST");
        System.Random ran = new System.Random(DateTime.Now.Millisecond);
        DateTime date;
        if (futureDefault) {
            date = new DateTime(9998, 1, 1);
        }
        else {
            date = new DateTime(1, 1, 1);
        }
        string serverResponse = string.Empty;

        // Represents the list of NIST servers
        string[] servers = new string[] {
                         "129.6.15.30",
                         "98.175.203.200",
                         "198.111.152.100",
                         "216.229.0.179",
                         "128.138.140.44",
                         "131.107.13.100",
                         "216.228.192.69",
                         "129.6.15.29"
                          };

        // Try each server in random order to avoid blocked requests due to too frequent request
        for (int i = 0; i < 5; i++) {
            try {
                // Open a StreamReader to a random time server
                StreamReader reader = new StreamReader(new System.Net.Sockets.TcpClient(servers[ran.Next(0, servers.Length)], 13).GetStream());
                serverResponse = reader.ReadToEnd();
                reader.Close();

                // Check to see that the signiture is there
                if (serverResponse.Length > 47 && serverResponse.Substring(38, 9).Equals("UTC(NIST)")) {
                    // Parse the date
                    int jd = int.Parse(serverResponse.Substring(1, 5));
                    int yr = int.Parse(serverResponse.Substring(7, 2));
                    int mo = int.Parse(serverResponse.Substring(10, 2));
                    int dy = int.Parse(serverResponse.Substring(13, 2));
                    int hr = int.Parse(serverResponse.Substring(16, 2));
                    int mm = int.Parse(serverResponse.Substring(19, 2));
                    int sc = int.Parse(serverResponse.Substring(22, 2));

                    if (jd > 51544)
                        yr += 2000;
                    else
                        yr += 1999;

                    date = new DateTime(yr, mo, dy, hr, mm, sc);

                    // Convert it to the current timezone if desired
                    if (false)
                        date = date.ToLocalTime();

                    // Exit the loop
                    break;
                }

            }
            catch (Exception ex) {
                // Do Nothing...try the next server /
            }
        }

        return date;
    }*/
}
