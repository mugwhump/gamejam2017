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
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, 0);
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Placed product at "+pos.ToString());
            caller.TaskOnSecondClick();
            Destroy(this);
        }
	}

    void OnDestroy() {
        MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in comps) {
            c.enabled = true;
        }
    }
    
}
