using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrizovoyOpros : MonoBehaviour {

    public mono_gmail mailSender;

    string name;
    string phone;
    bool answerGood;
    public Text nam;
    public Text phon;
    public MenuManager manager;

    public GameObject enterName;
    public GameObject enterError;
    public GameObject closePanel;

    void Start () {
        phone = "";
        name = "";

    }
	

	void Update () {
		
	}

    public void trueAnswer(bool ans)
    {
        answerGood = ans;
    }

    public void NameInput() {
        name = nam.text;
    }

    public void PhoneInput()
    {
        phone = phon.text;
    }

    public void sendData() {
        string mailtosent = manager.GetRiglaMail();
        if (phone.Length > 0 && name.Length > 0)
        {

            if (answerGood)
            {
                mailSender.SendMail(mailtosent, "Имя и телефон правильно ответившего на викторину", name + "\n" + phone);
            }
            PlayerPrefs.SetInt("PrizGone", 1);
            PlayerPrefs.Save();
            closePanel.SetActive(true);
            enterName.SetActive(false);

        }
        else {
            enterError.SetActive(true);
        }
    }

}
