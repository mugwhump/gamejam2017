using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Increases complacency of whatever collides with this object
public class Emanator : MonoBehaviour {
    public double rate; //complacency gained per second
    public Conspiracy type; //is this waves, fluoride, or chemtrails

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other) {
        Complacency target = other.GetComponent<Complacency>();
        if (target) {
            target.Placate(rate * Time.deltaTime, type);
        } 
    }
}
