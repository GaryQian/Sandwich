using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UpdateNursery : MonoBehaviour {
    public Text pop;
    public Text val; 
	// Use this for initialization
	void Start () {
	    
	}

    void update() {
        pop.text = Util.encodeNumber(Util.em.nurseryPop) + " Baby Sandwiches";
        val.text = "Worth $" + Util.encodeNumber(Util.em.nurseryPop * Util.em.sandwichValue);
    }

    void OnEnable() {
        InvokeRepeating("update", 0, 1f);
    }

    void OnDisable() {
        CancelInvoke("update");
    }
}
