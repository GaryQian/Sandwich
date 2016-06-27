using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour {
    private Upgrade up;
    private Button button;
    ColorBlock enabledColor;
    ColorBlock disabledColor;
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
