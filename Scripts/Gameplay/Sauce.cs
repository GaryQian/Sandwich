using UnityEngine;
using System.Collections;

public class Sauce : MonoBehaviour {
    public static int numberOfSauces = 26;

    public Sprite peanutButter;
    public Sprite strawberryJam;
    public Sprite tearsOfDespair;
    public Sprite nuhtelluh;
    public Sprite creamCheese;
    public Sprite hamburger;
    public Sprite garlic;
    public Sprite guac;
    public Sprite ham;
    public Sprite butter;
    public Sprite nails;
    public Sprite sushi;
    public Sprite ratPoison;
    public Sprite bacon;
    public Sprite gold;
    public Sprite sewage;
    public Sprite lava;
    public Sprite spum;
    public Sprite rawEggs;
    public Sprite gunpowder;
    public Sprite tnt;
    public Sprite acid;
    public Sprite tacoDip;
    public Sprite nuclearWaste;
    public Sprite camo;
    public Sprite sandmite;

    public GameObject sauceTypeText;

    private WorldManager wm;
    // Use this for initialization
    void Awake() {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.50f, Screen.height * 0.215f)) + new Vector3(Util.worldNormalizedWidth * -0.35f, 0, 10f);
        //transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.15f, Screen.height * 0.215f)) + new Vector3(0, 0, 10f);

        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D coll) {
        coll.gameObject.GetComponent<Knife>().hasSauce = true;
    }

    void OnTriggerExit2D(Collider2D coll) {
        coll.gameObject.GetComponent<Knife>().hasSauce = true;
    }

    public void update() {
        sauceTypeText.GetComponent<SauceTypeText>().txt.text = getSauceName();
        setImage(wm.em.sauceID);
        wm.em.recalculate();
        wm.gtm.knife.GetComponent<Knife>().deleteTrails();
        wm.gtm.knife.GetComponent<Knife>().newTrail();
        updateMenuBar();
    }

    public void updateMenuBar() {
        if (wm.menuState == MenuType.sandwich) {
            Upgrade up = wm.em.list.transform.FindChild("SauceUpgrade").GetComponent<Upgrade>();
            up.updateCost(wm.buttonHandler.sauceCost());
            up.updateName(getSauceName(wm.em.sauceID + 1));
            up.updateIcon(getImage(wm.em.sauceID + 1));
            up.updateStats("$" + Util.encodeNumber(wm.em.getSandwichValue(wm.em.sauceID + 1, Util.em.breadID)) + " ea");
        }
    }

    public string getSauceName() {
        return getSauceName(wm.em.sauceID);
    }

    public string getSauceName(int i) {
        switch (((i - 1) % Sauce.numberOfSauces) + 1) {
            case 1: return "Peanut Butter";
            case 2: return "Strawberry Jam";
            case 3: return "Tears of Despair";
            case 4: return "Nuhtellah";
            case 5: return "Cream Cheese";
            case 6: return "Hamburger";
            case 7: return "Garlic Spread";
            case 8: return "Guacamole";
            case 9: return "Ham";
            case 10: return "Butter";
            case 11: return "Nails";
            case 12: return "Sushi";
            case 13: return "Rat Poison";
            case 14: return "Bacon";
            case 15: return "Gold";
            case 16: return "Sewage";
            case 17: return "Lava";
            case 18: return "Spum";
            case 19: return "Eggs";
            case 20: return "Gunpowder";
            case 21: return "Dynamite";
            case 22: return "Acid";
            case 23: return "Taco Dip";
            case 24: return "Nuclear Waste";
            case 25: return "Camo";
            case 26: return "Sandmite";
        }
        return "Mystery Sauce";
    }

    //ALSO ADD TO TrailManager.cs
    private Sprite setImage(int i) {
        GetComponent<SpriteRenderer>().sprite = getImage(i);
        return GetComponent<SpriteRenderer>().sprite;
    }

    public Sprite getImage(int i) {
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
            case 12: return sushi;
            case 13: return ratPoison;
            case 14: return bacon;
            case 15: return gold;
            case 16: return sewage;
            case 17: return lava;
            case 18: return spum;
            case 19: return rawEggs;
            case 20: return gunpowder;
            case 21: return tnt;
            case 22: return acid;
            case 23: return tacoDip;
            case 24: return nuclearWaste;
            case 25: return camo;
            case 26: return sandmite;
        }
        return peanutButter;
    }
}
