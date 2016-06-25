using UnityEngine;
using System.Collections;

public class BreadTop : MonoBehaviour {
    SpriteRenderer sr;

    private float dropTime = 0.25f;
    private float dropTimer = 0;
    private float waitTime = 0.05f;
    private float waitTimer = 0;
    private float removeTime = 0.07f;
    private float removeTimer = 0;

    private Vector3 startPos;

    public GameObject bread;
	// Use this for initialization
	void Awake () {
	    sr = GetComponent<SpriteRenderer>();
        Invoke("suicide", 2.8f);
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (dropTimer < dropTime) {
            dropTimer += Time.deltaTime;
            sr.color = new Color(1f, 1f, 1f, dropTimer / dropTime - 0.3f);
            transform.Translate(new Vector3(5.5f / dropTime * Time.deltaTime, -4f / dropTime * Time.deltaTime));
            bread.transform.Translate(new Vector3(5.5f / dropTime * Time.deltaTime, 0));
        }
        else if (waitTimer < waitTime) {
            waitTimer += Time.deltaTime;
            //transform.position = startPos + new Vector3(0, -4f);
        }
        else if (removeTimer < removeTime) {
            
        }
	}

    void suicide() {
        Destroy(gameObject);
    }
}
