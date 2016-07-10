using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UpdateNursery : MonoBehaviour {
    public Text pop;
    public Text val;

    public BabyManager bm;

    public float maxBabyTime;
    // Use this for initialization
    void Awake() {
    }

	void Start () {
	    
	}

    void update() {
        pop.text = Util.encodeNumber(Util.em.nurseryPop) + " Baby Sandwiches";
        val.text = "Worth $" + Util.encodeNumber(Util.em.nurseryPop * Util.em.getSandwichValue());
        while (bm.babyCount <= bm.maxbabies * (Util.em.nurseryPop / Util.em.maxBabyPop)) {
            bm.spawnbaby();
        }
    }

    public void sellBabies() {
        bm.eatBabies();
        double num = Util.em.nurseryPop * Util.em.getSandwichValue();
        Util.em.income(num);
        Util.em.nurseryPop = 0;
        GameObject obj = Instantiate(Util.wm.buttonHandler.canvasNotificationTextPrefab);
        obj.GetComponent<CanvasNotificationText>().setup("+$" + Util.encodeNumber(num), new Vector3(0, 0, 0), new Color(0, 1f, 0), 120, 100);
        update();
    }

    void OnEnable() {
        InvokeRepeating("update", 0, 1f);
    }

    void OnDisable() {
        CancelInvoke("update");
    }
}
