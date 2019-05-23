
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System;
using UnityEngine.UI;


public class CloudController : MonoBehaviour, ITrackableEventHandler
{
    SimpleCloudHandler simpleCloud;
    float starter;
    bool nofound;

    private TrackableBehaviour mTrackableBehaviour;
    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

        if (GameObject.FindGameObjectWithTag("cloudrec").GetComponent<SimpleCloudHandler>())
            simpleCloud = GameObject.FindGameObjectWithTag("cloudrec").GetComponent<SimpleCloudHandler>();

       
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
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

    private void OnTrackingLost()
        {
            nofound = true;
        }

    private void OnTrackingFound()
        {
            simpleCloud.StopCloudReco();
            starter = 0;
            nofound = false;
            Debug.Log("StopCloud");

    }

    void Update () {
        if (nofound) starter = starter + Time.deltaTime;
        if (starter > 5) {
                simpleCloud.StartCloudReco();
                nofound = false;
                Debug.Log("startCloud");
                starter = 0;
        }
        }
    }