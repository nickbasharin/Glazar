using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class OprosManager : MonoBehaviour {

    bool oprosiGo;
    public bool notTimeToGo;
    public GameObject[] oprosi;
    public GameObject panelProydeni;
    public mono_gmail mailSender;

    public string mailForOpros;
    int nowOpros;

    
   // public GameObject openedOpros;
    bool timeToSend;
    string fullAnswer;

	void Start () {
        nowOpros = 0;
        fullAnswer = "";
        string s;
        for(int i = 1; i <= oprosi.Length; i++)
        {
            s = "" + i;
            if (!PlayerPrefs.HasKey("opros" + s)) {
                oprosi[i-1].SetActive(true);
                nowOpros = i-1;
                fullAnswer = PlayerPrefs.GetString("FAnswer");
                oprosiGo = true;
                break;
            }
        }

        if (!oprosiGo) { panelProydeni.SetActive(true); }
    }

    // Update is called once per frame
    void Update () {

    }

    public void SetAnswer(string answer, string oprNumber) {
        PlayerPrefs.SetInt("opros" + oprNumber, 1);
        fullAnswer = fullAnswer + answer;
        PlayerPrefs.SetString("FAnswer", fullAnswer);
        PlayerPrefs.Save();
        Debug.Log(fullAnswer);
        if (!((nowOpros + 1) < oprosi.Length)) mailSender.SendMail(mailForOpros, "Ответы к сессии", fullAnswer);

    }

    public void nextOpros() {
        oprosi[nowOpros].SetActive(false);
        nowOpros++;
        if (nowOpros < oprosi.Length)
        {
            oprosi[nowOpros].SetActive(true);
        }
        else {
            SendAnswers();
        }
    }

    void SendAnswers() {
        gameObject.SetActive(false);
    }

    public void ClearPrefs() {
        PlayerPrefs.DeleteAll();
        
    }
}
