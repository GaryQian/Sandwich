using UnityEngine;
using System.Collections;

public class Sauce : MonoBehaviour {
    private GameplayTouchManager gtm;
    public Sprite peanutButter;
    public Sprite strawberryJam;
    public Sprite tearsOfDespair;
    public Sprite nuhtelluh;

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
        setImage();
        wm.em.recalculate();
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
        }
        return "Mystery Sauce";
    }

    private void setImage() {
        switch (wm.em.sauceID) {
            case 1: GetComponent<SpriteRenderer>().sprite = peanutButter; break;
            case 2: GetComponent<SpriteRenderer>().sprite = strawberryJam; break;
            case 3: GetComponent<SpriteRenderer>().sprite = tearsOfDespair; break;
            case 4: GetComponent<SpriteRenderer>().sprite = nuhtelluh; break;
            case 5: GetComponent<SpriteRenderer>().sprite = peanutButter; break;
        }
    }
}
