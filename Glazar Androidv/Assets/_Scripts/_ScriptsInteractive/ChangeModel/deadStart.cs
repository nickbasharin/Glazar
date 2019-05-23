using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadStart : MonoBehaviour {

    ModelSelect[] mode;
    // Use this for initialization
    void Start () {
        mode = GetComponentsInChildren<ModelSelect>();
        GameObject a = GameObject.FindGameObjectWithTag("deadbuttons");
        if (a == null)
        {
            a = GameObject.FindGameObjectWithTag("deadrobotbtns");
        }
        

        a.GetComponent<DeadButtons>().SetModels(mode);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
