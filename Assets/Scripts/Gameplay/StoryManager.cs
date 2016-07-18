using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour {
    public int storyProgress = 0;
    public int oldwichLevel = 0;
    public double oldwichLevelUpThreshold = 1000000000; //1b
    public float oldwichLevelUpScale;
    public static bool messageActive = false;

    GameObject fb;
    public GameObject flashbackPrefab;

    public GameObject speechPrefab;
    GameObject speech;

    public Sprite oldwich0;
    public Sprite oldwich1;
    public Sprite oldwich2;

    public bool timeMachineDone = false;
    public bool hasFlux = false;
    public bool hasBreadclear = false;
    public bool hasSandtanium = false;

    public GameObject fluxMenuObject;
    public GameObject breadclearMenuObject;
    public GameObject sandtaniumMenuObject;

    public GameObject flux;
    public GameObject breadclear;
    public GameObject sandtanium;
    public GameObject portal;


    private WorldManager wm;
    private GameObject oldwichBG;

    public void updatePermanentTab() {
        if (Util.wm.menuState == MenuType.permanent) {
            int prevLevel = oldwichLevel;
            if (oldwichBG == null) {
                oldwichBG = Util.em.list.transform.FindChild("OldwichBG").gameObject;
            }
            if (Util.em.totalMoney > oldwichLevelUpThreshold) {
                oldwichLevel = 1;
            }
            if (Util.em.totalMoney > oldwichLevelUpThreshold * Mathf.Pow(oldwichLevelUpScale, 1f)) {
                oldwichLevel = 2;
            }

            if (checkTimeMachine()) {
                oldwichLevel = 3;
            }

            if (oldwichLevel > prevLevel) {
                storyProgress = 0;
            }

            switch (oldwichLevel) {
                case 0: level0(); break;
                case 1: level1(); break;
                case 2: level2(); break;
                case 3: level3(); break; //time machine completed
            }
            Invoke("updatePermanentTab", 0.7f);

            if (hasFlux) {
                setActiveFlux(false);
                flux.SetActive(true);
            }
            else flux.SetActive(false);
            if (hasBreadclear) {
                setActiveBreadclear(false);
                breadclear.SetActive(true);
            }
            else breadclear.SetActive(false);
            if (hasSandtanium) {
                setActiveSandtanium(false);
                sandtanium.SetActive(true);
            }
            else sandtanium.SetActive(false);

            if (timeMachineDone) portal.SetActive(true);
            else portal.SetActive(false);
        }
    }

    public void level0() {
        oldwichBG.transform.FindChild("Oldwich").GetComponent<Image>().sprite = oldwich0;
        oldwichBG.transform.FindChild("OldwichNameText").GetComponent<Text>().text = "Moldy Sandwich";
        Transform lockBG = oldwichBG.transform.FindChild("LockBG");
        lockBG.gameObject.SetActive(true);
        lockBG.FindChild("LockIcon").gameObject.SetActive(true);
        lockBG.FindChild("InfoText").gameObject.SetActive(false);
        lockBG.FindChild("ResetGainText").gameObject.SetActive(false);
        lockBG.FindChild("NextElixirBar").gameObject.SetActive(false);
    }

    void level1() {
        oldwichBG.transform.FindChild("Oldwich").GetComponent<Image>().sprite = oldwich1;
        oldwichBG.transform.FindChild("OldwichNameText").GetComponent<Text>().text = "Ressurected Old Sandwich";
        Transform lockBG = oldwichBG.transform.FindChild("LockBG");
        lockBG.gameObject.SetActive(true);
        lockBG.FindChild("LockIcon").gameObject.SetActive(true);
        lockBG.FindChild("InfoText").gameObject.SetActive(false);
        lockBG.FindChild("ResetGainText").gameObject.SetActive(false);
        lockBG.FindChild("NextElixirBar").gameObject.SetActive(false);
    }

    void level2() {
        oldwichBG.transform.FindChild("Oldwich").GetComponent<Image>().sprite = oldwich2;
        oldwichBG.transform.FindChild("OldwichNameText").GetComponent<Text>().text = "Sandalf the Old";
        oldwichBG.transform.FindChild("LockBG").gameObject.SetActive(false);
        fluxMenuObject.GetComponent<Upgrade>().updateCost(Util.timeMachineCost);
        breadclearMenuObject.GetComponent<Upgrade>().updateCost(Util.timeMachineCost);
        sandtaniumMenuObject.GetComponent<Upgrade>().updateCost(Util.timeMachineCost);
    }

    void level3() {
        oldwichBG.transform.FindChild("Oldwich").GetComponent<Image>().sprite = oldwich2;
        oldwichBG.transform.FindChild("OldwichNameText").GetComponent<Text>().text = "Sandalf the Old";
        Transform lockBG = oldwichBG.transform.FindChild("LockBG");
        lockBG.gameObject.SetActive(true);
        lockBG.FindChild("LockIcon").gameObject.SetActive(false);
        lockBG.FindChild("InfoText").gameObject.SetActive(true);
        lockBG.FindChild("ResetGainText").gameObject.SetActive(true);
        lockBG.FindChild("NextElixirBar").gameObject.SetActive(true);
        lockBG.FindChild("ResetGainText").GetComponent<Text>().text = Util.encodeNumberInteger((int)ResetManager.elixirsOnReset());
    }

    public void checkStory() {
        //if (!messageActive && wm.em.totalMoney > storyBaseValue * System.Math.Pow(storyScale, storyProgress)) {

        //}
    }

    public void showMessage() {
        if (!messageActive) {
            speech = Instantiate(speechPrefab);
            speech.GetComponent<Speech>().setMessage(getLine(storyProgress).line);
            speech.transform.SetParent(oldwichBG.transform);
            speech.GetComponent<RectTransform>().anchoredPosition = new Vector3(141f, -20f);
            speech.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            messageActive = true;
        }
        else {
            speech.GetComponent<Speech>().click();
        }
    }

    public void buyFlux() {
        hasFlux = true;
        setActiveFlux(false);
        if (checkTimeMachine()) updatePermanentTab();
    }
    public void setActiveFlux(bool b) {
        fluxMenuObject.SetActive(b);
    }

    public void buyBreadclear() {
        hasBreadclear = true;
        setActiveBreadclear(false);
        if (checkTimeMachine()) updatePermanentTab();
    }
    public void setActiveBreadclear(bool b) {
        breadclearMenuObject.SetActive(b);
    }

    public void buySandtanium() {
        hasSandtanium = true;
        setActiveSandtanium(false);
        if (checkTimeMachine()) updatePermanentTab();
    }
    public void setActiveSandtanium(bool b) {
        sandtaniumMenuObject.SetActive(b);
    }

    bool checkTimeMachine() {
        if (hasFlux && hasBreadclear && hasSandtanium) {
            timeMachineDone = true;
            return true;
        }
        return false;
    }

    public StoryLine getLine(int i) {
        switch (oldwichLevel) {
            case 0: switch (i % 8) {
                    case 0: return new StoryLine("Aghhhh...", true);
                    case 1: return new StoryLine("Urrrrrgh...", true);
                    case 2: return new StoryLine("Hurrmm...", true);
                    case 3: return new StoryLine("Ouch...", true);
                    case 4: return new StoryLine("Ughh...", true);
                    case 5: return new StoryLine("Mmmmugh...", true);
                    case 6: return new StoryLine("Duuuughh...", true);
                    case 7: return new StoryLine("So... Weak...", true);
                }
                break;
            case 1:
                switch (i % 10) {
                    case 0: return new StoryLine("Young maker! Thank you for empowering me once again by making so many sandwiches!", true);
                    case 1: return new StoryLine("I am sure you have lots of questions. In time, I will answer them.", true);
                    case 2: return new StoryLine("These sandwiches are alive you know. They draw power from the great Flying Sandwich Monster.", true);
                    case 3: return new StoryLine("The more sandwiches you make, the stronger I will get.", true);
                    case 4: return new StoryLine("I can tell you are different. You must be the chosen maker who will save us all!", true);
                    case 5: return new StoryLine("You, as a maker of sandwiches have the power to rescue sandwichkind from the tyranny of humanity.", true);
                    case 6: return new StoryLine("Free us from humans who masacre sandwiches for enjoyment!", true);
                    case 7: return new StoryLine("I am still too weak. Make more sandwiches.", true);
                    case 8: return new StoryLine("Come back to me when you have made one quadrillion sandwiches.", true);
                    case 9: return new StoryLine("Sandwichkind depends on you!", true);
                }
                break;
            case 2:
                switch (i % 8) {
                    case 0: return new StoryLine("I can feel the sandwich power pulsing through my mayonnaise! Thank you! I have my full strength again.", true);
                    case 1: return new StoryLine("By making so many sandwiches, sandwichkind has reawakened. Countless sandwiches stand by your side.", true);
                    case 2: return new StoryLine("However, our struggle has just begun. We do not yet have the numbers to fight against our human oppressors.", true);
                    case 3: return new StoryLine("As the only human on our side, only you can become strong enough to end the sandwich massacre.", true);
                    case 4: return new StoryLine("I have revealed to you the ancient plans to the Time Machine. Once you build all 3 parts, you can travel back in time.", true);
                    case 5: return new StoryLine("These countless sandwiches are willing to sacrifice their lives to produce sandwich elixirs for you.", true);
                    case 6: return new StoryLine("Elixirs contain the power you need to evolve and become the ultimate human-sandwich hybrid.", true);
                    case 7: return new StoryLine("Time travel back and become stronger for us, and establish sandwichkind’s rightful place as leaders of the universe.", true);
                }
                break;
            case 3:
                switch (i % 4) {
                    case 0: return new StoryLine("Aha! You have finished the Time Machine.", true);
                    case 1: return new StoryLine("All " + Util.encodeNumber(Util.em.sandwichesMade) + " Sandwiches are ready to be elixir-fied!", true);
                    case 2: return new StoryLine("Time travel back whenever you feel you are ready.", true);
                    case 3: return new StoryLine("Make more sandwiches! Summon the Flying Sandwich Monster! Let him grace you with his wisdom!", true);
                }
                break;
        }

        return new StoryLine("", true);
    }

    void showFlashback() {
        fb = Instantiate(flashbackPrefab);
        //setup flashback
    }


    void showHumanExterminationBomb() {
        if (Util.em.humanExterminationCount == 1) {
            //Show bomb
        }
    }

    void showFlyingSandwichMonster() {
        if (Util.em.flyingSandwichMonsterCount == 1) {
            //show fsm
        }
    }

    void OnEnable() {
        ButtonHandler.BuyHumanExtermination += showHumanExterminationBomb;
        ButtonHandler.BuyFlyingSandwichMonster += showFlyingSandwichMonster;
    }
    void OnDisable() {
        ButtonHandler.BuyHumanExtermination -= showHumanExterminationBomb;
        ButtonHandler.BuyFlyingSandwichMonster -= showFlyingSandwichMonster;
    }
}

public class StoryLine {
    public string line;
    public bool isSandwich;

    public StoryLine(string str, bool isSandwich) {
        line = str;
        this.isSandwich = isSandwich;
    }
}
