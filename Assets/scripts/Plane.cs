using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour {
    public Vector2 target; //where the plane will drop its payload
    public float flightSpeed; //speed in tiles/sec
    public SpawnEmanators payload;

	// Use this for initialization
	void Start () {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = new Vector2(flightSpeed, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
