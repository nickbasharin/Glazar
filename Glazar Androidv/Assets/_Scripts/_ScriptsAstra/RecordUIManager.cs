using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordUIManager: MonoBehaviour {

    public Image buttonOn;
    public Image buttonOff;
    public Image recMark;

    void Start () {
	}

    public void Change(bool recordOn) {
        if (recordOn)
        {
            Debug.Log("RUIM. recordOn");
            recMark.enabled = true;
            buttonOn.enabled = false;
            buttonOff.enabled = true;
        }
        else {
            Debug.Log("RUIM. recordOff");
            recMark.enabled = false;
            buttonOff.enabled = false;
            buttonOn.enabled = true;
        }
        }
    
}
