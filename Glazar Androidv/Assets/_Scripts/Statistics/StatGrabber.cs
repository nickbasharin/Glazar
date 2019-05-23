using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class StatGrabber : MonoBehaviour

{

    String startViewTime;
    String stopViewTime;
    DateTime startDT;
    TimeSpan watchTime;

    public InitStat initstat;
    Vector3 rastInit;
    double angle;
    bool tracking;

    int counter;
    double angleSum;
    float distanceSum;

    GameObject camera;

   // private TrackableBehaviour mTrackableBehaviour;

    void Start()
    {
      /*  mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }*/

        camera = GameObject.FindGameObjectWithTag("MainCamera");
     //   initstat = GameObject.FindGameObjectWithTag("manager").GetComponent<InitStat>();

    }
/*
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

    private void OnTrackingFound()
    {
        StartWatching(0);
    }


    private void OnTrackingLost()
    {
        StopWatching(0);
    }*/

    public void StartWatching(int reason) {
        counter = 0;
        distanceSum = 0;
        angleSum = 0;
        startDT = DateTime.Now;
        startViewTime = DateTime.Now.ToString();
        tracking = true;
        if (camera == null) Debug.Log("NO CAMERA BLYAT");
        rastInit = camera.transform.position - new Vector3(0, 0, 0);
        GetAngle();
        Debug.Log("Start reason = " + reason +",  Start Distance: " + rastInit.magnitude + ", Start Angle:" + RadianToDegree(angle));
    }


    public void StopWatching(int reason)
    {

        tracking = false;
        //   stopViewTime = DateTime.Now.ToString();
        watchTime = DateTime.Now.Subtract(startDT);
        Debug.Log(startViewTime);
        /*string a = initstat.getStartTime();
        Debug.Log(a);*/
//        Debug.Log("Stop Reason = " + reason + ",  Average Distance: " + (distanceSum/counter) + ", Average Angle:" + RadianToDegree(angleSum / counter) + ",  View Time: " + watchTime.ToString());

    }

    void Update()
    {
        if (tracking) {
            GetAngle();
            angleSum = angleSum + angle;
            distanceSum = distanceSum + camera.transform.position.magnitude;
            counter++;
        }
    }

    void GetAngle() {
        Vector3 projectionXOZ, vectorToProjection;

        projectionXOZ = new Vector3(camera.transform.position.x, 0, camera.transform.position.z);
        vectorToProjection = new Vector3(0, 0, 0) - projectionXOZ;
        float distanceToProjection = vectorToProjection.magnitude;
        float projectionY = camera.transform.position.y;

        angle = Math.Atan(projectionY / distanceToProjection);
    }

    private double RadianToDegree(double a)
    {
        return Math.Round(a * (180.0 / Math.PI), 1);
    }
}
