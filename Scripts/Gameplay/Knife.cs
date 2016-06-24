using UnityEngine;
using System.Collections;

public class Knife : MonoBehaviour {
    public GameObject trailPrefab;
	// Use this for initialization
	void Awake() {
        newTrail();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void newTrail() {
        GameObject trail = Instantiate(trailPrefab);
        trail.transform.position = transform.position;
        trail.transform.SetParent(this.transform);
        trail.name = "Trail";
    }
}
