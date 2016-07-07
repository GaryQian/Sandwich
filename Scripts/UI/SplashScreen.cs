using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("loadGame", 2.583f);
	}
	
	public void loadGame() {
        Application.LoadLevel("Game");
    }
}
