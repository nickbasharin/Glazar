using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System;
using UnityEngine.UI;


public class hdel : MonoBehaviour, ITrackableEventHandler
{

    public GameObject handss;
    private TrackableBehaviour mTrackableBehaviour;

    // Use this for initialization
    void Start () {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}


    public void OnTrackableStateChanged(
    TrackableBehaviour.Status previousStatus,
    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            handss.SetActive(false);
        }


    }

}
