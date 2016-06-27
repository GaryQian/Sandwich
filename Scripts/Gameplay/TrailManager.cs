using UnityEngine;
using System.Collections;

public class TrailManager : MonoBehaviour {
    public Material peanutButter;
    public Material strawberryJam;
    public Material tearsOfDespair;
    public Material nuhtelluh;
    public Material creamCheese;


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

    //ALSO ADD TO Sauce.cs
    Material getMaterial(int id) {
        switch (id) {
            case 1: return peanutButter;
            case 2: return strawberryJam;
            case 3: return tearsOfDespair;
            case 4: return nuhtelluh;
            case 5: return creamCheese;
        }
        return peanutButter;
    }
}
