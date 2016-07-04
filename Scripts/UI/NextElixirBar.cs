using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextElixirBar : MonoBehaviour {
    public GameObject elixirCounter;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3((float) (1f - (ResetManager.moneyRemainingNextElixir() / ResetManager.nextElixirCost())), 1f, 1f);
	}

    void updateElixirCounter() {
        elixirCounter.GetComponent<Text>().text = Util.encodeNumberInteger((int)ResetManager.elixirsOnReset());
    }

    void OnEnable() {
        //InvokeRepeating("updateElixirCounter", 0, 1f);
    }

    void OnDisable() {
        //CancelInvoke("updateElixirCounter");
    }
}
