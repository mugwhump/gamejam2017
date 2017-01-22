using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Spawns complacency emanating tiles. Runs on creation.
//Manually re-run when terrain changes.
public class SpawnEmanators : MonoBehaviour {
    public EmanateComplacency emanator;
    //TODO: determine river direction
    public Direction[] dirs; //which direction complacency emanators should be spawned
    public bool radiate = false; //if true, radiates in a circle around point, ignoring dirs variable
    public double startingRate; //emanation rate at origin. 
    public int length; //how far effect can go before running out
    public bool constantRate = false; //if true, rate will stay the same, and emanations will just disappear once they go far enough.
    private double rateDecrease; //how much the rate decreases for each tile moved


	// Use this for initialization
	void Start () {
        rateDecrease = startingRate / length;
        Spawn();
	}

    public void Spawn() {
        Vector3 startingPos = transform.position;
        if(radiate) {
            emanateRadius(startingRate);
        }
        else {
            foreach(Direction dir in dirs) {
                emanateLine(dir, startingRate);
            }
        }
    }

    private void emanateLine(Direction dir, double rate) {
        double currentRate = startingRate;
        for(int i=0; i< length; i++) {
            currentRate -= i * ()
        }
    }

    private void emanateRadius(double rate) {

    }

    // Update is called once per frame
    void Update () {
		
	}
}
