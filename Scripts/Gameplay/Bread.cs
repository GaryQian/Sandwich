using UnityEngine;
using System.Collections;

public class Bread : MonoBehaviour {
    private GameplayTouchManager gtm;
    private Vector3 enterPoint = Vector3.zero;
    public bool inPlace;
    public bool finished;
    public bool spreading;

    private WorldManager wm;
    // Use this for initialization
    void Awake () {
	    transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.7f, Screen.height * 0.15f)) + new Vector3(0, 0, 10f);
        gtm = GameObject.Find("WorldManager").GetComponent<GameplayTouchManager>();
        finished = false;
        inPlace = true;
        spreading = false;

        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (spreading) {
            Vector3 offset = new Vector3(0, (wm.activeBread.transform.position.y - gtm.knife.transform.position.y) * 0.6f + 0.1f);
            gtm.knife.transform.FindChild("Trail").transform.position = gtm.knife.transform.position + offset;
        }
	}

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.GetComponent<Knife>() != null && inPlace) {
            enterPoint = coll.gameObject.transform.position;
            spreading = true;
            finished = false;
            Vector3 offset = new Vector3(0, (wm.activeBread.transform.position.y - enterPoint.y) * 0.6f + 0.1f);
            gtm.knife.transform.FindChild("Trail").transform.Translate(offset);
            gtm.knife.transform.FindChild("Trail").GetComponent<TrailManager>().trail.enabled = true;
            gtm.knife.transform.FindChild("Trail").GetComponent<TrailManager>().trail.Clear();
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        if (coll.gameObject.GetComponent<Knife>() != null && inPlace) {
            if (Vector3.Distance(enterPoint, coll.gameObject.transform.position) > (GetComponent<BoxCollider2D>().size.x * 0.8f * transform.localScale.x)) {
                finished = true;
            }
            stopSpreading();
        }
    }

    public void stopSpreading() {
        gtm.knife.transform.FindChild("Trail").transform.SetParent(this.transform);
        gtm.knife.GetComponent<Knife>().newTrail();
        spreading = false;
    }

    void OnDestroy() {
        //gtm.knife.GetComponent<TrailManager>().trail.enabled = false;
    }
}
