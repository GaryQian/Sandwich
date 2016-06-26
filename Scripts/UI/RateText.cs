/*using UnityEngine;
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
*/
using UnityEngine;
using System.Collections;

public class RateText : MonoBehaviour {
    TextMesh txt;
    WorldManager wm;
    public GameObject tagImage;
    // Use this for initialization
    void Awake() {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        txt = GetComponent<TextMesh>();

    }

    void Start() {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.645f, Screen.height * 0.332f));
        transform.position = new Vector3(txt.transform.position.x, txt.transform.position.y, 0);
        tagImage.transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.65f, Screen.height * 0.32f));
        tagImage.transform.position = new Vector3(tagImage.transform.position.x, tagImage.transform.position.y, 0);
    }

    public void updateRate(double rate) {
        txt.text = "+$" + wm.encodeNumber(rate) + " /s";
    }
}