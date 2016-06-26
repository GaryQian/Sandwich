using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

    public EconomyManager em;

    void Awake() {
        em = GetComponent<EconomyManager>();
    }
	// Use this for initialization
	void Start () {
	
	}


}
