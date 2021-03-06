﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour {
    public GameObject product; //what this button creates
    public int cost; //how much it costs
    public double cooldown; //how long is the cooldown in seconds
    public bool rotateable = false; //whether object can be rotated
    public Direction startingDir = Direction.RIGHT; //direction that matches the unrotated sprite
    public Direction dir;

    private Button mButton;
    private GameManager gm;
    private double remainingTime; //how long until this can be clicked again

	// Use this for initialization
	void Start () {
        mButton = GetComponent<Button>();
        mButton.onClick.AddListener(TaskOnClick);
        gm = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(remainingTime > 0) {
            remainingTime -= Time.deltaTime;
        }
	}

    void TaskOnClick() {
        Debug.Log("Button Task doin");
        if (gm.money < cost) {
            Debug.Log("YOU TOO POOR");
        }
        else if (remainingTime > 0) {
            Debug.Log("STILL IN COOLDOWN");
        }
        else {
            //charge them once they actually build it
            dir = startingDir;
            GameObject prod = Instantiate(product);
            ButtonProduct bp = prod.AddComponent<ButtonProduct>();
            bp.caller = this;
        }
    }

    public void TaskOnSecondClick() {
        Debug.Log("Click2");
        gm.money -= cost;
        remainingTime = cooldown;
        if (rotateable) {
            Plane plane = product.GetComponent<Plane>();
            //Debug.Log("Spawner is " + spawner.ToString());
            if (plane != null) {
                plane.SetDir(dir);
                //Debug.Log("Facing " + plane.payload.dirs.ToString());
            }
            RedirectEmanation redirector = product.GetComponent<RedirectEmanation>();
            if (redirector != null) {
                redirector.dirs = new Direction[1] { dir };
                Debug.Log("Facing " + redirector.dirs.ToString());
            }
        }
    }
}
