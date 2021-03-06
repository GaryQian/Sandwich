﻿using UnityEngine;
using System.Collections;

public class NotificationText : MonoBehaviour {
    GUIText txt;
    public float life;
    float timer;
    public float riseRate;
    private float initOpacity;
	// Use this for initialization
	void Awake () {
        txt = GetComponent<GUIText>();
        timer = life;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        transform.Translate(new Vector3(0, riseRate * Time.deltaTime, 0));
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, initOpacity * (timer / life));
        if (timer < 0) {
            Destroy(gameObject);
        }
	}

    public void setup(string str, Vector3 pos, Color c, int fontSize, float o) {
        txt = GetComponent<GUIText>();
        txt.text = str;
        txt.color = c;
        txt.fontSize = fontSize;
        initOpacity = o;
        Vector2 sp = Camera.main.WorldToScreenPoint(pos);
        transform.position = new Vector3(sp.x / Screen.width, sp.y / Screen.height);
    }

    public void setup(string str, Vector3 pos) {
        setup(str, pos, new Color(1f, 1f, 1f), (int)(Screen.height * 0.03f), 1f);
    }
}
