using UnityEngine;
using System.Collections;

public class SauceTypeText : MonoBehaviour {
    TextMesh txt;
    int sauceNumber;
	// Use this for initialization
	void Awake () {
        txt = GetComponent<TextMesh>();
        
    }

    void Start() {
        txt.transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.03f, Screen.height * 0.01f));
        txt.transform.position = new Vector3(txt.transform.position.x, txt.transform.position.y, -1f);
    }

    void setSauce(int num) {
        sauceNumber = num;
        switch (sauceNumber) {
            case 1: txt.text = "Peanut Butter"; break;
            case 2: txt.text = "Sauce 2"; break;
            case 3: txt.text = "Sauce 3"; break;
        }
    }
}
