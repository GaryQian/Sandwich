using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Whitescreen : MonoBehaviour {
    public GameObject portal;
    public GameObject white;
    public static float fadeTime = 2f;
    float fadeIn = 0;
    float hold = 0;
    float fadeOut;
	// Use this for initialization
	void Start () {
        transform.SetParent(Util.wm.canvas.transform);
        transform.localScale = new Vector3(1f, 1f, 1f);
        GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        fadeOut = fadeTime;

        Util.wm.fullAudioSource.PlayOneShot(ResetManager.timeTravel);
	}

    // Update is called once per frame
    void Update() {
        if (fadeIn < fadeTime) {
            fadeIn += Time.deltaTime;
            white.GetComponent<Image>().color = new Color(1f, 1f, 1f, fadeIn / fadeTime);
            portal.GetComponent<Image>().color = new Color(1f, 0, 1f, fadeIn / fadeTime);
            portal.transform.localScale = new Vector3(0.75f + fadeIn / fadeTime, 0.75f + fadeIn / fadeTime, 1f);
        }
        else if (hold < fadeTime) {
            hold += Time.deltaTime;
            portal.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0);
        }
        else if (fadeOut > 0) {
            fadeOut -= Time.deltaTime;
            white.GetComponent<Image>().color = new Color(1f, 1f, 1f, fadeOut / fadeTime);
        }
        else {
            Destroy(gameObject);
        }
    }
}
