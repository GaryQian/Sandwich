﻿using UnityEngine;
using System.Collections;

public class AchievementManager : MonoBehaviour {

    public void checkMoneyAcheivements() {
        if (!Util.wm.hasCheated) {
        #if UNITY_ANDROID
            if (Util.em.totalMoney < 1E+18f) {
                if (Util.em.money >= 1E+6f) Social.ReportProgress("CgkI1rDm6sMKEAIQBA", 100.0f, (bool success) => { });
                if (Util.em.money >= 1E+9f) Social.ReportProgress("CgkI1rDm6sMKEAIQBQ", 100.0f, (bool success) => { });
                if (Util.em.money >= 1E+12f) Social.ReportProgress("CgkI1rDm6sMKEAIQBw", 100.0f, (bool success) => { });
                if (Util.em.money >= 1E+15f) Social.ReportProgress("CgkI1rDm6sMKEAIQBg", 100.0f, (bool success) => { });
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
#elif UNITY_IOS
            if (Util.em.totalMoney < 1E+18f) {
                if (Util.em.money >= 1E+6f) Social.ReportProgress("millionaire", 100.0f, (bool success) => { });
                if (Util.em.money >= 1E+9f) Social.ReportProgress("billionaire", 100.0f, (bool success) => { });
                if (Util.em.money >= 1E+12f) Social.ReportProgress("trillionaire", 100.0f, (bool success) => { });
                if (Util.em.money >= 1E+15f) Social.ReportProgress("quadrillionaire", 100.0f, (bool success) => { });
            }
            else if (Util.em.totalMoney < 1E+30f) {
                if (Util.em.money >= 1E+18f) Social.ReportProgress("quintillionaire", 100.0f, (bool success) => { });
                if (Util.em.money >= 1E+21f) Social.ReportProgress("sextillionaire", 100.0f, (bool success) => { });
                if (Util.em.money >= 1E+24f) Social.ReportProgress("septillionaire", 100.0f, (bool success) => { });
                if (Util.em.money >= 1E+27f) Social.ReportProgress("octillionaire", 100.0f, (bool success) => { });

            }
            else {
                if (Util.em.money >= 1E+30f) Social.ReportProgress("nonillionaire", 100.0f, (bool success) => { });
                if (Util.em.money >= 1E+33f) Social.ReportProgress("decillionaire", 100.0f, (bool success) => { });
                if (Util.em.money >= 1E+36f) Social.ReportProgress("undecillionaire", 100.0f, (bool success) => { });
            }
#endif
        }
        
    }

    public void checkSwipeAchievements() {
        if (!Util.wm.hasCheated) {
#if UNITY_ANDROID
            if (Util.em.totalSwipes >= 100) Social.ReportProgress("CgkI1rDm6sMKEAIQCQ", 100.0f, (bool success) => { }); //sandwich chef
            if (Util.em.lifetimeSwipes + Util.em.totalSwipes >= 1000) Social.ReportProgress("CgkI1rDm6sMKEAIQHQ", 100.0f, (bool success) => { });
            if (Util.em.lifetimeSwipes + Util.em.totalSwipes >= 10000) Social.ReportProgress("CgkI1rDm6sMKEAIQHg", 100.0f, (bool success) => { });
            if (Util.em.lifetimeSwipes + Util.em.totalSwipes >= 100000) Social.ReportProgress("CgkI1rDm6sMKEAIQHw", 100.0f, (bool success) => { });
            if (Util.em.lifetimeSwipes + Util.em.totalSwipes >= 1000000) Social.ReportProgress("CgkI1rDm6sMKEAIQIA", 100.0f, (bool success) => { });
#elif UNITY_IOS
            if (Util.em.totalSwipes >= 100) Social.ReportProgress("sandwichchef", 100.0f, (bool success) => { }); //sandwich chef
            if (Util.em.lifetimeSwipes + Util.em.totalSwipes >= 1000) Social.ReportProgress("1000swipes", 100.0f, (bool success) => { });
            if (Util.em.lifetimeSwipes + Util.em.totalSwipes >= 10000) Social.ReportProgress("10000swipes", 100.0f, (bool success) => { });
            if (Util.em.lifetimeSwipes + Util.em.totalSwipes >= 100000) Social.ReportProgress("100000swipes", 100.0f, (bool success) => { });
            if (Util.em.lifetimeSwipes + Util.em.totalSwipes >= 1000000) Social.ReportProgress("1000000swipes", 100.0f, (bool success) => { });
#endif
        }
    }

        void handleHumanExtermination() {
        if (Util.em.humanExterminationCount >= 1 && !Util.wm.hasCheated) {
#if UNITY_ANDROID
            Social.ReportProgress("CgkI1rDm6sMKEAIQAQ", 100.0f, (bool success) => { }); //New World Order
#elif UNITY_IOS
            Social.ReportProgress("newworldorder", 100.0f, (bool success) => { });
#endif
        }
    }

    void handleFlyingSandwichMonster() {
        if (Util.em.flyingSandwichMonsterCount >= 1 && !Util.wm.hasCheated) {
#if UNITY_ANDROID
            Social.ReportProgress("CgkI1rDm6sMKEAIQAw", 100.0f, (bool success) => { }); //All Hail
#elif UNITY_IOS
            Social.ReportProgress("allhail", 100.0f, (bool success) => { });
#endif
        }
    }

    void handleSandriaLaw() {
        if (Util.em.sandriaLawCount >= 1 && !Util.wm.hasCheated) {
#if UNITY_ANDROID
            Social.ReportProgress("CgkI1rDm6sMKEAIQGA", 100.0f, (bool success) => { }); //I Declare War
#elif UNITY_IOS
            Social.ReportProgress("ideclarewar", 100.0f, (bool success) => { }); //I Declare War
#endif
        }
    }

    void OnEnable() {
        ButtonHandler.BuyHumanExtermination += handleHumanExtermination;
        ButtonHandler.BuyFlyingSandwichMonster += handleFlyingSandwichMonster;
        ButtonHandler.BuySandriaLaw += handleSandriaLaw;
    }

    void OnDisable() {
        ButtonHandler.BuyHumanExtermination -= handleHumanExtermination;
        ButtonHandler.BuyFlyingSandwichMonster -= handleFlyingSandwichMonster;
        ButtonHandler.BuySandriaLaw -= handleSandriaLaw;
    }
}
