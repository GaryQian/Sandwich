using UnityEngine;
using System.Collections;

public class AchievementManager : MonoBehaviour {
    
    void handleHumanExtermination() {
        if (Util.em.humanExterminationCount >= 1) Social.ReportProgress("CgkI1rDm6sMKEAIQAQ", 100.0f, (bool success) => { }); //New World Order
    }

    void handleFlyingSandwichMonster() {
        if (Util.em.flyingSandwichMonsterCount >= 1) Social.ReportProgress("CgkI1rDm6sMKEAIQAw", 100.0f, (bool success) => { }); //All Hail
    }

    void OnEnable() {
        ButtonHandler.BuyHumanExtermination += handleHumanExtermination;
        ButtonHandler.BuyFlyingSandwichMonster += handleFlyingSandwichMonster;
    }
}
