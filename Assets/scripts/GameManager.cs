using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public int money = 1000;
    public GameObject newsMemes;

	// Use this for initialization
	void Awake () {
        newsMemes.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
