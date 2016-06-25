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

    private float introTime = 0.12f;
    private float introTimer;
    private float waitTime = 0.05f;
    private float waitTimer = 0;
    private Vector3 finalPos;
    SpriteRenderer sr;

    private WorldManager wm;
    // Use this for initialization
    void Awake () {
        finalPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.73f, Screen.height * 0.15f)) + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.1f, 0.1f), 10f);
        transform.position = finalPos + new Vector3(0, -2.7f);
        
        finished = false;
        inPlace = false;
        spreading = false;
        introTimer = 0;
        gtm = GameObject.Find("WorldManager").GetComponent<GameplayTouchManager>();
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start() {
        deleteTrails();
    }
	
	// Update is called once per frame
	void Update () {
        if (!inPlace) {
            if (waitTimer <= waitTime) {
                waitTimer += Time.deltaTime;
                sr.color = new Color(1f, 1f, 1f, 0);
            }
            if (introTimer <= introTime) {
                introTimer += Time.deltaTime;
                transform.Translate(new Vector3(0, 2.7f / introTime * Time.deltaTime));
                sr.color = new Color(1f, 1f, 1f, introTimer / introTime);
            }
            else {
                inPlace = true;
                transform.position = finalPos;
            }
        }
        else if (spreading) {
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
            if (Vector3.Distance(enterPoint, coll.gameObject.transform.position) > (GetComponent<BoxCollider2D>().size.x * 0.8f * transform.localScale.x) && !finished && spreading) {
                finished = true;
                spreading = false;
                wm.em.swipe();
                stopSpreading();
            }
            else {
                resetTrail();
            }
            
        }
    }

    public void stopSpreading() {
        //trails.Add(gtm.knife.transform.FindChild("Trail"));
        if (inPlace) {
            gtm.knife.transform.FindChild("Trail").transform.SetParent(transform);
            gtm.knife.GetComponent<Knife>().trail = null;
            gtm.knife.GetComponent<Knife>().newTrail();
            spreading = false;
        }
    }

    public void resetTrail() {
        Destroy(gtm.knife.GetComponent<Knife>().trail);
        gtm.knife.GetComponent<Knife>().trail = null;
        gtm.knife.GetComponent<Knife>().newTrail();
        spreading = false;
    }

    public void finish() {
        top = (GameObject)Instantiate(BreadTopPrefab, transform.position + new Vector3(Random.Range(-0.15f, 0.15f), 0.5f + 4f, -6f), Quaternion.identity);
        top.GetComponent<BreadTop>().bread = gameObject;
        Invoke("delete", 0.5f);
        wm.activeBread = Instantiate(BreadPrefab);
        Invoke("deleteTrails", 0.08f);
    }

    void deleteTrails() {
        while (transform.childCount > 0) {
            Transform trail = transform.FindChild("Trail");
            if (trail != null) GameObject.DestroyImmediate(trail.gameObject);
        }
        //Debug.LogError("Deleteing trails");
    }

    void delete() {
        DestroyImmediate(gameObject);
    }
}
