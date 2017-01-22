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
            EmanateRadius(startingRate);
        }
        else {
            foreach(Direction dir in dirs) {
                EmanateLine(dir, startingRate);
            }
        }
    }

    private void EmanateLine(Direction dir, double rate) {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        EmanateLine(dir, rate, pos, length);
    }

    private void EmanateLine(Direction dir, double rate, Vector2 startPos, int newLength) {
        double currentRate = startingRate;
        for(int i=0; i< newLength; i++) {
            currentRate -= i * rateDecrease;
            Vector2 newPos = startPos + Dir.toVec(dir) * i; //TODO make sure this actually works
            Collider2D hit = Physics2D.OverlapPoint(newPos, LayerMask.NameToLayer("Redirect Emanation"));
            if(hit != null) {
                RedirectEmanation redirect = hit.GetComponent<RedirectEmanation>();
                foreach (Direction dir2 in redirect.dirs) {
                    double newRate = (redirect.keepRate) ? rate : redirect.rate;
                    EmanateLine(dir2, newRate, newPos, redirect.length);
                }
            }
            else { //if didn't hit anything, create emanator
                
            }
        }
    }

    private void EmanateRadius(double rate) {

    }

    private void SpawnEmanator(Vector2 pos) {
        Vector3 pos3 = new Vector3(pos.x, pos.y, 0);
        EmanateComplacency newEmanator = GameObject.Instantiate<EmanateComplacency>(emanator, transform);
        newEmanator.transform.position = pos3;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
