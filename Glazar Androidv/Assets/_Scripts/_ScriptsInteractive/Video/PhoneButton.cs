using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhoneButton : MonoBehaviour {


    public string linkPhone;

    // Use this for initialization
    void Start()
    {
 
    }


    public void ToCallToLink()
    {
        Application.OpenURL("tel:" + linkPhone);
    }

}
