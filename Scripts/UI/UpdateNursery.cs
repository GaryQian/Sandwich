using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UpdateNursery : MonoBehaviour {
    public Text pop;
    public Text val;
    public GameObject bar;
    public BabyManager bm;
    public Text timer;
    public float ratio;

    public GameObject alert;
    public GameObject alertPrefab;

    public delegate void NurseryEvent();
    public static event NurseryEvent SoldBabies;
    // Use this for initialization
    void Awake() {
    }

	void Start () {
	    
	}

    void update() {
        pop.text = Util.encodeNumber(Util.em.nurseryPop) + " Baby Sandwiches";
        val.text = "Worth $" + Util.encodeNumber(Util.em.nurseryPop * Util.em.getSandwichValue() * Util.wm.x3Multiplier * Util.wm.x7Multiplier);
        if (Util.em.nurseryPop > 0) {
            ratio = (float)(Util.em.nurseryPop / Util.em.maxBabyPop);
        }
        else {
            ratio = 0;
        }
        while (bm.babyCount <= bm.maxbabies * ratio) {
            bm.spawnbaby();
        }
        bar.transform.localScale = new Vector3(ratio, 1f, 1f);
        timer.text = Util.encodeTimeShort(Util.maxBabyTime * (1f - ratio)) + " Until Full";

        Util.wm.spawnAlert();
    }


    public void sellBabies() {
        bm.eatBabies();
        double num = Util.em.nurseryPop * Util.em.getSandwichValue();
        Util.em.income(num);
        Util.em.nurseryPop = 0;
        GameObject obj = Instantiate(Util.wm.buttonHandler.canvasNotificationTextPrefab);
        obj.GetComponent<CanvasNotificationText>().setup("+$" + Util.encodeNumber(num * Util.wm.x3Multiplier * Util.wm.x7Multiplier), new Vector3(0, 0, 0), new Color(0, 1f, 0), 120, 100);
        update();

        if (SoldBabies != null) SoldBabies();
    }

    void OnEnable() {
        InvokeRepeating("update", 0, 1f);
    }

    void OnDisable() {
        CancelInvoke("update");
    }
}
