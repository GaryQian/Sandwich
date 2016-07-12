using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FIRE : MonoBehaviour {
    Animator anim;
    void Start() {
        Invoke("checkStart", 0.7f);
        ButtonHandler.BuySandriaLaw += OnBuySandriaLaw;
        ButtonHandler.BuyHumanExtermination += OnBuyHumanExtermination;
    }

    void checkStart() {
        anim = GetComponent<Animator>();
        OnBuySandriaLaw();
    }

    void OnBuySandriaLaw() {
        Debug.LogError("ONBUYSANDRIALAW");
        if (Util.em.sandriaLawCount >= 1 && Util.em.humanExterminationCount == 0) {
            anim.SetTrigger("FireOn");
        }
        else {
            anim.SetTrigger("FireOff");
        }
    }

    void OnBuyHumanExtermination() {
        anim.SetTrigger("FireFade");
        Invoke("setOff", 1.667f);
    }

    void setOff() {
        anim.SetTrigger("FireOff");
    }

    void OnEnable() {
        ButtonHandler.BuySandriaLaw += OnBuySandriaLaw;
        ButtonHandler.BuyHumanExtermination += OnBuyHumanExtermination;
    }
    void OnDisable() {
        ButtonHandler.BuySandriaLaw -= OnBuySandriaLaw;
        ButtonHandler.BuyHumanExtermination += OnBuyHumanExtermination;
    }
}
