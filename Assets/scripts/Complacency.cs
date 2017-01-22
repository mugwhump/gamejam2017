using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complacency : MonoBehaviour {
    public double maxComplacency;
    public double complacency;
    public GameObject maxTransform; //what this turns into when complacency is maxed
    public GameObject[] minTransform; //what this turns into when complacency empties. Array indexed by Conspiracy
    [HideInInspector]
    public double[] suspicions; //suspicion levels for each conspiracy, go up 
    public double suspicionReductionRate; //how much suspicion is reduced per second

    // Use this for initialization
    void Start () {
        suspicions = new double[(int)Conspiracy.NUM_CONSPIRACIES];
	}
	
	// Update is called once per frame
	void Update () {
        for(int i = 0; i < (int)Conspiracy.NUM_CONSPIRACIES; i++) {
            suspicions[i] -= Time.deltaTime * suspicionReductionRate;
        }
        if (complacency >= maxComplacency) {
            MaxOut();
        }
        if (complacency <= 0) {
            BottomOut();
        }
    }

    private void MaxOut() {
        GameObject newObj = Instantiate(maxTransform, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }

    private void BottomOut() {
        GameObject prefab = minTransform[0];
        if (minTransform.Length == (int)Conspiracy.NUM_CONSPIRACIES) {
            int biggestTheory = 0;
            for (int i = 0; i < (int)Conspiracy.NUM_CONSPIRACIES; i++) {
                if (suspicions[i] > suspicions[biggestTheory]) {
                    biggestTheory = i;
                }
            }
            prefab = minTransform[biggestTheory];
        }
        
        GameObject newObj = Instantiate(prefab, transform.position, transform.rotation, transform.parent);
        Debug.Log("DON'T PUSH ME MAN");
        Destroy(gameObject);
    }

    public void Placate(double amount, Conspiracy type) {
        complacency += amount;
        suspicions[(int)type] += amount;
    }
}
