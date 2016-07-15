using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BabyManager : MonoBehaviour {
    public Sprite b1;
    public Sprite b2;
    public Sprite b3;

    public GameObject babyPrefab;

    ArrayList babies;

    public int babyCount = 0;
    public int maxbabies;

    void Awake() {
        babies = new ArrayList();
    }
	// Use this for initialization
	void Start () {
        
    }

    public void spawnbaby() {
        GameObject baby = Instantiate(babyPrefab);
        baby.transform.SetParent(transform);
        baby.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360f));
        baby.GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-680f, 680f), Random.Range(-310f, 310f), 0);
        baby.transform.localScale = new Vector3(1f, 1f, 1f);
        switch ((int)Random.Range(0, 2.99f)) {
            case 0: baby.GetComponent<Image>().sprite = b1; break;
            case 1: baby.GetComponent<Image>().sprite = b2; break;
            case 2: baby.GetComponent<Image>().sprite = b3; break;
        }
        babies.Add(baby);
        babyCount++;
    }

    public void eatBabies() {
        foreach (GameObject b in babies) {
            Destroy(b);
        }
        babyCount = 0;
    }
}
