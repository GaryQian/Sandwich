using UnityEngine;
using System.Collections;

public class ResetManager : MonoBehaviour {
    public GameObject warningPrefab;
    public GameObject warning;

    public GameObject whitescreen;

    public static AudioClip timeTravel;
    public AudioClip timeTravelSound;
    /// <summary>
    /// EVENTS
    /// </summary>
    /// 
    public delegate void ResetEvent();
    public static event ResetEvent RESET;



	// Use this for initialization
	void Start () {
        timeTravel = timeTravelSound;
	}

    public static long elixirsOnReset() {
        double num = System.Math.Pow(Util.em.totalMoney / Util.elixirBaseCost, 1f / Util.elixirScale);
        return (long)System.Math.Floor(num);
    }

    public static double costOfElixirs(long curr) {
        return Util.elixirBaseCost * System.Math.Pow(curr, Util.elixirScale);
    }

    public static double moneyRemainingNextElixir() {
        return costOfElixirs(elixirsOnReset() + 1) - Util.em.totalMoney;
    }

    public static double nextElixirCost() {
        long num = elixirsOnReset();
        return Util.elixirBaseCost * (System.Math.Pow(num + 1, Util.elixirScale) - System.Math.Pow(num, Util.elixirScale));
    }

    public void showResetWarning() {
        if (Util.wm.sm.timeMachineDone) {
            warning = Instantiate(warningPrefab);
            warning.transform.SetParent(Util.wm.canvas.transform);
            warning.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 400f);
            warning.transform.localScale = new Vector3(1f, 1f, 1f);
            warning.transform.SetAsLastSibling();
        }
    }

    public static void reset() {
        WorldManager wm = Util.wm;
        EconomyManager em = Util.em;

        wm.playthroughCount++;

        //transfer into totals
        em.elixir += elixirsOnReset();
        em.totalElixir += elixirsOnReset();
        em.lifetimeMoney += em.totalMoney;
        em.lifetimeSandwichesMade += em.sandwichesMade;
        em.lifetimeBuildings += em.buildings;
        em.lifetimeSwipes += em.totalSwipes;

        //reset to defaults

        em.money = 0;
        em.totalMoney = 0;
        em.knifeVamp = 0; //amount of the total cps gained on each swipe
        em.sauceID = 1; //which is the current spread
        em.breadID = 0;
        em.gameTime = 0f;
        em.rate = 0;
        em.sandwichValue = 0;

        //ButtonHandler.buyCount = 1;

        em.buildings = 0;
        em.sandwichCartCount = 0;
        em.deliCount = 0;
        em.autochefCount = 0;
        em.mcdandwichCount = 0;
        em.sandwichCityCount = 0;
        em.breadCloningCount = 0;
        em.sandwocracyCount = 0;
        em.sandriaLawCount = 0;
        em.sandwichPlanetCount = 0;
        em.humanExterminationCount = 0;
        em.sandwichFleetCount = 0;
        em.enslaveAliensCount = 0;
        em.deathSandwichCount = 0;
        em.sandwichGalaxyCount = 0;
        em.flyingSandwichMonsterCount = 0;

        em.nurseryPop = 0;
        em.reproductionRate = 0;

        wm.sm.hasFlux = false;
        wm.sm.hasBreadclear = false;
        wm.sm.hasSandtanium = false;
        wm.sm.timeMachineDone = false;
        wm.sm.storyProgress = 0;
        wm.sm.oldwichLevel = 0;

        wm.tutorialManager.tutorialActive = false;

        //temp mute
        bool tempMute = wm.muted;
        wm.muted = true;

        //redo calculations
        em.recalculate();
        wm.sm.updatePermanentTab();
        wm.sm.level0();
        wm.sm.setActiveFlux(true);
        wm.sm.setActiveBreadclear(true);
        wm.sm.setActiveSandtanium(true);
        wm.tabManager.selectProducer();
        em.list.transform.FindChild("BuyCounterSwitchPanel1").FindChild("SwitchButton").gameObject.GetComponent<Switch>().setx1();
        em.updateProducerMenuCounters();
        wm.sauce.GetComponent<Sauce>().update();
        Bread.updateLabel();
        em.swipe();
        em.money = 0;
        em.moneyText.updateColor();

        GameObject.Find("FIRE").GetComponent<Animator>().SetTrigger("FireFade");

        wm.muted = tempMute;

        em.save();

#if UNITY_ANDROID
        Social.ReportProgress("CgkI1rDm6sMKEAIQAg", 100.0f, (bool success) => { });
#elif UNITY_IOS
        Social.ReportProgress("timetraveler", 100.0f, (bool success) => { });
#endif

        if (RESET != null) RESET();

    }

    public static void completeReset() {
        reset();
        WorldManager wm = Util.wm;
        EconomyManager em = Util.em;

        wm.playthroughCount = 0;

        //transfer into totals
        em.elixir = 0;
        em.totalElixir = 0;
        em.lifetimeMoney = 0;
        em.lifetimeSandwichesMade = 0;
        em.lifetimeBuildings = 0;
        em.lifetimeSwipes = 0;

        em.totalTime = 0;

        wm.sandWitchesClicked = 0;

        em.toasterVisionLevel = 0;
        em.communalMindLevel = 0;
        em.dexterousHandsLevel = 1;

        wm.knifeID = 0;


        em.recalculate();
    }
}
