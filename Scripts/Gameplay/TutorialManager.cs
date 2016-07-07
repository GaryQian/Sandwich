using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {

    public bool tutorialActive = true;

    public GameObject fingerPrefab;
    public GameObject yellowArrow;

    public GameObject sandwichButtonGlow;
    public GameObject sauceYellowArrow;

    public GameObject producerButtonGlow;
    public GameObject sandwichCartYellowArrow;

    public GameObject permanentButtonGlow;
    public GameObject permYellowArrow1;
    public GameObject permYellowArrow2;
    public GameObject permYellowArrow3;
    WorldManager wm;

    void Awake() {
        wm = GetComponent<WorldManager>();
    }

	// Use this for initialization
	void Start () {
        if (needTutorial()) {
            tutorialActive = true;
            if (needFinger()) {
                Invoke("showFinger", 1f);
            }
        }
        else {
            tutorialActive = false;
        }
        
    }

    bool needTutorial() {
        if (Util.em.totalMoney < 2000f || Util.em.sauceID == 1 || Util.em.sandwichCartCount == 0) {
            if (Util.wm.playthroughCount == 0) {
                return true;
            }
        }
        return false;
    }

    private bool needFinger() {
        if (wm.em.totalMoney < 75f || wm.em.sps < 0.4f || (wm.em.gameTime < 200f && wm.em.gameTime / wm.em.totalSwipes > 3f)) {
            return true;
        }
        return false;
    }

    public void showFinger() {
        Instantiate(fingerPrefab);
        if (needFinger()) {
            Invoke("showFinger", 10f);
        }
    }

    public void activateSandwichCartTutorial() {
        if (Util.em.sandwichCartCount < 2) {
            producerButtonGlow.SetActive(true);
            sandwichCartYellowArrow.SetActive(true);
        }
    }
    void removeSandwichCartTutorial() {
        producerButtonGlow.SetActive(false);
        sandwichCartYellowArrow.SetActive(false);
        //activateSauceTutorial();
    }

    /*public void removeYellowArrow() {
        if (yellowArrow != null) {
            Destroy(yellowArrow);
        }
    }*/

    void activateSauceTutorial() {
        if (Util.em.sauceID == 1 && Util.em.sandwichCartCount >= 1) {
            sandwichButtonGlow.SetActive(true);
            sauceYellowArrow.SetActive(true);

        }
    }
    void removeSauceTutorial() {
        sandwichButtonGlow.SetActive(false);
        sauceYellowArrow.SetActive(false);
        activateSandwichCartTutorial();
    }



    void activateEvolutionTutorial() {
        permanentButtonGlow.SetActive(true);
        permYellowArrow1.SetActive(true);
        permYellowArrow2.SetActive(true);
        permYellowArrow3.SetActive(true);
    }
    void removeEvolutionTutorial() {
        permanentButtonGlow.SetActive(false);
        permYellowArrow1.SetActive(false);
        permYellowArrow2.SetActive(false);
        permYellowArrow3.SetActive(false);
    }



    void OnEnable() {
        WorldManager.Reached50 += activateSandwichCartTutorial;
        WorldManager.Reached500 += activateSauceTutorial;
        ButtonHandler.BuySauce += removeSauceTutorial;
        ButtonHandler.BuySandwichCart += removeSandwichCartTutorial;
        ResetManager.RESET += activateEvolutionTutorial;
        ButtonHandler.BuyEvolution += removeEvolutionTutorial;
    }
    void OnDisable() {
        WorldManager.Reached50 -= activateSandwichCartTutorial;
        WorldManager.Reached500 -= activateSauceTutorial;
        ButtonHandler.BuySauce -= removeSauceTutorial;
        ButtonHandler.BuySandwichCart -= removeSandwichCartTutorial;
        ResetManager.RESET -= activateEvolutionTutorial;
        ButtonHandler.BuyEvolution -= removeEvolutionTutorial;
    }
}
