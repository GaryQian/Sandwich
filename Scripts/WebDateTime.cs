using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using UnityEngine.UI;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System;

public class WebDateTime : MonoBehaviour {
    string timeString = "", dateString = "";
    public string TimeZone = "cst";

    public DateTime GetCurrentTime() {
        //Request html from Bing
        ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
        WebRequest request;
        request = WebRequest.Create("https://www.bing.com/search?q=date+time+" + TimeZone + "&qs=AS&pq=date+time+" + TimeZone + "&sc=1-16&sp=1&cvid=8F9748DDC7AF4F77997833D02E1C0927&FORM=QBLH");
        WebResponse response = request.GetResponse();
        //Copy html to a Stream
        Stream stream = response.GetResponseStream();
        StreamReader reader = new StreamReader(stream);
        string s = "";
        //Search for date and time within html
        while (!reader.EndOfStream) {
            s = reader.ReadLine();
            if (s.Contains("b_focusTextLarge"))
                timeString = s;
            if (s.Contains("b_secondaryFocus"))
                dateString = s;
        }
        //Close StreamReader and WebResponse
        reader.Close();
        response.Close();
        //Extract date and time
        string time = extractTime(timeString);
        string date = extractDate(dateString);
        //Convert date and time to DateTime
        return DateTime.Parse(date + " " + time);
    }
    string extractTime(string s) {
        string timeString = "";
        //Search for <b_focusTextLarge>
        for (int x = 0; x < s.Length; x++) {
            if (s[x] == '>') {
                string testString = "";
                if (x >= 17) {
                    for (int y = 17; y > 1; y--) {
                        testString += s[x - y];
                    }
                }
                if (testString == "b_focusTextLarge") {
                    for (int z = 1; z < 11; z++) {
                        //Extract the time
                        timeString += s[x + z];
                    }
                }
            }
        }
        return timeString;
    }
    string extractDate(string s) {
        string dateString = "";
        //Search for <b_secondaryFocus>
        for (int x = 0; x < s.Length; x++) {
            if (s[x] == '>') {
                string testString = "";
                if (x >= 17) {
                    for (int y = 17; y > 1; y--) {
                        testString += s[x - y];
                    }
                }
                if (testString == "b_secondaryFocus") {
                    int w = 1;
                    while (s[x + w] != ',') {
                        w++;
                    }
                    x += w + 1;
                    //Extract Month
                    w = 1;
                    string month = "";
                    while (s[x + w] != ' ') {
                        month += s[x + w];
                        w++;
                    }
                    x += w + 1;
                    dateString += MonthtoInt(month) + "/";
                    //Extract Day
                    int day = int.Parse("" + s[x] + s[x + 1]);
                    dateString += day + "/";
                    x += 3;
                    //Extract Year
                    int year = int.Parse("" + s[x] + s[x + 1] + s[x + 2] + s[x + 3] + s[x + 4]);
                    dateString += year;
                }
            }
        }
        return dateString;
    }
    int MonthtoInt(string month) {
        //Convert Month to Int
        switch (month) {
            case "January":
                return 1;
            case "February":
                return 2;
            case "March":
                return 3;
            case "April":
                return 4;
            case "May":
                return 5;
            case "June":
                return 6;
            case "July":
                return 7;
            case "August":
                return 8;
            case "September":
                return 9;
            case "October":
                return 10;
            case "November":
                return 11;
            case "December":
                return 12;
        }
        return 1;
    }
    public bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
        bool isOk = true;
        // If there are errors in the certificate chain, look at each error to determine the cause.
        if (sslPolicyErrors != SslPolicyErrors.None) {
            for (int i = 0; i < chain.ChainStatus.Length; i++) {
                if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown) {
                    chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                    chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                    chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                    bool chainIsValid = chain.Build((X509Certificate2)certificate);
                    if (!chainIsValid) {
                        isOk = false;
                    }
                }
            }
        }
        return isOk;
    }
}
