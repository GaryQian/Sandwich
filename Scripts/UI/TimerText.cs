using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TimerText : MonoBehaviour {
    WorldManager wm;
    Text txt;
    Color normalColor;
    void Awake() {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        txt = GetComponent<Text>();
        normalColor = txt.color;
    }
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        TimeSpan t = TimeSpan.FromSeconds(wm.adWatchTime);

        txt.text = string.Format("{0:D2}:{1:D2}",
                t.Minutes,
                t.Seconds);
        /*if (wm.adWatchTime <= 0) {
            txt.color = new Color(0, 0.9f, 0.05f);
        }
        else {
            txt.color = normalColor;
        }*/
    }
}
