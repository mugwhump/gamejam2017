using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Increases complacency of whatever collides with this object
public class EmanateComplacency : MonoBehaviour {
    public double rate; //complacency gained per second

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other) {
        Complacency target = other.GetComponent<Complacency>();
        if (target) {
            Debug.Log("COMPLACIFIED");
        }
    }
}
