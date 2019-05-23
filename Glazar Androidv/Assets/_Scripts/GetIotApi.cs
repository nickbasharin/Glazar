using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIotApi : MonoBehaviour {

    TextMesh tm;
    float timer;

    // Use this for initialization
	void Start () {
        tm = GetComponent<TextMesh>();
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
 
        timer = timer + Time.deltaTime;
        if (timer > 2.5f) {
            StartCoroutine(DownloadIotAPI());
            timer = 0;
        }
    }


    IEnumerator DownloadIotAPI()
    {

        string url = "https://energy.saymon.info/node/api/objects/5ba9001ac023277bdbddcb6c/stat?api-token=49127d0a-5a80-4b80-b27f-d30c9eb2683c";
        WWW www = new WWW(url);
        while (!www.isDone)
        {
            yield return null;
        }
        string alltext = www.text;
        // int linkStop = alltext.IndexOf("%22" + "Name" + "%22" + ":" + "%22"+ "Total" + "%22" + "," + "%22" + "I" + "%22" + ":" + "%22" + "-" + "%22" + "," + "%22" + "P" + "%22");
      //  Debug.Log(alltext.IndexOf("Total"));

        string allSubs = alltext.Substring(alltext.IndexOf("Total")+5, alltext.Length - alltext.IndexOf("Total")-6);
        // "Name":"Total","I":"-","P":"
        int linkStop = allSubs.IndexOf("Total");
  //      Debug.Log(linkStop);
        
        string link = allSubs.Substring(linkStop + 36, 4);
        string textMarker = link;
        if (link.Contains("\"")) {
            textMarker = link.Substring(0, 3);
        }

        if (textMarker.Contains("\"")) { textMarker = "0.0"; }

    //    Debug.Log(textMarker);

        tm.text = textMarker + " Вт";

    }
}
