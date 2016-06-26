using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

    public EconomyManager em;
    private WorldManager wm;

    public double sauceBaseCost;

    void Awake() {
        em = GetComponent<EconomyManager>();
        wm = GetComponent<WorldManager>();
    }
	// Use this for initialization
	void Start () {
	
	}

    void notEnough() {

    }

    /// <summary>
    /// SAUCE UPGRADE
    /// </summary>
    public void upgradeSauce() {
        if (em.money >= sauceCost()) {
            em.spend(sauceCost());
            em.sauceID++;
            wm.sauce.GetComponent<Sauce>().update();
        }
        else {
            notEnough();
        }
    }
    double sauceCost() {
        return sauceBaseCost * Mathf.Pow(3, em.sauceID - 1);
    }


    /// <summary>
    /// SANDWICH CART
    /// </summary>
    public void buySandwichCart() {
        if (em.money >= sauceCost()) {
            em.spend(sauceCost());
            em.sandwichCartCount++;
            em.recalculate();
            em.updateMenuCounters();
        }
        else {
            notEnough();
        }
    }

}
