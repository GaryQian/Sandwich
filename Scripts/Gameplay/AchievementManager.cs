using UnityEngine;
using System.Collections;

public class AchievementManager : MonoBehaviour {

    public void checkMoneyAcheivements() {
        if (Util.em.totalMoney < 1E+18f) {
            if (Util.em.money >= 1E+6f) Social.ReportProgress("CgkI1rDm6sMKEAIQBA", 100.0f, (bool success) => { });
            if (Util.em.money >= 1E+9f) Social.ReportProgress("CgkI1rDm6sMKEAIQBQ", 100.0f, (bool success) => { });
            if (Util.em.money >= 1E+12f) Social.ReportProgress("CgkI1rDm6sMKEAIQBw", 100.0f, (bool success) => { });
            if (Util.em.money >= 1E+15f) Social.ReportProgress("CgkI1rDm6sMKEAIQBg", 100.0f, (bool success) => { });

            if (Util.em.totalSwipes >= 100) Social.ReportProgress("CgkI1rDm6sMKEAIQCQ", 100.0f, (bool success) => { }); //sandwich chef
        }
        else if (Util.em.totalMoney < 1E+30f) {
            if (Util.em.money >= 1E+18f) Social.ReportProgress("CgkI1rDm6sMKEAIQCA", 100.0f, (bool success) => { });
            if (Util.em.money >= 1E+21f) Social.ReportProgress("CgkI1rDm6sMKEAIQCg", 100.0f, (bool success) => { });
            if (Util.em.money >= 1E+24f) Social.ReportProgress("CgkI1rDm6sMKEAIQCw", 100.0f, (bool success) => { });
            if (Util.em.money >= 1E+27f) Social.ReportProgress("CgkI1rDm6sMKEAIQDw", 100.0f, (bool success) => { });

        }
        else {
            if (Util.em.money >= 1E+30f) Social.ReportProgress("CgkI1rDm6sMKEAIQEA", 100.0f, (bool success) => { });
            if (Util.em.money >= 1E+33f) Social.ReportProgress("CgkI1rDm6sMKEAIQEQ", 100.0f, (bool success) => { });
            if (Util.em.money >= 1E+36f) Social.ReportProgress("CgkI1rDm6sMKEAIQEg", 100.0f, (bool success) => { });
            if (Util.em.money >= 1E+39d) Social.ReportProgress("CgkI1rDm6sMKEAIQEw", 100.0f, (bool success) => { });
            if (Util.em.money >= 1E+32d) Social.ReportProgress("CgkI1rDm6sMKEAIQFA", 100.0f, (bool success) => { });
            if (Util.em.money >= 1E+35d) Social.ReportProgress("CgkI1rDm6sMKEAIQFQ", 100.0f, (bool success) => { });
            if (Util.em.money >= 1E+38d) Social.ReportProgress("CgkI1rDm6sMKEAIQFg", 100.0f, (bool success) => { });
            if (Util.em.money >= 1E+41d) Social.ReportProgress("CgkI1rDm6sMKEAIQFw", 100.0f, (bool success) => { });
        }
    }

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
