using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour {
    Text txt;
    // Use this for initialization
    void Awake() {
        txt = GetComponent<Text>();
    }
    
    void Start() {
        
        //txt.fontSize = (int) (Screen.height / 7f);
        //GetComponent<RectTransform>().offsetMax = new Vector2((Screen.width / 4f), -Screen.height / 160f);
        //GetComponent<RectTransform>().offsetMin = new Vector2((Screen.width / -4f), -Screen.height / 40f);
    }

    public void updateMoney(double money) {
        txt.text = "$" + Util.encodeNumber(money);
    }
}