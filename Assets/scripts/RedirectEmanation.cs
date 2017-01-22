using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//changes direction of complacency emanations
//For example, satellites, bends/splits in rivers
//Mountains redirect emanations nowhere
//TODO: better to use rotation than Direction, since prefabs ignore rotation
public class RedirectEmanation : MonoBehaviour {
    public Direction[] dirs; //which direction complacency emanators should be spawned
    public int length; //how many tiles this goes for
    public double rate; //emanation rate.
    public bool keepRate; //if true just keeps the current rate (used for bends in rivers)

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
