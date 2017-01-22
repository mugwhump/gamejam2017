using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This behaviour is attached to a product (tower, fluoride plant, etc) while it's being dragged around
//It disables all behaviors besides the sprite and waits for the user to click to place it somewhere, then 
//re-enables everything and removes itself from the object.
public class ButtonProduct : MonoBehaviour {
    public ClickButton caller;

	// Use this for initialization
	void Awake () {
        Debug.Log("ButtonProd here");
        MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in comps) {
            if(c != this) c.enabled = false;
        }
        GetComponent<SpriteRenderer>().enabled = true;
    }

    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float x = Mathf.Round(pos.x + .5f) - .5f;
        float y = Mathf.Round(pos.y + .5f) - .5f;
        transform.position = new Vector3(x, y, 0);
        if (Input.GetMouseButtonDown(0)) {
            Reenable();
            caller.TaskOnSecondClick();
            Destroy(this);
        }
        if (caller.rotateable && Input.GetMouseButtonDown(1)) {
            caller.dir = Dir.getCounterClockwise(caller.dir);
            Debug.Log(caller.dir.ToString());
            transform.Rotate(Vector3.forward * 90);
        }
    }

    void Reenable() {
        MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in comps) {
            c.enabled = true;
        }
    }
    
}
