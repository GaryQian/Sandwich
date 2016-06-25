using UnityEngine;
using System.Collections;

public class Knife : MonoBehaviour {
    public GameObject trailPrefab;
    public bool hasSauce;
    public GameObject trail;
	// Use this for initialization
	void Awake() {
        newTrail();
        hasSauce = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void newTrail() {
        trail = Instantiate(trailPrefab);
        trail.transform.position = transform.position + new Vector3(0, 0, -1f);
        trail.transform.SetParent(this.transform);
        trail.name = "Trail";
    }
}
