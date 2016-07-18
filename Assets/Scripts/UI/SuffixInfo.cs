using UnityEngine;
using System.Collections;

public class SuffixInfo : MonoBehaviour {

    void Awake() {
        transform.SetParent(Util.wm.canvas.transform);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        transform.localScale = new Vector3(1f, 1f, 1f);
        transform.SetAsLastSibling();
    }

    public void close() {
        Destroy(gameObject);
    }
}
