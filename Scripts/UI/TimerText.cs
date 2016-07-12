using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public enum TimerType { money, elixir, x2 }

public class TimerText : MonoBehaviour {
    WorldManager wm;
    Text txt;
    public TimerType type;
    //Color normalColor;
    void Awake() {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        txt = GetComponent<Text>();
        //normalColor = txt.color;
    }
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (!Util.even) {
            if (type == TimerType.money) {
                if (wm.adWatchTimeMoney > 0) {
                    TimeSpan t = TimeSpan.FromSeconds(wm.adWatchTimeMoney);

                    txt.text = string.Format("{0:D2}:{1:D2}",
                            t.Minutes,
                            t.Seconds);
                }
                else {
                    if (Advertisement.IsReady()) {
                        txt.text = "Ready!";
                    }
                    else {
                        txt.text = "Loading";
                    }
                }
            }
            else if (type == TimerType.elixir) {
                if (wm.adWatchTimeElixir > 0) {
                    TimeSpan t = TimeSpan.FromSeconds(wm.adWatchTimeElixir);

                    txt.text = string.Format("{0:D2}:{1:D2}",
                            t.Minutes,
                            t.Seconds);
                }
                else {
                    if (Advertisement.IsReady()) {
                        txt.text = "Ready!";
                    }
                    else {
                        txt.text = "Loading";
                    }
                }
            }
            else if (type == TimerType.x2) {
                if (wm.adWatchTimex2 > 0 && wm.x3Time <= 0 && wm.x7Time <= 0) {
                    TimeSpan t = TimeSpan.FromSeconds(wm.adWatchTimex2);
                    txt.fontSize = 100;
                    txt.text = string.Format("{0:D2}:{1:D2}",
                            t.Minutes,
                            t.Seconds);
                }
                else {
                    if (wm.x2Time > 0 || wm.x3Time > 0 || wm.x7Time > 0) {
                        txt.text = "Boosting!";
                        txt.fontSize = 85;
                    }
                    else if (Advertisement.IsReady()) {
                        txt.text = "Ready!";
                        txt.fontSize = 100;
                    }
                    else {
                        txt.text = "Loading";
                        txt.fontSize = 100;
                    }
                }
            }
        }
    }
}
