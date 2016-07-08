using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Switch : MonoBehaviour {
    public Sprite left;
    public Sprite right;
    public Image img;
    public Sprite x1Sprite;
    public Sprite x10Sprite;

    public Image buy1Img;
    public Image buy10Img;

    public delegate void BuyCountChange();
    public static event BuyCountChange Toggled;
	// Use this for initialization
	void Start () {
        setDarkness();
	}

    public void toggle() {
        if (ButtonHandler.buyCount == 1) {
            setx10();
        }
        else {
            setx1();
        }
    }

    public void setx10() {
        ButtonHandler.buyCount = 10;
        img.sprite = right;
        if (Toggled != null) Toggled();
        Util.wm.tabManager.setBuyButtonSprite(x10Sprite);
        Util.buyNumberScale = Util.x10BuyCostScale;
        Util.em.updateProducerMenuCounters();
    }
    public void setx1() {
        ButtonHandler.buyCount = 1;
        img.sprite = left;
        if (Toggled != null) Toggled();
        Util.wm.tabManager.setBuyButtonSprite(x1Sprite);
        Util.buyNumberScale = 1f;
        Util.em.updateProducerMenuCounters();
    }

    void setDarkness() {
        if (ButtonHandler.buyCount == 1) {
            buy1Img.color = new Color(0.5f, 0.5f, 0.5f);
            buy10Img.color = new Color(0, 0, 0, 0.5f);
            img.sprite = left;
        }
        else {
            buy10Img.color = new Color(0.5f, 0.5f, 0.5f);
            buy1Img.color = new Color(0, 0, 0, 0.5f);
            img.sprite = right;
        }
    }

    void OnEnable() {
        Switch.Toggled += setDarkness;
    }
    void OnDisable() {
        Switch.Toggled -= setDarkness;
    }
}
