using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SoccerInteractive : MonoBehaviour, ITrackableEventHandler
{


    public GameObject canvSoccer;
    public GameObject canvSoccer2;

    public GameObject hands;

    // Use this for initialization

    private TrackableBehaviour mTrackableBehaviour;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

    }

    private void OnTrackingFound()
    {
        canvSoccer.SetActive(true);
        canvSoccer2.SetActive(true);

        hands.SetActive(false);

    }

    private void OnTrackingLost()
    {
        if (canvSoccer != null)
        {
            //		hands.SetActive (true);

            canvSoccer.SetActive(false);
            canvSoccer2.SetActive(false);

        }

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
    void Update()
    {

    }
}