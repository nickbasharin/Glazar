using System;
using UnityEngine;
using System.Collections;

public class ABLoader : MonoBehaviour
{
    public string BundleURL;
    public string BundleFullURL;
    public string ABName;
    public string AssetName;
    public string TextAssetName;
    public int version;
    public bool objectActive;
    public bool isRun;
    public bool isURL;
    private string urlstring;
    public bool onRequestLost;
    private Preloader preloader;

    void Start()
    {
        objectActive = false;
		isRun = false;
        onRequestLost = false;
        preloader = gameObject.GetComponentInChildren<Preloader>();
    }

    void OnDestroy() {
        StopAllCoroutines();
    }

    public void URLSet(string abname) {
		BundleURL = "http://glazar.pa.infobox.ru/ar/GlazAR/android/";
        AssetName = "obj";
        TextAssetName = "url";
        version = 0;
        ABName = abname.ToLower();
        BundleFullURL = BundleURL + ABName;
        Debug.Log(BundleFullURL);
    }

    public void StartDownload() {
        if (!isRun)
        {   
            StartCoroutine(DownloadAndCache());
        }

    }

    IEnumerator DownloadAndCache()
    {
        // Wait for the Caching system to be ready
        preloader.Loading();
        isRun = true;
        Debug.Log("isRunningTRUE");
        while (!Caching.ready)
            yield return null;
        Debug.Log("Caching ready");
        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(BundleFullURL, version))
        {
            Debug.Log("Loading ready");
			if (www.error == null) preloader.LoadPercent (www);
            yield return www;
            if (www.error != null)
            {
                Debug.Log(ABName + " not downloaded. Error" + www.error);
                preloader.CantLoad();
            }
            else
            {
                AssetBundle bundle = www.assetBundle;
                if (AssetName == "" && TextAssetName == "")
                    Debug.Log("Asset name not assigned");
                else
                {
                    Debug.Log("Asset ready");
                    if (bundle.Contains(AssetName))
                    {
                        Instantiate(bundle.LoadAsset(AssetName), gameObject.transform);
                        objectActive = true;
                        Debug.Log("is OBJ");
                        preloader.Loaded();
                    }
                    else {
                        Debug.Log("Check asset name");
                    }
                }
                // Unload the AssetBundles compressed contents to conserve memory
                bundle.Unload(false);
            }       
            www.Dispose();
        } 
        Debug.Log("isRunningFALSE");
        isRun = false;
        if (onRequestLost) {
            OnTrackingLost();
        }
    }
    public void RefreshPreloader() {
        if(preloader!=null)
        preloader.RefreshOff();
    }

    public void ActivePreloader()
    {
        if (preloader != null)
            preloader.Loading();
    }
/*    public void PlayURL() {
        urlplayer.PlayURL(urlstring);
    }*/

    private void OnTrackingLost()
    {       
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }
    }
}