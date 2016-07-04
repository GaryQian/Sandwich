using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ElixirCountText : MonoBehaviour {
    Text txt;

    void Awake() {
        txt = GetComponent<Text>();
    }
    // Use this for initialization
    void Start() {

    }

    public void updateElixirText() {
        txt.text = Util.encodeNumberInteger((int)Util.em.elixir);
    }

    void OnEnable() {
        InvokeRepeating("updateElixirText", 0, 2f);
    }
}
