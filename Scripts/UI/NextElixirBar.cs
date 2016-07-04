using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextElixirBar : MonoBehaviour {
    public GameObject elixirCounter;
    public GameObject nextElixirText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Util.even) {
            transform.localScale = new Vector3((float) (1f - (ResetManager.moneyRemainingNextElixir() / ResetManager.nextElixirCost())), 1f, 1f);
            nextElixirText.GetComponent<Text>().text = "Next elixir in: $" + Util.encodeNumber(ResetManager.moneyRemainingNextElixir());
        }
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
