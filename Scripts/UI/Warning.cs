using UnityEngine;
using System.Collections;

public class Warning : MonoBehaviour {
    public GameObject confirm;
    public GameObject whitescreenPrefab;
	// Use this for initialization
	void Start () {
	
	}

    public void cancel() {
        Destroy(gameObject);
    }

    public void reset() {
        confirm.SetActive(true);
    }

    void callReset() {
        ResetManager.reset();
    }

    public void confirmClick() {
        Instantiate(whitescreenPrefab);
        Invoke("cancel", 1.1f);
        Invoke("callReset", 1f);
    }
}
