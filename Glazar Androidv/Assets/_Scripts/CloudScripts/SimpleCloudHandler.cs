using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.Video;
using System;

public class SimpleCloudHandler : MonoBehaviour, ICloudRecoEventHandler
{

    public ImageTargetBehaviour ImageTargetTemplate;
    public GameObject handsOn;
    public GameObject preloader;
    public GameObject videoGO;
    public GameObject mainCanvas;
    public GameObject riglaCanvas;
    public InitStat initstat;

    ObjectTracker tracker;

    private CloudRecoBehaviour mcloudRecoBehaviour;
    private bool mIsscanning = false;
    // Use this for initialization
    void Start()
    {
        CloudRecoBehaviour cloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        if (cloudRecoBehaviour)
        {
            cloudRecoBehaviour.RegisterEventHandler(this);
        }
        mcloudRecoBehaviour = cloudRecoBehaviour;

    }

    public void OnInitialized()
    {

    }

    public void OnInitError(TargetFinder.InitState initError)
    {

    }

    public void OnUpdateError(TargetFinder.UpdateState updateError)
    {

    }
    public void OnStateChanged(bool scanning)
    {
        mIsscanning = scanning;
        if (scanning)
        {
            ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            tracker.TargetFinder.ClearTrackables(false);
        }
    }

    public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
    {
        GameObject newImageTarget = Instantiate(ImageTargetTemplate.gameObject) as GameObject;
        GameObject augmentation = null;
        if (augmentation != null) augmentation.transform.SetParent(newImageTarget.transform);
        if (ImageTargetTemplate)
        {
            tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            ImageTargetBehaviour imageTargetBehaviour = (ImageTargetBehaviour)tracker.TargetFinder.EnableTracking(targetSearchResult, newImageTarget);
            if (imageTargetBehaviour.TrackableName.Contains("3dd"))
            {
                TrackableHandler tr = newImageTarget.gameObject.AddComponent<TrackableHandler>();

                newImageTarget.gameObject.AddComponent<ABLoader>();
                Instantiate(preloader, newImageTarget.gameObject.transform);
                tr.hands = handsOn;
                tr.mainCanv = mainCanvas;
                tr.riglaCanv = riglaCanvas;

            }
            else
            {

                imageTargetBehaviour.GetSize();

                VideoTrackableEventHandler vteh = imageTargetBehaviour.gameObject.AddComponent<VideoTrackableEventHandler>();
                vteh.mainCanv = mainCanvas;
                vteh.riglaCanv = riglaCanvas;

                GameObject vGO = Instantiate(videoGO, imageTargetBehaviour.gameObject.transform);

                //LOAD videoscale
                //    StartCoroutine(DownloadVideoScale(vGO, imageTargetBehaviour.TrackableName));
                //END LOAD videoscale
                vGO.GetComponentInChildren<VideoPlayer>().url = "http://glazar.pro/vid/" + imageTargetBehaviour.TrackableName + ".mp4";  //  "http://glazar.pro/api/marker/" + imageTargetBehaviour.TrackableName + "/stream"; // old version ;
                VideoController v = vGO.GetComponent<VideoController>();

                if (!imageTargetBehaviour.TrackableName.Contains("noplay"))
                {
                    v.playOnAwake = true;
                }

                StartCoroutine(StrechOrNot(vGO, imageTargetBehaviour.TrackableName, imageTargetBehaviour.GetSize()));

                HandsDeleter hd = imageTargetBehaviour.gameObject.AddComponent<HandsDeleter>();
                hd.hands = handsOn;
                hd.mainCanv = mainCanvas;
                hd.riglaCanv = riglaCanvas;
                hd.itCloud = true;
                StatGrabber sg = imageTargetBehaviour.gameObject.AddComponent<StatGrabber>();
                sg.initstat = initstat;
                v.statGrabber = sg;

                imageTargetBehaviour.gameObject.AddComponent<CloudController>();
            }
            mcloudRecoBehaviour.CloudRecoEnabled = false;
        }
        string a = targetSearchResult.MetaData;
        Debug.Log(targetSearchResult.TargetName + " CLOUD CHECK");
        /*   if (!mIsscanning)
           {
                       mcloudRecoBehaviour.CloudRecoEnabled = true;

           }*/

    }

    IEnumerator StrechOrNot(GameObject video, String tbname, Vector2 size)
    {
        string url = "http://www.glazar.pro/api/marker/" + tbname +"/video"; // + tbname;
        WWW www = new WWW(url);
        while (!www.isDone)
        {
            yield return null;
        }
        string strech = www.text;
        string boolstrech = strech.Substring(strech.IndexOf("stretch") + 9, 3);
        if (boolstrech.Contains("tr")) {
            if (size.x > size.y)
            {
                video.transform.localScale = new Vector3(size.x / 100, video.transform.localScale.y, size.y / 100);
            }
            else
            {
                video.transform.localScale = new Vector3(size.x / (size.y * 10), video.transform.localScale.y, size.y / (size.y * 10));
            }
        }
    }
    public void StartCloudReco()
    {
        mcloudRecoBehaviour.CloudRecoEnabled = true;
    }
    public void StopCloudReco()
    {
        mcloudRecoBehaviour.CloudRecoEnabled = false;
    }
}