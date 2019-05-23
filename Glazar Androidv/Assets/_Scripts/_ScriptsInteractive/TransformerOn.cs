using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TransformerOn : MonoBehaviour, ITrackableEventHandler{


	public GameObject canvTr;
	public GameObject hands;

	public AudioSource au;
	public Animator anim;
	// Use this for initialization

	private TrackableBehaviour mTrackableBehaviour;

	void Start () {
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}

	}

	private void OnTrackingFound()
	{
		canvTr.SetActive (true);
		hands.SetActive (false);

	}

	private void OnTrackingLost()
	{
		if (canvTr != null) {
	//		hands.SetActive (true);

			canvTr.SetActive (false);
		}
		au.Stop ();
		anim.SetBool ("tr", false);

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

	// Update is called once per frame
	void Update () {
		
	}
}
