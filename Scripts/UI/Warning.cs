using UnityEngine;
using System.Collections;

public class Warning : MonoBehaviour {
    public GameObject confirm;
	// Use this for initialization
	void Start () {
	
	}

    public void cancel() {
        Destroy(gameObject);
    }

    public void reset() {
        confirm.SetActive(true);
    }

    public void confirmClick() {
        cancel();
        ResetManager.reset();
    }
}
