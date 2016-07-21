using UnityEngine;
using System.Collections;

public class Suicider : MonoBehaviour {
    public float life;
	// Use this for initialization
	void Start () {
        Invoke("suicide", life);
	}

    public void suicide() {
        Destroy(gameObject);
    }
}
