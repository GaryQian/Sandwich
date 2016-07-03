using UnityEngine;
using System.Collections;

public class SandWitch : MonoBehaviour {
    bool isLeft = false;
    float flyInTime = 0.9f;
    float hoverTime = 7f;
    float flyIn;
    float flyOut;
    float flyHover;
    Vector2 startPos;
    Vector2 hoverPos;
    Vector2 offset;
    RectTransform rect;

    float startX = 1000f;
    float startY = 400f;
    float hoverIntensity = 50f;

    float correctionScale = 1.85f;

    void Awake() {
        rect = GetComponent<RectTransform>();
        
    }
	// Use this for initialization
	void Start () {
        transform.SetParent(Util.wm.canvas.transform);
        flyIn = 0;
        flyOut = 0;
        flyHover = 0;
        if (Random.Range(0, 1.9999f) < 1f) {
            isLeft = true;
        }
        hoverPos = new Vector3(Random.Range(-300f, 300f), Random.Range(-400f, 800f));
        if (!isLeft) {
            rect.localScale = new Vector3(-1f, 1f, 1f);
            startPos = hoverPos + new Vector2(startX, -startY);
            offset = new Vector2(-startX / flyInTime, startY / flyInTime) * correctionScale;
        }
        else {
            //GetComponent<RectTransform>().localScale = new Vector3(-1f, 1f, 1f);
            startPos = hoverPos + new Vector2(-startX, -startY);
            offset = new Vector2(startX / flyInTime, startY / flyInTime) * correctionScale;
        }

        rect.anchoredPosition = startPos;
	}

    // Update is called once per frame
    void Update() {
        rect.anchoredPosition = rect.anchoredPosition + new Vector2(0, Mathf.Sin((float)Util.em.gameTime) * Time.deltaTime * hoverIntensity);
        if (flyIn < flyInTime) {
            rect.anchoredPosition = rect.anchoredPosition + offset * Time.deltaTime * ((flyInTime - flyIn) / flyInTime);
            flyIn += Time.deltaTime;
        }
        else if (flyHover < hoverTime) {
            flyHover += Time.deltaTime;

        }
        else if (flyOut < flyInTime) {
            rect.anchoredPosition = rect.anchoredPosition + offset * Time.deltaTime * (flyOut / flyInTime);
            flyOut += Time.deltaTime;
        }
        else {
            Destroy(gameObject);
        }
	}
}
