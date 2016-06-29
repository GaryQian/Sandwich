using UnityEngine;
using System.Collections;

public class SandwichValueText : MonoBehaviour {
    TextMesh txt;
    WorldManager wm;
    public GameObject tagImage;
    // Use this for initialization
    void Awake() {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        txt = GetComponent<TextMesh>();

    }

    void Start() {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.665f, Screen.height * 0.305f));
        transform.position = new Vector3(txt.transform.position.x, txt.transform.position.y, 0);
        tagImage.transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.65f, Screen.height * 0.32f));
        tagImage.transform.position = new Vector3(tagImage.transform.position.x, tagImage.transform.position.y, 0);
        transform.SetParent(tagImage.transform);
    }

    public void updateValue(double rate) {
        txt.text = "+$" + wm.encodeNumber(rate) + " Ea";
    }
}
