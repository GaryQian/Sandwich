using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class YellowArrow : MonoBehaviour {

    void Awake() {
        
    }
	// Use this for initialization
	void Start () {
	
	}

    void check() {
        if (Util.em.money >= 50f && Util.em.totalMoney < 100 && Util.em.sandwichCartCount == 0) {
            gameObject.SetActive(true);
        }
    }
}
