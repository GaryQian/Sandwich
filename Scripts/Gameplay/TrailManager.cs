using UnityEngine;
using System.Collections;

public class TrailManager : MonoBehaviour {
    public Material peanutButter;
    public Material strawberryJam;
    public Material tearsOfDespair;
    public Material nuhtelluh;
    public Material creamCheese;
    public Material hamburger;
    public Material garlic;
    public Material guac;
    public Material ham;
    public Material butter;
    public Material nails;
    public Material camo;
    public Material ratPoison;
    public Material bacon;
    public Material gold;


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
    Material getMaterial(int i) {
        switch (((i - 1) % Sauce.numberOfSauces) + 1) {
            case 1: return peanutButter;
            case 2: return strawberryJam;
            case 3: return tearsOfDespair;
            case 4: return nuhtelluh;
            case 5: return creamCheese;
            case 6: return hamburger;
            case 7: return garlic;
            case 8: return guac;
            case 9: return ham;
            case 10: return butter;
            case 11: return nails;
            case 12: return camo;
            case 13: return ratPoison;
            case 14: return bacon;
            case 15: return gold;
        }
        return peanutButter;
    }
}
