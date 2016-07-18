using UnityEngine;
using System.Collections;

public class GameplayTouchManager : MonoBehaviour {
    public GameObject knifePrefab;
    public GameObject knife;

    private EconomyManager em;
    private WorldManager wm;

    private Vector3 downLoc;
    private float downTime;
    private Vector3 upLoc;

	// Use this for initialization
	void Awake () {
        knife = (GameObject)Instantiate(knifePrefab, new Vector3(-4f, -6f, 5f), Quaternion.identity);
        em = GetComponent<EconomyManager>();
        wm = GetComponent<WorldManager>();
        downTime = 0;
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
            if (wm.activeBread.GetComponent<Bread>().spreading && knife.transform.position.x - wm.activeBread.transform.position.x > 2.3f) {
                wm.activeBread.GetComponent<Bread>().stopSpreading();
            }
            knife.transform.position = Camera.main.ScreenToWorldPoint(touch.position) + new Vector3(0, 0, 4f);
            
        }
        else if (touch.phase == TouchPhase.Began) {
            knife.GetComponent<Knife>().deleteTrails();
            knife.GetComponent<Knife>().newTrail();
            if (wm.activeBread.GetComponent<Bread>().spreading) {
                wm.activeBread.GetComponent<Bread>().stopSpreading();
                
            }
            else {
                knife.transform.position = Camera.main.ScreenToWorldPoint(touch.position) + new Vector3(0, 0, 4f);
                knife.transform.eulerAngles = Vector3.zero;
            }
            downLoc = Camera.main.ScreenToWorldPoint(touch.position) + new Vector3(0, 0, 4f);
            downTime = Time.time;
        }
        else if (touch.phase == TouchPhase.Ended) {
            wm.activeBread.GetComponent<Bread>().stopSpreading();
            upLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 4f);
            //Check if shorthand swipe validation is fulfilled
            bool quickswipe = ((Vector3.Distance(downLoc, wm.sauce.transform.position) < wm.sauce.GetComponent<BoxCollider2D>().size.x / 2f || knife.GetComponent<Knife>().hasSauce) && Time.time - downTime < 0.3f && upLoc.x > wm.activeBread.transform.position.x) && Vector3.Distance(downLoc, upLoc) > 0.3f && upLoc.y < wm.activeBread.transform.position.y + 9f;
            
            //
            if (quickswipe) em.swipe();
            downTime = 0;
            
            
        }
    }

    void processClick() {
        if (Input.GetMouseButton(0)) {
            knife.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 4f);
        }
        if (Input.GetMouseButtonDown(0)) {
            downLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 4f);
            downTime = Time.time;
        }
        if (Input.GetMouseButtonUp(0)) {
            upLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 4f);
            bool quickswipe = ((Vector3.Distance(downLoc, wm.sauce.transform.position) < wm.sauce.GetComponent<BoxCollider2D>().size.x / 2f || knife.GetComponent<Knife>().hasSauce) && Time.time - downTime < 0.25f && upLoc.x > wm.activeBread.transform.position.x);
            if (quickswipe) em.swipe();
        }
    }
}
