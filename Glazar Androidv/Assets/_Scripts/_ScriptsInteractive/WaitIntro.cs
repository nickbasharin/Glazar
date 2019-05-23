using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitIntro : MonoBehaviour {

    public MenuManager men;
	// Use this for initialization
	void Start () {
        StartCoroutine(vidosStop());

    }

    // Update is called once per frame
    void Update () {
		
	}
    IEnumerator vidosStop()
    {
        yield return new WaitForSeconds(3.0f);
        men.StartAR();
    }

}
