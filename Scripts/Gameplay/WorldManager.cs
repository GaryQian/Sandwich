using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {
    public GameObject breadPrefab;
    public GameObject activeBread;
    public GameObject sauce;
    public GameplayTouchManager gtm;
    public EconomyManager em;
	// Use this for initialization
	void Start () {
        activeBread = Instantiate(breadPrefab);
        gtm = GetComponent<GameplayTouchManager>();
        em = GetComponent<EconomyManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void spawnBread() {
        activeBread = Instantiate(breadPrefab);
    }

    public string encodeNumber(double money) {
        int numSize = 4;
        while (money / Mathf.Pow(10f, numSize) > 1f) {
            numSize += 3;
        }
        string suffix = "";
        switch (numSize) {
            case 4: suffix = ""; break;
            case 7: suffix = "k"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 10: suffix = "m"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 13: suffix = "b"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 16: suffix = "t"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 19: suffix = "qd"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 22: suffix = "qt"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 25: suffix = "sx"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 28: suffix = "sp"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 31: suffix = "oc"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 34: suffix = "nn"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 37: suffix = "dc"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 40: suffix = "ud"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 43: suffix = "dd"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 46: suffix = "td"; money = money / Mathf.Pow(10f, numSize - 4); break;
            case 49: suffix = "qtd"; money = money / Mathf.Pow(10f, numSize - 4); break;
        }
        if (money < 100f) {
            return string.Format("{0:N2}", money) + suffix;
        }
        else {
            return string.Format("{0:N1}", money) + suffix;
        }
    }
}
