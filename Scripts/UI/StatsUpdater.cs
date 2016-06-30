using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsUpdater : MonoBehaviour {
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
        multiplierText.text = "100%";

        moneyMadeText.text = "$" + Util.encodeNumber(Util.em.totalMoney);
        sandiwchesMadeText.text = Util.encodeNumber(Util.em.sandwichesMade);
        swipesText.text = Util.encodeNumberInteger(Util.em.totalSwipes);
        buildingsText.text = Util.encodeNumberInteger(Util.em.buildings);


        //right
        gameTimeText.text = Util.encodeTime(Util.em.gameTime);
}

    void OnEnable() {
        InvokeRepeating("updateStats", 0, 0.5f);
    }

    void OnDisable() {
        CancelInvoke("updateStats");
    }
}
