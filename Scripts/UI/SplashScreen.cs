using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {
    public GameObject BlackFadeIn;
    public GameObject Splash;
	// Use this for initialization
	void Start () {
        Invoke("closeSplash", 3.5f);
	}
	
	public void closeSplash() {
        Destroy(Splash);
    }

    public void closeSplashEarly() {
        Destroy(BlackFadeIn);
        Destroy(Splash);
    }
}
