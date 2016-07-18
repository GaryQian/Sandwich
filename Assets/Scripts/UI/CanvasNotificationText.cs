using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasNotificationText : MonoBehaviour {

    Text txt;
    public float life;
    float timer;
    public float riseRate;
    private float initOpacity = 1f;
    RectTransform rect;
    // Use this for initialization
    void Awake() {
        txt = GetComponent<Text>();
        timer = life;
        rect = GetComponent<RectTransform>();
    }

    void Start() {
        transform.SetAsLastSibling();
        rect.localScale = new Vector3(1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime;
        rect.anchoredPosition = rect.anchoredPosition + new Vector2(0, riseRate * Time.deltaTime);
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, initOpacity * (timer / life));
        if (timer < 0) {
            Destroy(gameObject);
        }
    }

    public void setup(string str, Vector2 pos, Color c, int fontSize, float rr) {
        txt = GetComponent<Text>();
        txt.text = str;
        txt.color = c;
        txt.fontSize = fontSize;
        riseRate = rr;
        transform.SetParent(Util.wm.canvas.transform);
        rect.anchoredPosition = pos;
    }

    public void setup(string str, Vector3 pos) {
        setup(str, pos, new Color(1f, 1f, 1f), (int)(Screen.height * 0.03f), 1f);
    }
}