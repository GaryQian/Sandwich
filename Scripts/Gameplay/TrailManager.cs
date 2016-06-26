using UnityEngine;
using System.Collections;

public class TrailManager : MonoBehaviour {
    public Material peanutButter;
    public Material strawberryJam;
    public Material tearsOfDespair;
    public TrailRenderer trail;
    // Use this for initialization
    void Awake () {
        trail = GetComponent<TrailRenderer>();
        trail.sortingLayerName = "Top";
        trail.sortingOrder = 0;
        trail.transform.position = trail.transform.position + new Vector3(0, 0, 0);
    }

    void Start() {
        trail.material = getMaterial(GameObject.Find("WorldManager").GetComponent<EconomyManager>().sauceID);
    }

    Material getMaterial(int id) {
        switch (id) {
            case 1: return peanutButter;
            case 2: return strawberryJam;
            case 3: return tearsOfDespair;
            case 4: return peanutButter;
            case 5: return peanutButter;
        }
        return peanutButter;
    }
}
