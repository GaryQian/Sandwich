using UnityEngine;
using System.Collections;

public class NurseryAlert : MonoBehaviour {

    void Awake() {
        name = "NurseryAlert";
    }

    void OnSellBabies() {
        Destroy(gameObject);
    }

    public void click() {
        Util.wm.tabManager.showNursery();
    }

    void OnEnable() {
        UpdateNursery.SoldBabies += OnSellBabies;
    }
    void OnDisable() {
        UpdateNursery.SoldBabies -= OnSellBabies;
    }
}
