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

    public Sprite oldwich0;
    public Sprite oldwich1;
    public Sprite oldwich2;

    public bool timeMachineDone = false;
    public bool hasFlux = false;
    public bool hasBreadclear = false;
    public bool hasSandtanium = false;

    private WorldManager wm;
    private GameObject oldwichBG;
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
            if (timeMachineDone) {
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
    }

    void level1() {
        oldwichBG.transform.FindChild("Oldwich").GetComponent<Image>().sprite = oldwich1;
        oldwichBG.transform.FindChild("OldwichNameText").GetComponent<Text>().text = "Mysterious Sandwich";
        Transform lockBG = oldwichBG.transform.FindChild("LockBG");
        lockBG.gameObject.SetActive(true);
        lockBG.FindChild("LockIcon").gameObject.SetActive(true);
        lockBG.FindChild("InfoText").gameObject.SetActive(false);
        lockBG.FindChild("ResetGainText").gameObject.SetActive(false);
    }

    void level2() {
        oldwichBG.transform.FindChild("Oldwich").GetComponent<Image>().sprite = oldwich2;
        oldwichBG.transform.FindChild("OldwichNameText").GetComponent<Text>().text = "Wise Old Sandwich";
        oldwichBG.transform.FindChild("LockBG").gameObject.SetActive(false);
    }

    void level3() {
        oldwichBG.transform.FindChild("Oldwich").GetComponent<Image>().sprite = oldwich2;
        oldwichBG.transform.FindChild("OldwichNameText").GetComponent<Text>().text = "Time-Master Sandwich";
        Transform lockBG = oldwichBG.transform.FindChild("LockBG");
        lockBG.gameObject.SetActive(true);
        lockBG.FindChild("LockIcon").gameObject.SetActive(false);
        lockBG.FindChild("InfoText").gameObject.SetActive(true);
        lockBG.FindChild("ResetGainText").gameObject.SetActive(true);
        lockBG.FindChild("ResetGainText").GetComponent<Text>().text = Util.encodeNumberInteger(amountGained());
    }

    public int amountGained() {
        return 10;
    }

    public void checkStory() {
        //if (!messageActive && wm.em.totalMoney > storyBaseValue * System.Math.Pow(storyScale, storyProgress)) {

        //}
    }

    public void showMessage() {
        if (!messageActive) {
            GameObject speech = Instantiate(speechPrefab);
            speech.GetComponent<Speech>().setMessage(getLine(storyProgress).line);
            speech.transform.SetParent(oldwichBG.transform);
            speech.GetComponent<RectTransform>().anchoredPosition = new Vector3(141f, -20f);
            speech.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            messageActive = true;
        }
    }

    public StoryLine getLine(int i) {
        switch (oldwichLevel) {
            case 0: switch (i % 8) {
                        case 0: return new StoryLine("...", true); break;
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
                switch (i) {
                    case 0: return new StoryLine("...", true); break;
                    case 1: return new StoryLine("...", true); break;
                    case 2: return new StoryLine("...", true); break;
                    case 3: return new StoryLine("...", true); break;
                    case 4: return new StoryLine("...", true); break;
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
