using UnityEngine;
using System.Collections;

public class Bread : MonoBehaviour {
    public Sprite bread;
    public Sprite wheat;
    public Sprite potato;
    public Sprite rye;
    public Sprite banana;
    public Sprite corn;
    public Sprite flat;


    private GameplayTouchManager gtm;
    private Vector3 enterPoint = Vector3.zero;
    public bool inPlace;
    public bool finished;
    public bool spreading;
    public bool failedSpread;

    public GameObject BreadTopPrefab;
    public GameObject BreadPrefab;
    private GameObject top;

    private float introTime = 0.12f;
    private float introTimer;
    private float waitTime = 0.05f;
    private float waitTimer = 0;
    public Vector3 finalPos;
    SpriteRenderer sr;

    public AudioSource audio;
    public AudioClip s1;
    public AudioClip s2;
    public AudioClip s3;
    public AudioClip s4;
    public AudioClip s5;
    public AudioClip s6;

    private WorldManager wm;
    // Use this for initialization
    void Awake () {
        finalPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.50f, Screen.height * 0.15f)) + new Vector3(Util.worldNormalizedWidth * +0.23f + Random.Range(-0.2f, 0.2f), Random.Range(-0.1f, 0.1f), 10f);
        //finalPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.73f, Screen.height * 0.15f)) + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.1f, 0.1f), 10f);
        transform.position = finalPos + new Vector3(0, -2.7f);
        
        finished = false;
        inPlace = false;
        spreading = false;
        failedSpread = false;
        introTimer = 0;
        gtm = Util.wm.gtm;
        wm = Util.wm;
        sr = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
    }

    void Start() {
        deleteTrails();
        sr.sprite = getBreadSprite();
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
                if (transform.position.y > finalPos.y) {
                    transform.position = finalPos;
                }
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
                failedSpread = true;
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
        top.GetComponent<SpriteRenderer>().sprite = getBreadSprite();
        Invoke("delete", 0.5f);
        wm.activeBread = Instantiate(BreadPrefab);
        wm.activeBread.name = "Bread";
        Invoke("deleteTrails", 0.08f);

        //play swish
        if (!Util.muted) {
            switch ((int)Random.Range(0, 5.99f)) {
                case 0: audio.PlayOneShot(s1, 0.3f); break;
                case 1: audio.PlayOneShot(s2, 0.3f); break;
                case 2: audio.PlayOneShot(s3, 0.3f); break;
                case 3: audio.PlayOneShot(s4, 0.3f); break;
                case 4: audio.PlayOneShot(s5, 0.3f); break;
                case 5: audio.PlayOneShot(s6, 0.3f); break;
            }
        }
    }

    void deleteTrails() {
        while (transform.childCount > 0) {
            Transform trail = transform.FindChild("Trail");
            if (trail != null) GameObject.DestroyImmediate(trail.gameObject);
        }
        //Debug.LogError("Deleteing trails");
    }


    public Sprite getBreadSprite() {
        return getBreadSprite(Util.em.breadID);
    }
    public Sprite getBreadSprite(int i) {
        switch (i) {
            case 0: return bread; break;
            case 1: return wheat; break;
            case 2: return potato; break;
            case 3: return rye; break;
            case 4: return banana; break;
            case 5: return corn; break;
            case 6: return flat; break;
            case 7: return bread; break;
        }
        return bread;
    }


    public string getBreadName() {
        return getBreadName(Util.em.breadID);
    }
    public string getBreadName(int i) {
        switch (i) {
            case 0: return "White Bread"; break;
            case 1: return "Wheat Bread"; break;
            case 2: return "Potato Bread"; break;
            case 3: return "Rye Bread"; break;
            case 4: return "Banana Bread"; break;
            case 5: return "Cornbread"; break;
            case 6: return "Flatbread"; break;
            case 7: return "Bread"; break;
        }
        return "Bread";
    }

    public static double cost(int i) {
        return Util.breadBaseCost * System.Math.Pow(Util.breadScale, i);
    }

    public static void updateButton() {
        Upgrade up = GameObject.Find("BreadUpgrade").GetComponent<Upgrade>();
        up.updateIcon(Util.wm.activeBread.GetComponent<Bread>().getBreadSprite(Util.em.breadID + 1));
        up.updateName(Util.wm.activeBread.GetComponent<Bread>().getBreadName(Util.em.breadID + 1));
        up.updateCost(Bread.cost(Util.em.breadID));
        up.updateStats("x2 ea &");
    }

    void delete() {
        DestroyImmediate(gameObject);
    }
}
