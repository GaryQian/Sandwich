using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BreadTypeText : MonoBehaviour {

    public TextMesh txt;
    // Use this for initialization
    void Awake() {
        txt = GetComponent<TextMesh>();

    }

    void Start() {
        txt.transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.975f, Screen.height * 0.01f));
        txt.transform.position = new Vector3(txt.transform.position.x, txt.transform.position.y, -1f);
    }

}