using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {
    public GameObject fingerPrefab;
    public GameObject yellowArrow;

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

        if (Util.em.sandwichCartCount > 0) {
            removeYellowArrow();
        }
        else {
            Invoke("checkYellowArrow", 0.5f);
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

    void checkYellowArrow() {
        if (yellowArrow != null && Util.money >= 50f && Util.em.sandwichCartCount == 0) {
            yellowArrow.GetComponent<Animator>().SetTrigger("Pulse");
        }
        else if (yellowArrow == null) {

        }
        else {
            Invoke("checkYellowArrow", 0.5f);
        }
    }

    public void removeYellowArrow() {
        if (yellowArrow != null) {
            Destroy(yellowArrow);
        }
    }
}
