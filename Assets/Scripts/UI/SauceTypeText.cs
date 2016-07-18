using UnityEngine;
using System.Collections;

public class SauceTypeText : MonoBehaviour {
    public TextMesh txt;
    int sauceNumber;
	// Use this for initialization
	void Awake () {
        txt = GetComponent<TextMesh>();
        
    }

    void Start() {
        txt.transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.03f, Screen.height * 0.01f));
        txt.transform.position = new Vector3(txt.transform.position.x, txt.transform.position.y, -1f);
    }

}
