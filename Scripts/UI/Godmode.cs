using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Godmode : MonoBehaviour {
    public int godCounter = 0;
    public Text txt;
    TouchScreenKeyboard kb;
    public int godmodeCode;
	// Use this for initialization
	void Start () {
        InvokeRepeating("countDown", 2f, 2f);
        godmodeCode = 1884297456;
    }

    void countDown() {
        if (godCounter >= 1) {
            godCounter--;
        }
    }

    public void click() {
        godCounter++;
        if (godCounter >= 10 && kb == null) {
            kb = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true);
        }
    }

    void Update() {
        if (kb != null) {
            if (kb.text.GetHashCode() == godmodeCode) {
                Util.godmode = true;
            }
        }
    }
}
