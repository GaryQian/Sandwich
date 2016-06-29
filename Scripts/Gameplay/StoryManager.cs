using UnityEngine;
using System.Collections;

public class StoryManager : MonoBehaviour {
    public int storyProgress = 0;
    // Use this for initialization
    void Start () {
	    
	}

    public void checkStory() {

    }

    public string getLine(int i) {
        switch (i) {

        }
        return "";
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
