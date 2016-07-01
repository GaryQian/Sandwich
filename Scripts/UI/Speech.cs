using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Speech : MonoBehaviour {
    public float clickDelay = 2f;
    public bool ready = false;
	// Use this for initialization
	void Start () {
        Invoke("setReady", clickDelay);
	}

    void setReady() {
        ready = true;
    }

    public void click() {
        if (ready) {
            Destroy(gameObject);
            StoryManager.messageActive = false;
            Util.wm.sm.storyProgress++;
        }
    }

    public void setMessage(string msg) {
        transform.FindChild("Text").GetComponent<Text>().text = msg;
    }
	
}
