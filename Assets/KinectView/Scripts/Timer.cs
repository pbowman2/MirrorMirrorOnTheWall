using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Timer : MonoBehaviour {
    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = System.DateTime.Now.ToString("HH:mm");
    }
}
