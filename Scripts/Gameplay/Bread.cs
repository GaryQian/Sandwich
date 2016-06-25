using UnityEngine;
using System.Collections;

public class Bread : MonoBehaviour {
    private GameplayTouchManager gtm;
    private Vector3 enterPoint = Vector3.zero;
    public bool inPlace;
    public bool finished;
    public bool spreading;

    public GameObject BreadTopPrefab;
    public GameObject BreadPrefab;
    private GameObject top;

    private WorldManager wm;
    // Use this for initialization
    void Awake () {
	    transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.73f, Screen.height * 0.15f)) + new Vector3(0, 0, 10f);
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
        if (coll.gameObject.GetComponent<Knife>() != null && coll.gameObject.GetComponent<Knife>().hasSauce && inPlace) {
            enterPoint = coll.gameObject.transform.position;
            spreading = true;
            Vector3 offset = new Vector3(0, (wm.activeBread.transform.position.y - enterPoint.y) * 0.6f + 0.1f);
            gtm.knife.transform.FindChild("Trail").transform.Translate(offset);
            gtm.knife.transform.FindChild("Trail").GetComponent<TrailManager>().trail.enabled = true;
            gtm.knife.transform.FindChild("Trail").GetComponent<TrailManager>().trail.Clear();
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        if (coll.gameObject.GetComponent<Knife>() != null && inPlace) {
            if (Vector3.Distance(enterPoint, coll.gameObject.transform.position) > (GetComponent<BoxCollider2D>().size.x * 0.8f * transform.localScale.x) && !finished) {
                finished = true;
                spreading = false;
                wm.em.swipe();
                Debug.LogError("Swping from bread");
            }
            stopSpreading();
            
        }
    }

    public void stopSpreading() {
        //trails.Add(gtm.knife.transform.FindChild("Trail"));
        gtm.knife.transform.FindChild("Trail").transform.SetParent(this.transform);
        gtm.knife.GetComponent<Knife>().newTrail();
        spreading = false;
    }

    public void finish() {
        top = (GameObject)Instantiate(BreadTopPrefab, transform.position + new Vector3(Random.Range(-0.1f, 0.1f), 0.5f + 4f + Random.Range(-0.1f, 0.1f), 0), Quaternion.identity);
        Destroy(gameObject, 0.5f);
        wm.activeBread = Instantiate(BreadPrefab);
        Invoke("deleteTrails", 0.15f);
    }

    void deleteTrails() {
        GameObject trail = transform.FindChild("Trail").gameObject;
        while (trail != null) {
            Destroy(trail);
            trail = transform.FindChild("Trail").gameObject;
        }
    }

    void OnDestroy() {
        //gtm.knife.GetComponent<TrailManager>().trail.enabled = false;
    }
}
