using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    Text textt;
	// Use this for initialization
	void Start () {
        textt = GetComponentInChildren<Text>();
	}

    public void SetText(string txt) {
        textt.text = txt;
    }
}
