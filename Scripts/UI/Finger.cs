using UnityEngine;
using System.Collections;

public class Finger : MonoBehaviour {
    public float life;
    float timer;
    public Vector3 offset;
    WorldManager wm;

	// Use this for initialization
	void Start () {
        wm = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        timer = 0f;
        transform.position = wm.sauce.transform.position - new Vector3(1f, 0, 0);
        offset = (wm.activeBread.transform.position - wm.sauce.transform.position + new Vector3(4f, 0, 0)) / life;

	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer < life) {
            transform.Translate(offset * Time.deltaTime);
            if (timer < life / 4f) {
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (timer / (life / 4f)));
                transform.localScale = new Vector3(1f + (((life / 4f) - timer) / (life / 4f)), 1f + (((life / 4f) - timer) / (life / 4f)), 1f);
            }
            else if (timer > life * 0.75f) {
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (life - timer) / (life / 4f));
                transform.localScale = new Vector3(2f - ((life - timer) / (life / 4f)), 2f - ((life - timer) / (life / 4f)), 1f);
            }
        }
        
        else {
            Destroy(gameObject);
        }
	}
}
