using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour {
    public Vector2 target; //where the plane will drop its payload
    public float flightSpeed; //speed in tiles/sec
    public SpawnEmanators payload;
    public Direction dir;

	// Use this for initialization
	void Start () {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        float angle = transform.rotation.eulerAngles.z; //GODDAM QUATERNIONS REEEEE
        Vector2 vec = Vector2FromAngle(angle); 
        rbody.velocity = vec;
    }

    public void SetDir(Direction d) {
        dir = d;
        payload.dirs = new Direction[1] { dir };
    }

    public Vector2 Vector2FromAngle(float a) {
        float r = a * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(r), Mathf.Sin(r));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
