using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLoseRiglaPanels : MonoBehaviour {

    public GameObject okomp;
    public GameObject spikeri;
    public GameObject oprosi;
    public GameObject map;
    public GameObject kontakti;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CloseAll() {
        okomp.SetActive(false);
        spikeri.SetActive(false);
        oprosi.SetActive(false);
        map.SetActive(false);
        kontakti.SetActive(false);

    }
}
