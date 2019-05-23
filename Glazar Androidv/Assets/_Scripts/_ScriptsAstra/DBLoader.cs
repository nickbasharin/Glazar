using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using UnityEngine.Video;


public class DBLoader : MonoBehaviour
{

/*	public Text deb1;
	public Text deb2;
	public Text deb3;*/

	public string DataSetURL;
    private string DSPath;
    private bool requestCheck = false;
    private int version;
    private int current_version=-1;
    private bool isRunning = false;
    private InitStat initstat;
    public GameObject preloader;
    public GameObject videoGO;
	public GameObject handsOn;
    public GameObject loadImage;
    public GameObject mainCanvas;
    public GameObject riglaCanvas;

	public GameObject gatchinaPanel;

    Text loadText;
    WWW www1;

    private bool mLoaded = false;

    void Start()
    {
        loadText = loadImage.GetComponentInChildren<Text>();
        loadImage.SetActive(false);
        initstat = this.gameObject.GetComponent<InitStat>();
    }
    void Update () {
		if (VuforiaRuntimeUtilities.IsVuforiaEnabled () && !mLoaded) {
			mLoaded = true;
			VuforiaARController.Instance.RegisterVuforiaStartedCallback (StartStart);

		}

        if (loadImage.activeSelf) {
            loadText.text = "Загрузка базы " + Mathf.RoundToInt(www1.progress * 100) + "%";
        }
    }
    void StartStart()
    {

        DSPath = Application.persistentDataPath; //path of app
        Debug.Log("DSPath is " + DSPath);
//		deb1.text = "DSPath is " + DSPath; // BRL
        current_version = PlayerPrefs.GetInt("DS_version"); // check version of datafiles
        if (current_version==-1)
            {
            current_version = 0;
            PlayerPrefs.SetInt("DS_version",0);
            PlayerPrefs.Save();
            }
        if (!requestCheck && !isRunning)
        {
            StartCoroutine(CheckDSVersion());
        }
    }

    void OnApplicationFocus(bool onFocus) {
        if (onFocus && !requestCheck && !isRunning)
        {
            Debug.Log("FOCUS");
            StartCoroutine(CheckDSVersion());
        }
    }

    IEnumerator CheckDSVersion()
    {
        isRunning = true;
        WWW www = new WWW(DataSetURL + ".txt");
        while (!www.isDone)
        {
            yield return null;
        }
        if (string.IsNullOrEmpty(www.error))
        {
            version = int.Parse(www.text);
            if (version != current_version)
            {
                StartCoroutine(DownloadDS());
            }
            else LoadDataSet();               
        }
        else LoadDataSet();
    }

    
    IEnumerator DownloadDS()
    {
        WWW www = new WWW(DataSetURL + ".xml");
        while (!www.isDone)
        {
            yield return null;
        }
        Debug.Log("ds.xml downloaded");
        string fullPath = DSPath + "/ds.xml";
        if (string.IsNullOrEmpty(www.error))
            File.WriteAllBytes(fullPath, www.bytes);
        www.Dispose();

        loadImage.SetActive(true);

        www = new WWW(DataSetURL + ".dat");
        www1 = www;
        while (!www.isDone)
        {
            yield return null;
        }
        Debug.Log("ds.dat downloaded");
            fullPath = DSPath + "/ds.dat";
        if (string.IsNullOrEmpty(www.error))
            File.WriteAllBytes(fullPath, www.bytes);
        www.Dispose();
        current_version = version;
        PlayerPrefs.SetInt("DS_version", current_version);
        Debug.Log("versionChanged" + current_version);
	//	deb2.text = "versionChanged" + current_version; // BRL
        PlayerPrefs.Save();
        loadImage.SetActive(false);

        LoadDataSet();
    }

    private void LoadDataSet()
    {
        ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (tracker != null) { Debug.Log("tracker here"); }
        DataSet dset = tracker.CreateDataSet();
        if (dset.Load(DSPath + "/ds.xml", VuforiaUnity.StorageType.STORAGE_ABSOLUTE))
        {
            Debug.Log("Dataset loaded");
            if (tracker.ActivateDataSet(dset))
            {
                Debug.Log("DS activated");
                requestCheck = true;
	//			deb3.text = "DS activated";
            }
            else
            {
                Debug.Log("DS not activated");
            }
        }
        else
        {
            Debug.LogError("Failed to load dataset!");
	//		deb3.text = "Failed to load dataset!";

        }


        int counter = 0;

        IEnumerable<TrackableBehaviour> tbs = TrackerManager.Instance.GetStateManager().GetTrackableBehaviours();
        foreach (TrackableBehaviour tb in tbs)
        {
            if (tb.name == "New Game Object")
            {

                // change generic name to include trackable name
                tb.gameObject.name = ++counter + ":DynamicImageTarget-" + tb.TrackableName;

                // add additional script components for trackable
                if (tb.TrackableName.Contains("vid_"))
                {
                    VideoTrackableEventHandler vteh = tb.gameObject.AddComponent<VideoTrackableEventHandler>();
                    vteh.mainCanv = mainCanvas;
                    vteh.riglaCanv = riglaCanvas;
                    GameObject vGO = Instantiate(videoGO, tb.gameObject.transform);
                    ImageTargetBehaviour itb = (ImageTargetBehaviour)tb;
                    if (tb.TrackableName.Contains("stre"))
                    {
                        if (itb.GetSize().x > itb.GetSize().y)
                        {
                            vGO.transform.localScale = new Vector3(itb.GetSize().x / 100, vGO.transform.localScale.y, itb.GetSize().y / 100);
                        }
                        else
                        {
                            vGO.transform.localScale = new Vector3(itb.GetSize().x / (itb.GetSize().y * 10), vGO.transform.localScale.y, itb.GetSize().y / (itb.GetSize().y * 10));
                        }
                    }
                    else
                    {
                      //  Debug.Log(itb.GetSize());
                        //LOAD videoscale
                        StartCoroutine(DownloadVideoScale(vGO, tb.TrackableName));
                        //END LOAD videoscale
                    }
                    vGO.GetComponentInChildren<VideoPlayer>().url = "http://glazar.pa.infobox.ru/ar/GlazAR/Video/" + tb.TrackableName + ".mp4";
                    VideoController v = vGO.GetComponent<VideoController>();


                    if (tb.TrackableName.Contains("play")) {
                        v.playOnAwake = true;
                    }

                    HandsDeleter hd = tb.gameObject.AddComponent<HandsDeleter>();
                    hd.hands = handsOn;
                    hd.mainCanv = mainCanvas;
                    hd.riglaCanv = riglaCanvas;
                    StatGrabber sg = tb.gameObject.AddComponent<StatGrabber>();
                    sg.initstat = initstat;
                    v.statGrabber = sg;
                  //  v.stat
                }
                else {
                    tb.gameObject.AddComponent<ABLoader>();
                    GameObject prl = Instantiate(preloader, tb.gameObject.transform);
                    //       prl.transform.localScale = prl.transform.lossyScale; 
                    if (tb.TrackableName.Contains("deadpool"))
                    {
                      deadpoolInteractive a =  tb.gameObject.AddComponent<deadpoolInteractive>();
                        a.tagbtns = "deadbuttons";
                    }
                    if (tb.TrackableName.Contains("robottank"))
                    {
                        deadpoolInteractive a = tb.gameObject.AddComponent<deadpoolInteractive>();
                        a.tagbtns = "deadrobotbtns";
                    }
                    if (tb.TrackableName.Contains("tank")|| tb.TrackableName.Contains("deadpool"))
                    {
                        ((ImageTarget)tb.Trackable).StartExtendedTracking();
                    }

                    TrackableHandler tr = tb.gameObject.AddComponent<TrackableHandler>();
                        tr.hands = handsOn;
                    tr.mainCanv = mainCanvas;
                    tr.riglaCanv = riglaCanvas;
					if (tb.TrackableName.Contains ("pavelg")) {
						PavelMarker pm = tb.gameObject.AddComponent<PavelMarker>();
						pm.gatchinaUI = gatchinaPanel;
					}
                }
                tb.gameObject.AddComponent<TurnOffBehaviour>();
            }
        }
        handsOn.SetActive(true);

        isRunning = false;
    }

	IEnumerator DownloadVideoScale(GameObject video, String tbname)
	{
		WWW www = new WWW("http://glazar.pa.infobox.ru/ar/GlazAR/Video/" + tbname+ "w.txt");
		while (!www.isDone)
		{
			yield return null;
		}
		float vidX;
		if (string.IsNullOrEmpty (www.error)) {
			vidX = float.Parse (www.text);
			video.transform.localScale = new Vector3 (vidX, video.transform.localScale.y,  video.transform.localScale.z) ;
		}
		www.Dispose();


		WWW www2 = new WWW("http://glazar.pa.infobox.ru/ar/GlazAR/Video/" + tbname+ "h.txt");
		while (!www2.isDone)
		{
			yield return null;
		}
		float vidZ;
		if (string.IsNullOrEmpty (www2.error)) {
			vidZ = float.Parse (www2.text);
			video.transform.localScale = new Vector3 (video.transform.localScale.x, video.transform.localScale.y,  vidZ) ;

		}


		www2.Dispose();

	}   





}
