using UnityEngine;
using System.Collections;

public class TrailManager : MonoBehaviour {
    public TrailRenderer trail;
    // Use this for initialization
    void Awake () {
        trail = GetComponent<TrailRenderer>();
        trail.sortingLayerName = "Top";
        trail.sortingOrder = 0;
        trail.transform.position = trail.transform.position + new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
