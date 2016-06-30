using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {
    public GameObject fingerPrefab;

    WorldManager wm;

    void Awake() {
        wm = GetComponent<WorldManager>();
    }

	// Use this for initialization
	void Start () {
        //Invoke("showFinger", 0.5f);
        if (needFinger()) {
            Invoke("showFinger", 1f);
        }
	}

    private bool needFinger() {
        if (wm.em.totalMoney < 75f || wm.em.sps < 0.4f || (wm.em.gameTime < 200f && wm.em.gameTime / wm.em.totalSwipes > 3f)) {
            return true;
        }
        return false;
    }

    public void showFinger() {
        Instantiate(fingerPrefab);
        if (needFinger()) {
            Invoke("showFinger", 10f);
        }
    }
}
