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
            case 0://Green
                bg1.color = new Color(0.737f, 1f, 0);
                setOtherColors();
                break;
            case 1://Blue
                bg1.color = new Color(0, 0.8f, 1f);
                setOtherColors();
                break;
            case 2://RED
                bg1.color = new Color(1f, 0.15f, 0.15f);
                bg2.color = bg1.color * bg1.color * new Color(0.8f, 0.8f, 0.8f);
                txt.color = new Color(0.2f, 0, 0);
                mute1.color = (bg1.color * bg1.color * bg1.color + Color.white * 0.3f) * 0.5f;
                mute2.color = mute1.color;
                info.color = mute1.color;
                break;
            case 3://Gold
                bg1.color = new Color(1f, 0.9f, 0);
                setOtherColors();
                break;
        }
    }

    void setOtherColors() {
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