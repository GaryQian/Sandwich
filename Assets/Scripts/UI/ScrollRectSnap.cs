﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollRectSnap : MonoBehaviour {

    float[] points;
    [Tooltip("how many screens or pages are there within the content (steps)")]
    public int screens = 8;
    public float stepSize;

    ScrollRect scroll;
    bool LerpH;
    float targetH;
    [Tooltip("Snap horizontally")]
    public bool snapInH = true;

    bool LerpV;
    //float targetV;
    [Tooltip("Snap vertically")]
    public bool snapInV = false;


    int currentIndex = 0;
    public Text nameText;

    public GameObject knifePanel;

    public Text continueButtonText;

    public Sprite blueSaber;
    public Sprite redSaber;
    public Sprite greenSaber;
    public Sprite purpleSaber;

    public Sprite redFlowers;
    public Sprite whiteFlowers;
    public Sprite yellowFlowers;
    public Sprite purpleFlowers;

    public Sprite redShoe;
    public Sprite blackShoe;
    public Sprite purpleShoe;
    public Sprite blueShoe;

    int saberCount = 0;

    public Image saberImage;
    public Image flowerImage;
    public Image shoeImage;
    // Use this for initialization
    void Start() {
        scroll = gameObject.GetComponent<ScrollRect>();
        scroll.inertia = false;

        if (screens > 0) {
            points = new float[screens];
            //stepSize = 1 / (float)(screens - 1);

            for (int i = 0; i < screens; i++) {
                points[i] = i * stepSize;
            }
        }
        else {
            points[0] = 0;
        }
        currentIndex = Util.wm.knifeID;
        targetH = points[currentIndex];
        scroll.horizontalNormalizedPosition = targetH;
        nameText.text = Knife.getKnifeName(currentIndex);
        if (Util.wm.knifeCollectionPurchased || Util.godmode) continueButtonText.text = "Use This Knife!";

        switch (Util.wm.saberColor) {
            case SaberColor.blue: saberCount = 40000; break;
            case SaberColor.red: saberCount = 40001; break;
            case SaberColor.green: saberCount = 40002; break;
            case SaberColor.purple: saberCount = 40003; break;
        }
        Util.wm.flowerColor = Util.wm.flowerColor % 4 + 40000;
        Util.wm.shoeColor = Util.wm.shoeColor % 4 + 40000;
        setSaberColor();
        setFlowerColor();
        setShoeColor();

    }

    void Update() {
        if (LerpH) {
            scroll.horizontalNormalizedPosition = Mathf.Lerp(scroll.horizontalNormalizedPosition, targetH, 30 * scroll.elasticity * Time.deltaTime);
            if (Mathf.Approximately(scroll.horizontalNormalizedPosition, targetH)) LerpH = false;
        }
        /*if (LerpV) {
            scroll.verticalNormalizedPosition = Mathf.Lerp(scroll.verticalNormalizedPosition, targetV, 30 * scroll.elasticity * Time.deltaTime);
            if (Mathf.Approximately(scroll.verticalNormalizedPosition, targetV)) LerpV = false;
        }*/
    }

    public void DragEnd() {
        if (scroll.horizontal && snapInH) {
            targetH = points[FindNearest(scroll.horizontalNormalizedPosition, points)];
            LerpH = true;
        }
        if (scroll.vertical && snapInV) {
            targetH = points[FindNearest(scroll.verticalNormalizedPosition, points)];
            LerpH = true;
        }

        nameText.text = Knife.getKnifeName(currentIndex);
    }

    public void OnDrag() {
        LerpH = false;
        LerpV = false;
    }

    int FindNearest(float f, float[] array) {
        float distance = Mathf.Infinity;
        int output = 0;
        for (int index = 0; index < array.Length; index++) {
            if (Mathf.Abs(array[index] - f) < distance) {
                distance = Mathf.Abs(array[index] - f);
                output = index;
                currentIndex = index;
            }
        }
        return output;
    }

    public void closePanel() {
        if (Util.wm.knifeCollectionPurchased || Util.godmode) {
            Util.wm.knifeID = currentIndex;
            Util.wm.gtm.knife.GetComponent<Knife>().setupKnifeType();
        }
        switch (saberCount % 4) {
            case 0: Util.wm.saberColor = SaberColor.blue; break;
            case 1: Util.wm.saberColor = SaberColor.red; break;
            case 2: Util.wm.saberColor = SaberColor.green; break;
            case 3: Util.wm.saberColor = SaberColor.purple; break;
        }
        Destroy(knifePanel);
    }

    public void leftButtonSaber() {
        saberCount--;
        setSaberColor();
    }
    public void rightButtonSaber() {
        saberCount++;
        setSaberColor();
    }

    public void leftButtonFlower() {
        Util.wm.flowerColor--;
        setFlowerColor();
    }
    public void rightButtonFlower() {
        Util.wm.flowerColor++;
        setFlowerColor();
    }

    public void leftButtonShoe() {
        Util.wm.shoeColor--;
        setShoeColor();
    }
    public void rightButtonShoe() {
        Util.wm.shoeColor++;
        setShoeColor();
    }

    void setSaberColor() {
        switch (saberCount % 4) {
            case 0: saberImage.sprite = blueSaber; break;
            case 1: saberImage.sprite = redSaber; break;
            case 2: saberImage.sprite = greenSaber; break;
            case 3: saberImage.sprite = purpleSaber; break;
        }
    }
    void setFlowerColor() {
        switch (Util.wm.flowerColor % 4) {
            case 0: flowerImage.sprite = redFlowers; break;
            case 1: flowerImage.sprite = whiteFlowers; break;
            case 2: flowerImage.sprite = yellowFlowers; break;
            case 3: flowerImage.sprite = purpleFlowers; break;
        }
    }
    void setShoeColor() {
        switch (Util.wm.shoeColor % 4) {
            case 0: shoeImage.sprite = redShoe;  break;
            case 1: shoeImage.sprite = blackShoe;  break;
            case 2: shoeImage.sprite = purpleShoe; break;
            case 3: shoeImage.sprite = blueShoe; break;
        }
    }
}