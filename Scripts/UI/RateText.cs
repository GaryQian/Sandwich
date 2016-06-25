using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RateText : MonoBehaviour {
    Text txt;
    WorldManager wm;
    // Use this for initialization
    void Start() {
        txt = GetComponent<Text>();
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        //txt.fontSize = (int) (Screen.height / 7f);
        //GetComponent<RectTransform>().offsetMax = new Vector2((Screen.width / 4f), -Screen.height * 0.183f);
        //GetComponent<RectTransform>().offsetMin = new Vector2((Screen.width / -4f), -Screen.height * 0.3f);
    }

    public void updateRate(double rate) {
        txt.text = "+$" + wm.encodeNumber(rate) + " / sec";
    }
}
