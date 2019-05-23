using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoToLink : MonoBehaviour {


    public string link;

    // Use this for initialization
    void Start()
    {
  
    }

   
    public void ToSite()
    {
        Application.OpenURL(link);
    }

}
