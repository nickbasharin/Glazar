using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class deadpoolInteractive : MonoBehaviour, ITrackableEventHandler
{

    public string tagbtns;
    GameObject canvDead;
    GameObject hands;

    // Use this for initialization

    private TrackableBehaviour mTrackableBehaviour;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
        hands = GameObject.FindGameObjectWithTag("hands");
        canvDead = GameObject.FindGameObjectWithTag(tagbtns);

    }

    private void OnTrackingFound()
    {
       if (canvDead!=null) canvDead.SetActive(true);
        hands.SetActive(false);
    }

    private void OnTrackingLost()
    {
        if (canvDead != null)
        {
            //		hands.SetActive (true);

            canvDead.SetActive(false);
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
