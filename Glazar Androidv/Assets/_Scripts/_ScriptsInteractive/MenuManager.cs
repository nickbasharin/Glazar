using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class MenuManager : MonoBehaviour {
	public GameObject mainCanv;
	public GameObject riglaCanv;
	public GameObject gatchinaUI;
	float rastGatchina;
    public GameObject oprosi;
    public GameObject oprosiNoTime;

    string riglaEmail;
	string gatchinaEmail;
	mono_gmail mailSender;
    string startTime;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("StartGlaz") == 1)
        {
            mainCanv.SetActive(false);
            riglaCanv.SetActive(true);
        }
		if (PlayerPrefs.GetInt("StartGlaz") == 2)
		{
			gatchinaUI.SetActive (true);
		}

		mailSender = GetComponent<mono_gmail> ();
        StartCoroutine(GetMail());
        StartCoroutine(GetStartTime());
		StartCoroutine(RastGatchina());


    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
             Application.Quit();
             return;
            }
        }
    }

	public void StartAR(){
		//loading.SetActive (true);
		SceneManager.LoadScene ("ARScene");
	}
	 public void BackToMenu(){
		SceneManager.LoadScene ("Menu");
	}
	public void ToSite(string site){
		Application.OpenURL(site);
        
	}

    IEnumerator GetMail()
    {
        WWW www = new WWW("http://glazar.pa.infobox.ru/ar/GlazAR/email/riglaMail.txt");
        while (!www.isDone)
        {
            yield return null;
        }
        if (string.IsNullOrEmpty(www.error))
        {
            riglaEmail = www.text;
        }
        www.Dispose();

		WWW www2 = new WWW("http://glazar.pa.infobox.ru/ar/GlazAR/email/gatchinaMail.txt");
		while (!www2.isDone)
		{
			yield return null;
		}
		if (string.IsNullOrEmpty(www2.error))
		{
			gatchinaEmail = www2.text;
		}
		www2.Dispose();

    }

    IEnumerator GetStartTime()
    {
        WWW www = new WWW("http://glazar.pa.infobox.ru/ar/GlazAR/timer/time.txt");
        while (!www.isDone)
        {
            yield return null;
        }
        if (string.IsNullOrEmpty(www.error))
        {
            startTime = www.text;
        }
        else {
            startTime = "2018 / 10 / 06 18:05:00";
        }
        www.Dispose();
    }

    public void StartOprosi() {
        Debug.Log(DateTime.Now.ToString());
        DateTime parsedDate = DateTime.Parse(startTime);
        OprosManager om = oprosi.GetComponent<OprosManager>();
        om.mailForOpros = riglaEmail;

        if (oprosi.activeSelf) oprosi.SetActive(false);
        else {
            if (DateTime.Now > parsedDate)
            {
                oprosi.SetActive(true);
            }
            else
            {
                oprosiNoTime.SetActive(true);
            }
        }
       // if (oprosiNoTime.activeSelf) oprosiNoTime.SetActive(false);
    }

	IEnumerator RastGatchina()
	{
		WWW www = new WWW("http://glazar.pa.infobox.ru/ar/GlazAR/email/gatchinaRast.txt");
		while (!www.isDone)
		{
			yield return null;
		}
		if (string.IsNullOrEmpty (www.error)) {
			rastGatchina = float.Parse (www.text);
		} else
			rastGatchina = 3.5f;
		www.Dispose();

	}



    public string GetRiglaMail() {
        return riglaEmail;
    }
	public string GetGatchinaMail() {
		return gatchinaEmail;
	}

	public void GatchinaUI(bool b){
		gatchinaUI.SetActive (b);
	}
    
	public float GetRastGatchina(){
		return rastGatchina;
	}

	public void SendEmail(string mail, string subject, string text){
		if (!mail.Equals ("0")) {
			mailSender.SendMail (mail, subject, text);
		}

	}
	/*public void doIntro(){
		GameObject intro1 = Instantiate (intro);
		FallingIntro falin = intro1.GetComponentInChildren<FallingIntro> ();
		falin.hands = hand;
	}*/
}
