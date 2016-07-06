/*using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using AudienceNetwork;

[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(RectTransform))]
public class NativeAdModule : MonoBehaviour {
    private NativeAd nativeAd;

    // UI elements in scene
    [Header("Text:")]
    public Text title;
    public Text socialContext;
    [Header("Images:")]
    public Image coverImage;
    public Image iconImage;
    [Header("Buttons:")]
    public Text callToAction;
    public Button callToActionButton;

    void Awake() {
        // Create a native ad request with a unique placement ID (generate your own on the Facebook app settings).
        // Use different ID for each ad placement in your app.
        NativeAd nativeAd = new AudienceNetwork.NativeAd("1709799402613387_1709802542613073");
        this.nativeAd = nativeAd;

        // Wire up GameObject with the native ad; the specified buttons will be clickable.
        nativeAd.RegisterGameObjectForImpression(gameObject, new Button[] { callToActionButton });

        // Set delegates to get notified on changes or when the user interacts with the ad.
        nativeAd.NativeAdDidLoad = (delegate () {
            Debug.Log("Native ad loaded.");
            Debug.Log("Loading images...");
            // Use helper methods to load images from native ad URLs
            StartCoroutine(nativeAd.LoadIconImage(nativeAd.IconImageURL));
            StartCoroutine(nativeAd.LoadCoverImage(nativeAd.CoverImageURL));
            Debug.Log("Images loaded.");
            title.text = nativeAd.Title;
            socialContext.text = nativeAd.SocialContext;
            callToAction.text = nativeAd.CallToAction;
        });
        nativeAd.NativeAdDidFailWithError = (delegate (string error) {
            Debug.Log("Native ad failed to load with error: " + error);
        });
        nativeAd.NativeAdWillLogImpression = (delegate () {
            Debug.Log("Native ad logged impression.");
        });
        nativeAd.NativeAdDidClick = (delegate () {
            Debug.Log("Native ad clicked.");
        });

        // Initiate a request to load an ad.
        nativeAd.LoadAd();
    }

    void OnGUI() {
        // Update GUI from native ad
        coverImage.sprite = nativeAd.CoverImage;
        iconImage.sprite = nativeAd.IconImage;
    }

    void OnDestroy() {
        // Dispose of native ad when the scene is destroyed
        if (this.nativeAd) {
            this.nativeAd.Dispose();
        }
        Debug.Log("NativeAdTest was destroyed!");
    }

}
*/