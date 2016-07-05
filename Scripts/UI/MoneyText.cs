using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour {
    public Image bg1;
    public Image bg2;
    public Image mute1;
    public Image mute2;
    public Image info;
    public GameObject SuffixInfo;
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

    public void updateColor() {
        switch (Util.wm.playthroughCount % 4) {
            case 0:
                //GetComponent<Text>().color = new Color(0.086f, 0.5f, 0);
                bg1.color = new Color(0.737f, 1f, 0);
                break;
            case 1:
                bg1.color = new Color(0, 0.8f, 1f);
                break;
            case 2:
                bg1.color = new Color(1f, 0.9f, 0);
                break;
            case 3:
                bg1.color = new Color(1f, 0, 0.5f);
                break;
        }

        bg2.color = bg1.color * bg1.color * new Color(0.8f, 0.8f, 0.8f);
        txt.color = bg1.color * bg1.color * bg1.color * new Color(0.5f, 0.5f, 0.5f);
        mute1.color = (bg1.color * bg1.color * bg1.color + Color.white * 0.3f) * 0.5f;
        mute2.color = mute1.color;
        info.color = mute1.color;
    }

    public void updateMoney(double money) {
        txt.text = "$" + Util.encodeNumber(money);
    }

    public void bounce() {
        GetComponent<Animator>().SetTrigger("Bounce");
        Instantiate(SuffixInfo);
    }
}