using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum CurrencyType { money, elixir }
public enum BuyType { x1Only, x10 }

public class BuyButton : MonoBehaviour {
    private Upgrade up;
    private Button button;
    ColorBlock enabledColor;
    ColorBlock disabledColor;
    public CurrencyType type;
    public BuyType buyType;
    public GameObject prevButton;
    // Use this for initialization
    void Awake () {
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
	void Update () {
        if (type == CurrencyType.money) {
            if (buyType == BuyType.x10) {
                if (Util.money < up.cost) {
                    //disable
                    button.colors = disabledColor;
                }
                else {
                    //enable
                    button.colors = enabledColor;
                }
            }
            else {
                if (Util.money < up.cost) {
                    //disable
                    button.colors = disabledColor;
                }
                else {
                    //enable
                    button.colors = enabledColor;
                }
            }
        }
        else {
            if (Util.em.elixir < up.cost) {
                //disable
                button.colors = disabledColor;
            }
            else {
                //enable
                button.colors = enabledColor;
            }
        }
	}
}
