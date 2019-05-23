using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System;
using UnityEngine.UI;


public class HandsDeleter : MonoBehaviour, ITrackableEventHandler {
	public GameObject hands;
    public GameObject mainCanv;
    public GameObject riglaCanv;
    public bool itCloud;

    PhoneButton phoneButton;
    GoToLink gotolink;
    ModelSelect infoMarker;
    InfoPanel infoPanel;
    GameObject phonePanel;
    GameObject linkPanel;
    GameObject infoButton;
    SimpleCloudHandler simpleCloud;

    String bText;
    String bLink;

    private TrackableBehaviour mTrackableBehaviour;

	void Start () {
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}

        simpleCloud = GameObject.FindGameObjectWithTag("cloudrec").GetComponent<SimpleCloudHandler>();

        phoneButton = GetComponentInChildren<PhoneButton>();
        gotolink = GetComponentInChildren<GoToLink>();
        infoMarker = GetComponentInChildren<ModelSelect>();
        infoPanel = GetComponentInChildren<InfoPanel>();
        phonePanel = phoneButton.gameObject;
        linkPanel = gotolink.gameObject;
        infoButton = infoMarker.gameObject;
        
        phonePanel.SetActive(false);
        linkPanel.SetActive(false);
        infoButton.SetActive(false);
    }

    private void OnTrackingFound()
	{
        if (!itCloud) { StartCoroutine(DownloadText()); }
        else {
            StartCoroutine(DownloadFromAPI());
        }
        hands.SetActive(false);

        if (mTrackableBehaviour.TrackableName.Contains("rigla"))
        {
            PlayerPrefs.SetInt("StartGlaz", 1);
            PlayerPrefs.SetInt("rigla" + mTrackableBehaviour.TrackableName.Substring(13, 1), 1);
            PlayerPrefs.Save();
            mainCanv.SetActive(false);
            riglaCanv.SetActive(true);
            riglaCanv.GetComponentInChildren<StarCollector>().starCount();
           // Debug.Log("rigla" + mTrackableBehaviour.TrackableName.Substring(13, 1));
        }
        else
        {
			

            mainCanv.SetActive(true);
            riglaCanv.SetActive(false);
            PlayerPrefs.SetInt("StartGlaz", 0);
            PlayerPrefs.Save();
        }
		if (GameObject.FindGameObjectWithTag ("manager").GetComponent<MenuManager> ())
			GameObject.FindGameObjectWithTag ("manager").GetComponent<MenuManager> ().GatchinaUI (false);
		
    }
    private void OnTrackingLost()
	{
        //		hands.SetActive (true);

    }

    public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			OnTrackingFound();
		}
		else
		{
			OnTrackingLost();
		}

	}

    IEnumerator DownloadText()
    {
        
     //   Debug.Log("http://glazar.pa.infobox.ru/ar/GlazAR/Video/" + mTrackableBehaviour.TrackableName + "textb.txt");
        WWW www = new WWW("http://glazar.pa.infobox.ru/ar/GlazAR/Video/" + mTrackableBehaviour.TrackableName + "textb.txt");

        while (!www.isDone)
        {
            yield return null;
        }


        if (string.IsNullOrEmpty(www.error))
        {
            bText = www.text;
            phoneButton.linkPhone = bText;
         //   Debug.Log(bText);
            phonePanel.SetActive(true);
        }

        WWW www2 = new WWW("http://glazar.pa.infobox.ru/ar/GlazAR/Video/" + mTrackableBehaviour.TrackableName + "linkb.txt");
        while (!www2.isDone)
        {
            yield return null;
        }
        if (string.IsNullOrEmpty(www2.error))
        {
            bLink = www2.text;
            gotolink.link = bLink;
            linkPanel.SetActive(true);
        }

        WWW www3 = new WWW("http://glazar.pa.infobox.ru/ar/GlazAR/Video/" + mTrackableBehaviour.TrackableName + "infob.txt");
        while (!www3.isDone)
        {
            yield return null;
        }
        if (string.IsNullOrEmpty(www3.error))
        {
            bLink = www3.text;
            infoPanel.SetText(bLink);
            infoButton.SetActive(true);
        }
        www.Dispose();
        www2.Dispose();
        www3.Dispose();
    }
    
    IEnumerator DownloadFromAPI()
    {
        string url = "http://www.glazar.pro/api/marker/" + mTrackableBehaviour.TrackableName;
        WWW www = new WWW(url);
        while (!www.isDone)
        {
            yield return null;
        }

        string alltext = www.text;
       // Debug.Log(alltext);

        int linkStop = alltext.IndexOf("phoneNumber");
       // Debug.Log(linkStop);
        int startCount = 18;
        string link = alltext.Substring(startCount, linkStop - 21);
       // Debug.Log(link);
        if (!link.Equals("ul"))
        {
            if (link.Contains("http"))
            {
                gotolink.link = link;
            }
            else
            {
                gotolink.link = "http://" + link;
            }
            linkPanel.SetActive(true);
        }

        int phoneStop = alltext.IndexOf("description");
        startCount = 18 + link.Length + 17;
        string phone = alltext.Substring(startCount, phoneStop - startCount - 3);
      //  Debug.Log(phone);
        if (!phone.Equals("ul"))
        {
            phoneButton.linkPhone = phone;
            phonePanel.SetActive(true);
        }

        startCount = 18 + link.Length + 17 + phone.Length + 17;
        string description = alltext.Substring(startCount, alltext.Length - startCount - 2);
       /* Debug.Log(startCount);
        Debug.Log(alltext.Length);
        Debug.Log(description);*/

        if (!description.Equals("ul"))
        {
            infoPanel.SetText(description);
            infoButton.SetActive(true);
        }

        //  { "targerAddress":"crab","phoneNumber":"111","description":"lalala"}
    }
}
