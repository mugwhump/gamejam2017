using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSideways : MonoBehaviour {
    Rigidbody2D rbody;
    public float speed;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = new Vector2(-speed, 0);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
