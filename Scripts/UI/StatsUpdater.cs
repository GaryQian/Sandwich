using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsUpdater : MonoBehaviour {
    public GameObject creditsPrefab;

    //LEFT
    public Text spsText;
    public Text mpsText;
    public Text sandiwchValueText;
    public Text multiplierText;

    public Text moneyMadeText;
    public Text sandiwchesMadeText;
    public Text swipesText;
    public Text buildingsText;

    


    //RIGHT
    public Text gameTimeText;
    public Text totalTimeText;
    public Text resetsText;
    public Text sandWitchesEatenText;

    public Text TotalmoneyMadeText;
    public Text TotalsandiwchesMadeText;
    public Text TotalswipesText;
    public Text TotalbuildingsText;

    public Text elixirsOwnedText;
    public Text elixirsEarnedText;
    public Text evolutionsText;

    void Awake() {
        
    }
	// Use this for initialization
	void Start () {
        
	}

    // Update is called once per frame
    public void updateStats() {
        //left
        spsText.text = Util.encodeNumber(Util.em.rate) + " &/s";
        mpsText.text = "+$" + Util.encodeNumber(Util.em.rate * Util.em.sandwichValue) + " /s";
        sandiwchValueText.text = "$" + Util.encodeNumber(Util.em.sandwichValue) + " each &";
        multiplierText.text = Util.encodeNumberInteger((int)((-2f + Util.em.toasterVisionBonus + Util.em.communalMindBonus) * 100f)) + "% Bonus";

        //current
        moneyMadeText.text = "$" + Util.encodeNumber(Util.em.totalMoney);
        sandiwchesMadeText.text = Util.encodeNumber(Util.em.sandwichesMade);
        swipesText.text = Util.encodeNumberInteger(Util.em.totalSwipes);
        buildingsText.text = Util.encodeNumberInteger(Util.em.buildings);

        


        //right
        gameTimeText.text = Util.encodeTime(Util.em.gameTime);
        totalTimeText.text = Util.encodeTime(Util.em.totalTime);
        resetsText.text = Util.encodeNumberInteger(Util.wm.playthroughCount) + " times";
        sandWitchesEatenText.text = Util.encodeNumberInteger(Util.wm.sandWitchesClicked);      
          
        //total
        TotalmoneyMadeText.text = "$" + Util.encodeNumber(Util.em.totalMoney + Util.em.lifetimeMoney);
        TotalsandiwchesMadeText.text = Util.encodeNumber(Util.em.sandwichesMade + Util.em.lifetimeSandwichesMade);
        TotalswipesText.text = Util.encodeNumberInteger(Util.em.totalSwipes + Util.em.lifetimeSwipes);
        TotalbuildingsText.text = Util.encodeNumberInteger(Util.em.buildings + Util.em.lifetimeBuildings);

        //elixir
        elixirsOwnedText.text = Util.encodeNumberInteger((int)Util.em.elixir);
        elixirsEarnedText.text = Util.encodeNumberInteger((int)Util.em.totalElixir);
        evolutionsText.text = Util.encodeNumberInteger(-1 + Util.em.toasterVisionLevel + Util.em.communalMindLevel + Util.em.dexterousHandsLevel);
    }

    void OnEnable() {
        InvokeRepeating("updateStats", 0, 0.5f);
    }

    void OnDisable() {
        CancelInvoke("updateStats");
    }

    public void showCredits() {
        Instantiate(creditsPrefab);
    }
}
