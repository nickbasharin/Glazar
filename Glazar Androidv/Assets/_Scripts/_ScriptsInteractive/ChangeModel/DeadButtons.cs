using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeadButtons : MonoBehaviour {


    Button [] buttons;
    ModelSelect[] models;


    // Use this for initialization
    void Start ()
    {
        buttons = GetComponentsInChildren<Button>();
        if (buttons[0] != null) buttons[0].onClick.AddListener(ButtonAct1);
        if (buttons[1] != null) buttons[1].onClick.AddListener(ButtonAct2);
        if (buttons[2] != null) buttons[2].onClick.AddListener(ButtonAct3);
        //    if (buttons[3] != null) buttons[3].onClick.AddListener(ButtonAct4);
        foreach (Button b in buttons) b.gameObject.SetActive(false);
    }


    public void SetModels(ModelSelect[] mod) {
        models = mod;
        foreach (Button b in buttons) b.gameObject.SetActive(true);
        ButtonAct1();
    }

    void OffModels() {
        foreach (ModelSelect m in models) {
            m.gameObject.SetActive(false);
        }
    }

    public void ButtonAct1() {
        if (models != null)
        {
            OffModels();
            if (models[0] != null) models[0].gameObject.SetActive(true);
        }
    }

    public void ButtonAct2()
    {
        if (models != null)
        {
            OffModels();
            if (models[1] != null) models[1].gameObject.SetActive(true);
        }
    }

    public void ButtonAct3()
    {
        if (models != null)
        {
            OffModels();
            if (models[2] != null) models[2].gameObject.SetActive(true);
        }
    }

   /* public void ButtonAct4()
    {
        if (models != null)
        {
            OffModels();
            if (models[3] != null) models[3].gameObject.SetActive(true);
        }
    }*/


}
