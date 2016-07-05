using UnityEngine;
using System.Collections;

public class Knife : MonoBehaviour {
    public Sprite knife;
    public Sprite k1;
    public Sprite k2;
    public Sprite k3;
    public Sprite k4;
    public Sprite k5;
    public Sprite k6;
    public Sprite k7;
    public Sprite k8;
    public Sprite k9;
    public Sprite k10;

    public GameObject trailPrefab;
    public bool hasSauce;
    public GameObject trail;
    // Use this for initialization
    void Awake() {
        newTrail();
        hasSauce = false;
    }

    void Start() {
        setupKnifeType();
    }

    public void setupKnifeType() {
        switch (Util.wm.knifeID) {
            case 0: GetComponent<SpriteRenderer>().sprite = knife; break;
            case 1: GetComponent<SpriteRenderer>().sprite = k1; break;
            case 2: GetComponent<SpriteRenderer>().sprite = k2; break;
            case 3: GetComponent<SpriteRenderer>().sprite = k3; break;
            case 4: GetComponent<SpriteRenderer>().sprite = k4; break;
            case 5: GetComponent<SpriteRenderer>().sprite = k5; break;
            case 6: GetComponent<SpriteRenderer>().sprite = k6; break;
            case 7: GetComponent<SpriteRenderer>().sprite = k7; break;
            case 8: GetComponent<SpriteRenderer>().sprite = k8; break;
            case 9: GetComponent<SpriteRenderer>().sprite = k9; break;
            case 10: GetComponent<SpriteRenderer>().sprite = k10; break;
        }
    }

    public void newTrail() {
        trail = Instantiate(trailPrefab);
        trail.transform.position = transform.position + new Vector3(0, 0, -1f);
        trail.transform.SetParent(this.transform);
        trail.name = "Trail";
    }

    public void deleteTrails() {
        while (transform.childCount > 0) {
            Transform trail = transform.FindChild("Trail");
            if (trail != null) GameObject.DestroyImmediate(trail.gameObject);
        }
    }
}
