using UnityEngine;
using System.Collections;

public class Sauce : MonoBehaviour {
    private GameplayTouchManager gtm;

    private WorldManager wm;
    // Use this for initialization
    void Awake() {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.1f, Screen.height * 0.2f)) + new Vector3(0, 0, 10f);
        gtm = GameObject.Find("WorldManager").GetComponent<GameplayTouchManager>();

        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D coll) {

    }

    void OnTriggerExit2D(Collider2D coll) {
        
    }
}
