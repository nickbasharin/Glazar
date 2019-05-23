using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InitStat : MonoBehaviour {

    private string startTime;
    public static String userId;
    public static String startId;


    void Start () {
            startTime = DateTime.Now.ToString();
        /*    if (PlayerPrefs.GetInt("FirstStart") == 0)
            {
                StartCoroutine(GetUserId());
                PlayerPrefs.SetInt("FirstStart", 1);
            }
            else {
                userId = PlayerPrefs.GetString("UserId");
            }*/
        userId = "testUserID";
    }

    void Update () {
        if ((userId != null)&&(startId == null)) {
            startId = "testId";
            //   StartCoroutine(AddStart());
        }
    }

    /* IEnumerator GetUserId()
     {
         string url = "http://www.glazar.pro/api/marker/" + mTrackableBehaviour.TrackableName;
         WWW www = new WWW(url);
         while (!www.isDone)
         {
             yield return null;
         }

         string alltext = www.text;
         Debug.Log(alltext);

     }*/

    /* IEnumerator GetUserId() {
         string url = "http://www.glazar.pro/api/marker/" + mTrackableBehaviour.TrackableName;
         WWW www = new WWW(url);
         while (!www.isDone)
         {
             yield return null;
         }

         string alltext = www.text;
         Debug.Log(alltext);
     }*/

    public string getStartTime() {
        return startTime;
    }
}
