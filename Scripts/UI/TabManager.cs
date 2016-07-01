using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TabManager : MonoBehaviour {
    WorldManager wm;

    public GameObject menu;
    ScrollRect scroll;

    public Button statsButton;
    public Button sandwichButton;
    public Button producerButton;
    public Button permanentButton;
    public Button shopButton;

    private ColorBlock normalColor;
    private ColorBlock highlightedColor;

    void Awake() {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        scroll = menu.GetComponent<ScrollRect>();
    }
	// Use this for initialization
	void Start () {
        highlightedColor = statsButton.colors;
        highlightedColor.normalColor = new Color(.0f, .0f, .0f);
        highlightedColor.highlightedColor = new Color(.0f, .0f, .0f);
        highlightedColor.pressedColor = new Color(.0f, .0f, .0f);
        normalColor = statsButton.colors;
        producerButton.colors = highlightedColor;
    }

    public void selectStats() {
        wm.menuState = MenuType.stats;
        disableCurrentMenu();
        wm.em.list = menu.transform.FindChild("Stats").gameObject;
        enableCurrentMenu();
        resetHighlight();
        statsButton.colors = highlightedColor;
        scroll.content = wm.em.list.GetComponent<RectTransform>();
        
    }

    public void selectSandwich() {
        wm.menuState = MenuType.sandwich;
        disableCurrentMenu();
        wm.em.list = menu.transform.FindChild("Sandwich").gameObject;
        enableCurrentMenu();
        resetHighlight();
        sandwichButton.colors = highlightedColor;
        scroll.content = wm.em.list.GetComponent<RectTransform>();
        
        setupSandwichMenu();
    }

    void setupSandwichMenu() {
        wm.sauce.GetComponent<Sauce>().update();
        wm.buttonHandler.updateSharpenKnives();
        wm.em.list.transform.FindChild("Value").transform.FindChild("SandwichValueText").GetComponent<Text>().text = "Sandwich Value: " + Util.encodeNumber(Util.em.getSandwichValue()) + " each";
    }

    public void selectProducer() {
        wm.menuState = MenuType.producer;
        disableCurrentMenu();
        wm.em.list = menu.transform.FindChild("Producer").gameObject;
        enableCurrentMenu();
        resetHighlight();
        producerButton.colors = highlightedColor;
        scroll.content = wm.em.list.GetComponent<RectTransform>();
    }

    public void selectPermanent() {
        wm.menuState = MenuType.permanent;
        disableCurrentMenu();
        wm.em.list = menu.transform.FindChild("Permanent").gameObject;
        enableCurrentMenu();
        resetHighlight();
        permanentButton.colors = highlightedColor;
        scroll.content = wm.em.list.GetComponent<RectTransform>();
        wm.sm.updatePermanentTab();
    }

    public void selectShop() {
        wm.menuState = MenuType.shop;
        disableCurrentMenu();
        wm.em.list = menu.transform.FindChild("Shop").gameObject;
        enableCurrentMenu();
        resetHighlight();
        shopButton.colors = highlightedColor;
        scroll.content = wm.em.list.GetComponent<RectTransform>();
    }

    void disableCurrentMenu() {
        wm.em.list.SetActive(false);
    }

    void enableCurrentMenu() {
        wm.em.list.SetActive(true);
    }

    void resetHighlight() {
        statsButton.colors = normalColor;
        sandwichButton.colors = normalColor;
        producerButton.colors = normalColor;
        permanentButton.colors = normalColor;
        shopButton.colors = normalColor;
    }
}
