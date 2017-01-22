using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Spawns complacency emanating tiles. Runs on creation.
//Manually re-run when terrain changes.
public class SpawnEmanators : MonoBehaviour {
    public Emanator emanator;
    //TODO: determine river direction
    //TODO: stop dishes from getting redirected by river turns. add enum for things that can redirect
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
        double currentRate = rate;
        for(int i=0; i< newLength; i++) {
            currentRate -= rateDecrease;
            Vector2 newPos = startPos + Dir.toVec(dir) * i; //TODO make sure this actually works
            Collider2D hit = RedirectorAt(newPos);
            if(i > 0 && hit != null) { //don't emanate on first step of line
                RedirectEmanation redirect = hit.GetComponent<RedirectEmanation>();
                Redirect(redirect, currentRate);
                break; //stop emanating along the old path
            }
            else { //if didn't hit anything, create emanator
                SpawnEmanator(newPos, currentRate);
            }
        }
    }

    private void EmanateRadius(double rate) {
        Vector2 thisVec = new Vector2(transform.position.x, transform.position.y);
        float minX = transform.position.x - (length / 2.0f);
        float maxX = minX + length;
        float minY = transform.position.y - (length / 2.0f);
        float maxY = minY + length;
        for (float x = minX; x <= maxX; x += 1) {
            for (float y = minY; y <= maxY; y += 1) {
                Vector2 vec = new Vector2(x, y);
                float dist = Vector2.Distance(vec, thisVec);
                if(dist <= length/2) {
                    RaycastHit2D hit = Physics2D.Linecast(thisVec, vec, 1 << LayerMask.NameToLayer("Mountains"));
                    //TODO: could use this linecast as a path for wave graphics to follow
                    if(hit.collider == null) { //no mountains in the way
                        double currentRate = rate - dist * rateDecrease;
                        SpawnEmanator(vec, currentRate);
                        //check for dishes. TODO: that limits radial stuff to towers only
                        Collider2D collider = RedirectorAt(vec);
                        if (collider != null && collider.gameObject.CompareTag("Dish")) {
                            Redirect(collider.GetComponent<RedirectEmanation>(), currentRate);
                        }

                    }
                }
            }
        }
    }

    private void Redirect(RedirectEmanation redirect, double rate) {
        foreach (Direction dir2 in redirect.dirs) {
            double newRate = (redirect.keepRate) ? rate : redirect.rate;
            if (newRate > 0 && redirect.length > 0) {
                Vector2 pos = new Vector2(redirect.transform.position.x, redirect.transform.position.y);
                EmanateLine(dir2, newRate, pos, redirect.length);
            }
        }
    }

    //returns redirector at position (null if nothing). Mountains are redirectors.
    private Collider2D RedirectorAt(Vector2 pos) {
        return Physics2D.OverlapPoint(pos, (1 << LayerMask.NameToLayer("Redirect Emanation")) | (1 << LayerMask.NameToLayer("Mountains")));
    }

    private void SpawnEmanator(Vector2 pos, double rate) {
        Vector3 pos3 = new Vector3(pos.x, pos.y, 0);
        Emanator newEmanator = GameObject.Instantiate<Emanator>(emanator, transform);
        newEmanator.transform.position = pos3;
        newEmanator.rate = rate;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
