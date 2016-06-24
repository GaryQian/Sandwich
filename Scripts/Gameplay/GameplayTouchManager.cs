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
        knife = Instantiate(knifePrefab);
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
        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary) {
            knife.transform.position = Camera.main.ScreenToWorldPoint(touch.position) + new Vector3(0, 0, 10f);
            knife.transform.eulerAngles = knife.transform.position - prevPos;
        }
        else if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary) {
            knife.transform.position = Camera.main.ScreenToWorldPoint(touch.position) + new Vector3(0, 0, 10f);
            prevPos = knife.transform.position;
            knife.transform.eulerAngles = Vector3.zero;
        }
        else if (touch.phase == TouchPhase.Ended) {
            //knife.transform.position = new Vector3(100f, 100f);
        }
    }

    void processClick() {
        if (Input.GetMouseButton(0)) {
            knife.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10f);
        }
        if (Input.GetMouseButtonDown(0)) {
            downLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10f);
        }
        if (Input.GetMouseButtonUp(0)) {
            upLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10f);
            if (Vector3.Distance(upLoc, wm.activeBread.transform.position) < 2) {
                em.swipe();
            }
        }
    }
}
