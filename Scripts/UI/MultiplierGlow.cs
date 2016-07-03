using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MultiplierGlow : MonoBehaviour {
    float fadeSpeed = 1f;
    float timer;
    Image img;
    void Awake() {
        img = GetComponent<Image>();
    }
	// Use this for initialization
	void Start () {
        GetComponent<RectTransform>().offsetMax = new Vector2(1920f / Screen.height * Screen.width / 2f, 1920f / 2f);
        GetComponent<RectTransform>().offsetMin = new Vector2(1920f / Screen.height * Screen.width / -2f, 1920f / -2f);

        img.color = new Color(1f, 1f, 0, 0);
    }

    public void show() {
        timer = fadeSpeed;
        img.color = new Color(1f, 1f, 0, 1f);
        CancelInvoke("fade");
        Invoke("fade", 0.7f);
    }

    public void fade() {
        timer -= 0.1f;
        if (timer >= 0) {
            img.color = new Color(1f, 1f, 0, 1f * (timer / fadeSpeed));
            Invoke("fade", 0.1f);
        }
        else {
            img.color = new Color(1f, 1f, 0, 0);
        }
    }
}
