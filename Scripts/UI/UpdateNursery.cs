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

    public AudioClip sellSound;

    void update() {
            pop.text = Util.encodeNumber(Util.em.nurseryPop) + " Baby Sandwiches";
            val.text = "Worth $" + Util.encodeNumber(Util.em.nurseryPop * Util.em.getSandwichValue() * Util.wm.x2Multiplier * Util.wm.x3Multiplier * Util.wm.x7Multiplier);
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
        if (Util.em.nurseryPop > 0.01f) {
            GameObject obj = Instantiate(Util.wm.buttonHandler.canvasNotificationTextPrefab);
            obj.GetComponent<CanvasNotificationText>().setup("+$" + Util.encodeNumber(num * Util.wm.x2Multiplier * Util.wm.x3Multiplier * Util.wm.x7Multiplier), new Vector3(0, 0, 0), new Color(0, 1f, 0), 120, 100);
            if (!Util.wm.muted) Util.wm.fullAudioSource.PlayOneShot(sellSound);
        }
        else {
            GameObject obj = Instantiate(Util.wm.buttonHandler.canvasNotificationTextPrefab);
            obj.GetComponent<CanvasNotificationText>().setup("Empty!", new Vector3(0, 0, 0), new Color(0.9f, 1f, 0), 120, 100);
        }
        Util.em.nurseryPop = 0;
        update();
        if (SoldBabies != null) SoldBabies();
    }

    void OnEnable() {
        InvokeRepeating("update", 0, 1.1f);
    }

    void OnDisable() {
        CancelInvoke("update");
    }
}
