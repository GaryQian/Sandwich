﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class WatchButton : MonoBehaviour {
    private Upgrade up;
    private Button button;
    ColorBlock enabledColor;
    ColorBlock disabledColor;
    WorldManager wm;
    // Use this for initialization
    void Awake() {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        up = transform.parent.GetComponent<Upgrade>();
        button = GetComponent<Button>();
    }

    void Start() {
        disabledColor = button.colors;
        disabledColor.normalColor = new Color(.5f, .5f, .5f);
        disabledColor.highlightedColor = new Color(.5f, .5f, .5f);
        disabledColor.pressedColor = new Color(.3f, .3f, .3f);
        enabledColor = button.colors;
    }

    // Update is called once per frame
    void Update() {
        if (wm.adWatchTime > 0 || !Advertisement.IsReady()) {
            //disable
            button.colors = disabledColor;
        }
        else {
            //enable
            button.colors = enabledColor;
        }
    }
}