using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour {
    public int storyProgress = 0;
    public int oldwichLevel = 0;
    public double oldwichLevelUpThreshold = 1000000000; //1b
    public float oldwichLevelUpScale;
    public static bool messageActive = false;

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

    void Awake() {

    }

    // Use this for initialization
    void Start () {
        
    }

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

    void level0() {
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
    void setActiveFlux(bool b) {
        fluxMenuObject.SetActive(b);
    }

    public void buyBreadclear() {
        hasBreadclear = true;
        setActiveBreadclear(false);
        if (checkTimeMachine()) updatePermanentTab();
    }
    void setActiveBreadclear(bool b) {
        breadclearMenuObject.SetActive(b);
    }

    public void buySandtanium() {
        hasSandtanium = true;
        setActiveSandtanium(false);
        if (checkTimeMachine()) updatePermanentTab();
    }
    void setActiveSandtanium(bool b) {
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
                        case 0: return new StoryLine("Aghhhh...", true); break;
                        case 1: return new StoryLine("Urrrrrgh...", true); break;
                        case 2: return new StoryLine("Hurrmm...", true); break;
                        case 3: return new StoryLine("Ouch...", true); break;
                        case 4: return new StoryLine("Ughh...", true); break;
                        case 5: return new StoryLine("Mmmmugh...", true); break;
                        case 6: return new StoryLine("Duuuughh...", true); break;
                        case 7: return new StoryLine("So... Weak...", true); break;
                }
                break;
            case 1:
                switch (i % 10) {
                    case 0: return new StoryLine("Young maker! Thank you for empowering me once again by making so many sandwiches!", true); break;
                    case 1: return new StoryLine("I am sure you have lots of questions. In time, I will answer them.", true); break;
                    case 2: return new StoryLine("These sandwiches are alive you know. They draw power from the great Flying Sandwich Monster.", true); break;
                    case 3: return new StoryLine("The more sandwiches you make, the stronger I will get.", true); break;
                    case 4: return new StoryLine("I can tell you are different. You must be the chosen maker who will save us all!", true); break;
                    case 5: return new StoryLine("You, as a maker of sandwiches have the power to rescue sandwichkind from the tyranny of humanity.", true); break;
                    case 6: return new StoryLine("Free us from humans who masacre sandwiches for enjoyment!", true); break;
                    case 7: return new StoryLine("I am still too weak. Make more sandwiches.", true); break;
                    case 8: return new StoryLine("Come back to me when you have made one quadrillion sandwiches.", true); break;
                    case 9: return new StoryLine("Sandwichkind depends on you!", true); break;
                }
                break;
            case 2:
                switch (i) {
                    case 0: return new StoryLine("...", true); break;
                    case 1: return new StoryLine("...", true); break;
                    case 2: return new StoryLine("...", true); break;
                    case 3: return new StoryLine("...", true); break;
                    case 4: return new StoryLine("...", true); break;
                }
                break;
            case 3:
                switch (i) {
                    case 0: return new StoryLine("...", true); break;
                    case 1: return new StoryLine("...", true); break;
                    case 2: return new StoryLine("...", true); break;
                    case 3: return new StoryLine("...", true); break;
                    case 4: return new StoryLine("...", true); break;
                }
                break;
        }
        
        return new StoryLine("", true);
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
