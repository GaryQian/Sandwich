using UnityEngine;
using System.Collections;

public class ResetManager : MonoBehaviour {

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
}
