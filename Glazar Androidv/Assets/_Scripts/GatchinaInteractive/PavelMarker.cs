using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System;

public class PavelMarker : MonoBehaviour, ITrackableEventHandler  {

	private TrackableBehaviour mTrackableBehaviour;

	public GameObject gatchinaUI;
	float rastToOn = 3.5f;
	GatchinaMap gm;
	Vector3 rast;
	GameObject camera, pavel;
	bool tracking, noanimator, replay;
	float soundTimer;
	AudioSource audio;


	int pavelNumber;
	// Use this for initialization
	void Start () {
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
		camera = GameObject.FindGameObjectWithTag("MainCamera");
		noanimator = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (tracking) {
			rast = camera.transform.position - new Vector3(0, 0, 0);
			Debug.Log (rast.magnitude);
			Debug.Log ("ASD" + rastToOn);
			if ((noanimator)&&(GetComponentInChildren<Animator>())) {
				pavel = GetComponentInChildren<Animator> ().gameObject;
				noanimator = false;
				audio = GetComponentInChildren<AudioSource> ();
			}

			if (GetComponentInChildren<Animator>())
				{
					if (pavelNumber!=gm.GetPlayingPavel()){
						if (gm)
							gm.PlayerManager (audio, pavelNumber);
					
					}
				}


			if (rast.magnitude<rastToOn)
			{
				if (gm) {
					 gm.OnOffGetOutPanel (true);
					if (pavel != null) {
						pavel.SetActive (false);
					}
						if (gm.mapopened) {
						gm.OnOffGetOutPanel (false);
						}
					
				}
			}

			if (rast.magnitude>rastToOn)
			{
				if (gm) {
					gm.OnOffGetOutPanel (false);
					if (pavel!=null)  {pavel.SetActive (true);}

				}
			}	
		}
	}

	private void OnTrackingFound()
	{
		gatchinaUI.SetActive (true);
		gm = gatchinaUI.GetComponent<GatchinaMap> ();
		MenuManager mm = GameObject.FindGameObjectWithTag ("manager").GetComponent<MenuManager> ();
		rastToOn =  mm.GetRastGatchina();
		StartCoroutine (DownloadSubs());
		if (mTrackableBehaviour.TrackableName.Equals ("pavelg1tank")) pavelNumber = 1;
		if (mTrackableBehaviour.TrackableName.Equals ("pavelg2tank")) pavelNumber = 2;
		if (mTrackableBehaviour.TrackableName.Equals ("pavelg3tank")) pavelNumber = 3;
		if (mTrackableBehaviour.TrackableName.Equals ("pavelg4tank")) pavelNumber = 4;
		if (mTrackableBehaviour.TrackableName.Equals ("pavelg5tank")) pavelNumber = 5;
		if (mTrackableBehaviour.TrackableName.Equals ("pavelg6tank")) pavelNumber = 6;
		if (mTrackableBehaviour.TrackableName.Equals ("pavelg7tank")) pavelNumber = 7;
		if (mTrackableBehaviour.TrackableName.Equals ("pavelg8tank")) pavelNumber = 8;
		if (mTrackableBehaviour.TrackableName.Equals ("pavelg9tank")) pavelNumber = 9;
		if (mTrackableBehaviour.TrackableName.Equals ("pavelg10tank")) pavelNumber = 10;
		if (mTrackableBehaviour.TrackableName.Equals ("pavelg11tank")) pavelNumber = 11;
		if (mTrackableBehaviour.TrackableName.Equals ("pavelg12tank")) pavelNumber = 12;

		if (PlayerPrefs.GetInt ("Pavel" + pavelNumber) != 1) {
			PlayerPrefs.SetInt ("Pavel" + pavelNumber, 1);
			PlayerPrefs.Save ();
			mm.SendEmail (mm.GetGatchinaMail (), "Просмотр у таблички № " + pavelNumber, "Новый просмотр у таблички № " + pavelNumber+". Устройство на ОС Android.");
		}
		tracking = true;
		PlayerPrefs.SetInt ("StartGlaz", 2);

/*		if (PlayerPrefs.GetInt("pavel" + pavelNumber) != 1)
		{
			//Send email here
			PlayerPrefs.SetInt ("pavel" + pavelNumber, 1);
			PlayerPrefs.Save();
		}*/
	}

	private void OnTrackingLost()
	{
		tracking = false;
		if (gm) {
			gm.OnOffGetOutPanel (false);
		}
		replay = true;
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

	IEnumerator DownloadSubs(){
		string rutext, entext;
		WWW www = new WWW ("http://glazar.pa.infobox.ru/ar/GlazAR/gatchina/" + mTrackableBehaviour.TrackableName + "r.txt");
		while (!www.isDone) {
			yield return null;
		}
		if (string.IsNullOrEmpty (www.error)) {
			 rutext = www.text;
		} else
			rutext = "";
		
		WWW www2 = new WWW("http://glazar.pa.infobox.ru/ar/GlazAR/gatchina/" + mTrackableBehaviour.TrackableName + "e.txt");
		while (!www2.isDone)
		{
			yield return null;
		}

		if (string.IsNullOrEmpty (www2.error)) {
			entext = www2.text;
		} else
			entext = "";

		gm.SetSubs (rutext, entext);
		gm.SetPoint (pavelNumber);

		www.Dispose ();
		www2.Dispose ();

	}


}
