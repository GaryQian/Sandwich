using UnityEngine;
using System.Collections;

public class Bread : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.7f, Screen.height * 0.15f)) + new Vector3(0, 0, 10f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
