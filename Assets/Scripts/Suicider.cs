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

    public void kill() {
        if (Util.em.sessionTime <= 2f * Util.em.updateRate || Util.em.sessionTime > 5f) {
            suicide();
        }
    }
}
