using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

    public EconomyManager em;
    private WorldManager wm;
    public GameObject red;

    public double sauceBaseCost;

    void Awake() {
        em = GetComponent<EconomyManager>();
        wm = GetComponent<WorldManager>();
    }
	// Use this for initialization
	void Start () {
	
	}

    void notEnough() {
        red.active = true;
        red.GetComponent<Animator>().SetTrigger("Flash");
        Invoke("disableRed", 0.267f);
    }

    void disableRed() {
        red.active = false;
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
        if (em.money >= sandwichCartCost()) {
            em.spend(sandwichCartCost());
            em.sandwichCartCount++;
            em.recalculate();
            em.updateProducerMenuCounters();
        }
        else {
            notEnough();
        }
    }
    double sandwichCartCost() {
        return em.list.transform.FindChild("SandwichCart").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.sandwichCartCount);
    }

    /// <summary>
    /// DELI
    /// </summary>
    public void buyDeli() {
        if (em.money >= deliCost()) {
            em.spend(deliCost());
            em.deliCount++;
            em.recalculate();
            em.updateProducerMenuCounters();
        }
        else {
            notEnough();
        }
    }
    double deliCost() {
        return em.list.transform.FindChild("Deli").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.deliCount);
    }

    /// <summary>
    /// AUTOCHEF 9k
    /// </summary>
    public void buyAutochef() {
        if (em.money >= autochefCost()) {
            em.spend(autochefCost());
            em.autochefCount++;
            em.recalculate();
            em.updateProducerMenuCounters();
        }
        else {
            notEnough();
        }
    }
    double autochefCost() {
        return em.list.transform.FindChild("Autochef9k").GetComponent<Upgrade>().baseCost * Mathf.Pow(Util.pScale, em.autochefCount);
    }

}
