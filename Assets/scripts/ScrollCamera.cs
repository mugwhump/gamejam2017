using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamera : MonoBehaviour {
	public int scrollDistance; 
	public float scrollSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float mousePosX = Input.mousePosition.x; 
		float mousePosY = Input.mousePosition.y; 
		if (mousePosX < scrollDistance) { 
			transform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime); 
		} 

		if (mousePosX >= Screen.width - scrollDistance) { 
			transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime); 
		}

		if (mousePosY < scrollDistance) { 
			transform.Translate(Vector3.up * -scrollSpeed * Time.deltaTime); 
		} 

		if (mousePosY >= Screen.height - scrollDistance) { 
			transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime); 
		}
	}

}
