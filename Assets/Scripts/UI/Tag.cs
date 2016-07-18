using UnityEngine;
using System.Collections;
//using UnityEngine.Advertisements;

public class Tag : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void OnMouseDown() {
        GetComponent<Animator>().SetTrigger("Bounce");
        //Advertisement.Show();
    }
}
