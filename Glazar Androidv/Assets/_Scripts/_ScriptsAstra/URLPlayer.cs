using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class URLPlayer : MonoBehaviour {
    private int i;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayURL(string URLString) {
        Debug.Log("app.openURL:"+URLString);
        Application.OpenURL(URLString);
    }
}
