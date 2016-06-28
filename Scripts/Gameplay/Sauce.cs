﻿using UnityEngine;
using System.Collections;

public class Sauce : MonoBehaviour {
    private GameplayTouchManager gtm;
    public Sprite peanutButter;
    public Sprite strawberryJam;
    public Sprite tearsOfDespair;
    public Sprite nuhtelluh;
    public Sprite creamCheese;
    public Sprite hamburger;

    public GameObject sauceTypeText;

    private WorldManager wm;
    // Use this for initialization
    void Awake() {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.15f, Screen.height * 0.215f)) + new Vector3(0, 0, 10f);
        gtm = GameObject.Find("WorldManager").GetComponent<GameplayTouchManager>();

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
        if (wm.menuState == MenuType.sandwich) {
            Upgrade up = wm.em.list.transform.FindChild("SauceUpgrade").GetComponent<Upgrade>();
            up.updateCost(wm.buttonHandler.sauceCost());
            up.updateName(getSauceName(wm.em.sauceID + 1));
            up.updateIcon(getImage(wm.em.sauceID + 1));
            up.updateStats("$" + Util.encodeNumber(wm.em.getSandwichValue(wm.em.sauceID + 1)) + " ea");
        }
    }

    public string getSauceName() {
        return getSauceName(wm.em.sauceID);
    }

    public string getSauceName(int i) {
        switch (i) {
            case 1: return "Peanut Butter";
            case 2: return "Strawberry Jam";
            case 3: return "Tears of Despair";
            case 4: return "Nuhtellah";
            case 5: return "Cream Cheese";
            case 6: return "Hamburger";
            case 7: return "Cream Cheese";
            case 8: return "Cream Cheese";
        }
        return "Mystery Sauce";
    }

    //ALSO ADD TO TrailManager.cs
    private Sprite setImage(int i) {
        GetComponent<SpriteRenderer>().sprite = getImage(i);
        return GetComponent<SpriteRenderer>().sprite;
    }

    public Sprite getImage(int i) {
        switch (i) {
            case 1: return peanutButter;
            case 2: return strawberryJam;
            case 3: return tearsOfDespair;
            case 4: return nuhtelluh;
            case 5: return creamCheese;
            case 6: return hamburger;
            case 7: return creamCheese;
            case 8: return creamCheese;
        }
        return peanutButter;
    }
}
