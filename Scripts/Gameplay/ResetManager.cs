using UnityEngine;
using System.Collections;

public class ResetManager : MonoBehaviour {
    public GameObject warningPrefab;
    public GameObject warning;
	// Use this for initialization
	void Start () {
	    
	}

    public static long elixirsOnReset() {
        double num = System.Math.Pow(Util.em.totalMoney / Util.elixirBaseCost, 1f / Util.elixirScale);
        return (long)System.Math.Floor(num);
    }

    public static double costOfElixirs(long curr) {
        return Util.elixirBaseCost * System.Math.Pow(curr, Util.elixirScale);
    }

    public static double moneyRemainingNextElixir() {
        return costOfElixirs(elixirsOnReset() + 1) - Util.em.totalMoney;
    }

    public static double nextElixirCost() {
        long num = elixirsOnReset();
        return Util.elixirBaseCost * (System.Math.Pow(num + 1, Util.elixirScale) - System.Math.Pow(num, Util.elixirScale));
    }

    public void showResetWarning() {
        warning = Instantiate(warningPrefab);
        warning.transform.SetParent(Util.wm.canvas.transform);
        warning.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 400f);
        warning.transform.localScale = new Vector3(1f, 1f, 1f);
        warning.transform.SetAsLastSibling();
    }

    public static void reset() {

    }
}
