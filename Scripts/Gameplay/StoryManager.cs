using UnityEngine;
using System.Collections;

public class StoryManager : MonoBehaviour {
    public int storyProgress = 0;
    public double storyBaseValue;
    public static bool messageActive = false;
    private WorldManager wm;
    // Use this for initialization
    void Start () {
	    
	}

    public void checkStory() {
        //if (!messageActive && wm.money > storyBaseValue * 
    }

    public StoryLine getLine(int i) {
        switch (i) {
            case 0: return new StoryLine("...", true); break;
            case 1: return new StoryLine("...", true); break;
            case 2: return new StoryLine("...", true); break;
            case 3: return new StoryLine("...", true); break;
            case 4: return new StoryLine("...", true); break;
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
