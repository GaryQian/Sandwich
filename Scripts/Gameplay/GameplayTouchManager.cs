using UnityEngine;
using System.Collections;

public class GameplayTouchManager : MonoBehaviour {
    public GameObject knifePrefab;
    public GameObject knife;

    private EconomyManager em;
    private WorldManager wm;

    private Vector3 downLoc;
    private Vector3 upLoc;

    private Vector3 prevPos;
	// Use this for initialization
	void Awake () {
        knife = (GameObject)Instantiate(knifePrefab, new Vector3(-4f, -6f, 5f), Quaternion.identity);
        em = GetComponent<EconomyManager>();
        wm = GetComponent<WorldManager>();
        //knife.transform.position = new Vector3(100f, 100f);
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.isMobilePlatform) {
            if (Input.touchCount > 0) {
                processTouch(0);
            }
        }
        else {
            processClick();
        }
	}

    void processTouch(int i) {
        Touch touch = Input.GetTouch(i);
        if (touch.phase == TouchPhase.Moved) {
            knife.transform.position = Camera.main.ScreenToWorldPoint(touch.position) + new Vector3(0, 0, 5f);
            knife.transform.eulerAngles = knife.transform.position - prevPos;
        }
        else if (touch.phase == TouchPhase.Began) {
            knife.transform.position = Camera.main.ScreenToWorldPoint(touch.position) + new Vector3(0, 0, 5f);
            prevPos = knife.transform.position;
            knife.transform.eulerAngles = Vector3.zero;
        }
        else if (touch.phase == TouchPhase.Ended) {
            upLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 5f);
            if (Vector3.Distance(upLoc, wm.activeBread.transform.position) < wm.activeBread.GetComponent<BoxCollider2D>().size.x * 0.4) {
                if (wm.activeBread.GetComponent<Bread>().finished) em.swipe();
            }
        }
    }

    void processClick() {
        if (Input.GetMouseButton(0)) {
            knife.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 5f);
        }
        if (Input.GetMouseButtonDown(0)) {
            downLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 5f);
        }
        if (Input.GetMouseButtonUp(0)) {
            upLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 5f);
            if (Vector3.Distance(upLoc, wm.activeBread.transform.position) < wm.activeBread.GetComponent<BoxCollider2D>().size.x * 0.4) {
                if (wm.activeBread.GetComponent<Bread>().finished) em.swipe();
            }
        }
    }
}
