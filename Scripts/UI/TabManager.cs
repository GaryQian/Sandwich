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


    public Image BuyButton1;
    public Image BuyButton2;
    public Image BuyButton3;
    public Image BuyButton4;
    public Image BuyButton5;
    public Image BuyButton6;
    public Image BuyButton7;
    public Image BuyButton8;
    public Image BuyButton9;
    public Image BuyButton10;
    public Image BuyButton11;
    public Image BuyButton12;
    public Image BuyButton13;
    public Image BuyButton14;
    public Image BuyButton15;

    private ColorBlock normalColor;
    private ColorBlock highlightedColor;

    /// <summary>
    /// EVENTS
    /// </summary>
    public delegate void TabSwitch();
    public static event TabSwitch SwitchPermanent;
    public static event TabSwitch SwitchStats;
    public static event TabSwitch SwitchSandwich;
    public static event TabSwitch SwitchProducer;
    public static event TabSwitch SwitchShop;

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

    public void setBuyButtonSprite(Sprite sp) {
        BuyButton1.sprite = sp;
        BuyButton2.sprite = sp;
        BuyButton3.sprite = sp;
        BuyButton4.sprite = sp;
        BuyButton5.sprite = sp;
        BuyButton6.sprite = sp;
        BuyButton7.sprite = sp;
        BuyButton8.sprite = sp;
        BuyButton9.sprite = sp;
        BuyButton10.sprite = sp;
        BuyButton11.sprite = sp;
        BuyButton12.sprite = sp;
        BuyButton13.sprite = sp;
        BuyButton14.sprite = sp;
        BuyButton15.sprite = sp;
    }

    public void selectStats() {
        wm.menuState = MenuType.stats;
        disableCurrentMenu();
        wm.em.list = menu.transform.FindChild("Stats").gameObject;
        enableCurrentMenu();
        resetHighlight();
        statsButton.colors = highlightedColor;
        scroll.content = wm.em.list.GetComponent<RectTransform>();
        if (SwitchStats != null) SwitchStats();
        
    }

    public void selectSandwich() {
        wm.menuState = MenuType.sandwich;
        disableCurrentMenu();
        wm.em.list = menu.transform.FindChild("Sandwich").gameObject;
        enableCurrentMenu();
        resetHighlight();
        sandwichButton.colors = highlightedColor;
        scroll.content = wm.em.list.GetComponent<RectTransform>();
        wm.tutorialManager.sandwichButtonGlow.SetActive(false);
        setupSandwichMenu();
        if (SwitchSandwich != null) SwitchSandwich();
    }

    void setupSandwichMenu() {
        wm.sauce.GetComponent<Sauce>().update();
        wm.buttonHandler.updateSharpenKnives();
        wm.em.list.transform.FindChild("Value").transform.FindChild("SandwichValueText").GetComponent<Text>().text = "$" + Util.encodeNumber(Util.em.getSandwichValue()) + " each &";
        wm.buttonHandler.updateSandwichReproduction();
        Bread.updateButton();
    }

    public void selectProducer() {
        wm.menuState = MenuType.producer;
        disableCurrentMenu();
        wm.em.list = menu.transform.FindChild("Producer").gameObject;
        enableCurrentMenu();
        resetHighlight();
        producerButton.colors = highlightedColor;
        scroll.content = wm.em.list.GetComponent<RectTransform>();
        wm.tutorialManager.producerButtonGlow.SetActive(false);
        if (SwitchProducer != null) SwitchProducer();
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
        Util.em.updateElixirUpgrades();
        if (SwitchPermanent != null) SwitchPermanent();
    }

    public void selectShop() {
        wm.menuState = MenuType.shop;
        disableCurrentMenu();
        wm.em.list = menu.transform.FindChild("Shop").gameObject;
        enableCurrentMenu();
        resetHighlight();
        shopButton.colors = highlightedColor;
        scroll.content = wm.em.list.GetComponent<RectTransform>();
        if (SwitchShop != null) SwitchShop();
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
