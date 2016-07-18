using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AdRewardText : MonoBehaviour {
    WorldManager wm;
    Text txt;
	// Use this for initialization
	void Awake () {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        txt = GetComponent<Text>();
	}

    // Update is called once per frame
    void Update() {
        if (!Util.even) {
            txt.text = "+$" + Util.encodeNumber(wm.buttonHandler.adValue());
        }
	}
}
