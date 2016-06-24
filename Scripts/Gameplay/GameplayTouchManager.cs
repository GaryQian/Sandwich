using UnityEngine;
using System.Collections;

public class GameplayTouchManager : MonoBehaviour {
    public GameObject knifePrefab;
    public GameObject knife;
	// Use this for initialization
	void Awake () {
        knife = Instantiate(knifePrefab);
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
        }
        else if (touch.phase == TouchPhase.Ended) {
            //knife.transform.position = new Vector3(100f, 100f);
        }
    }

    void processClick() {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
            knife.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10f);
        }
        else {
            //knife.transform.position = new Vector3(100f, 100f);
        }
    }
}
