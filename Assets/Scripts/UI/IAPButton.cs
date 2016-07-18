using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum IAPBuyButtonType { knife, boost }

public class IAPButton : MonoBehaviour {
    
    private Button button;
    public IAPBuyButtonType type;
    ColorBlock enabledColor;
    ColorBlock disabledColor;
    WorldManager wm;
    // Use this for initialization
    void Awake() {
        wm = Util.wm;
        //up = transform.parent.GetComponent<Upgrade>();
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
        if (Util.even) {
            if (type == IAPBuyButtonType.knife) {
                if (wm.knifeCollectionPurchased) {
                    //disable
                    button.colors = disabledColor;
                }
                else {
                    //enable
                    button.colors = enabledColor;
                }
            }
            else {
                if (wm.x3Time > 0 || wm.x7Time > 0) {
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
}