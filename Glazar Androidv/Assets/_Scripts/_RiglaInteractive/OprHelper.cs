using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OprHelper : MonoBehaviour {

    public GameObject errorText;
    public GameObject nextPanel;
    public OprosManager om;


    public Text mainText;

    public Text ansText1;
    public Text ansText2;
    public Text ansText3;

    public string oprosN;

    string answer;
    int ans;
	// Use this for initialization
	void Start () {
        ans = -1;
	}
	

	// Update is called once per frame
	void Update () {
		
	}

    public void answer1() {
        errorText.SetActive(false);
        answer = ansText1.text;
        ans = 1;
    }
    public void answer2() {
        errorText.SetActive(false);
        answer = ansText2.text;
        ans = 2;

    }
    public void answer3() {
        errorText.SetActive(false);
        answer = ansText3.text;
        ans = 3;

    }

    public void Answering() {
        if (ans > 0)
        {
            nextPanel.SetActive(true);
            string oprosAnswer = "Ответ на вопрос №" + oprosN + " :\n" + mainText.text + "\n" + answer + "\n\n";
            om.SetAnswer(oprosAnswer, oprosN);

        }
        else {
            errorText.SetActive(true);
        }
    }
}
