using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComplacencyUI : MonoBehaviour {
    private Complacency subject; //the one this script's attached to
    private Canvas canvas; //The canvas. Needs "Canvas" tag

    public GameObject complacencyPrefab; //complacency bar prefab. Will be instantiated for everyone that needs it.

    public float complacencyPanelOffset = 0.35f; //how high (world space coords) above subject the panel should appear
    public GameObject complacencyPanel; //instantiation of complacencyPrefab
    private Slider complacencySlider;
    // Use this for initialization
    void Start () {
        subject = GetComponent<Complacency>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        complacencyPanel = Instantiate<GameObject>(complacencyPrefab);
        complacencyPanel.transform.SetParent(canvas.transform, false);
        complacencySlider = complacencyPanel.GetComponentInChildren<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        complacencySlider.value = (float)subject.complacency / (float)subject.maxComplacency;
        Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + complacencyPanelOffset, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        complacencyPanel.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
        //complacencyPanel.transform.position = new Vector3(transform.position.x, transform.position.y + complacencyPanelOffset, transform.position.z);
    }
}
