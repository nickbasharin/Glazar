using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollector : MonoBehaviour {

    public GameObject[] stars;
    public GameObject allStarsPanel;

    int starCounter;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("PrizGone")) {
            this.gameObject.SetActive(false);
        }
        starCount();


    }
	
    public void starCount()
    {
        starCounter = 0;
        for (int i = 1; i <= stars.Length; i++)
        {
            string s = "" + i;
            if (PlayerPrefs.HasKey("rigla" + s))
            {
                starCounter++;
            }
        }

        for (int i = 0; i < starCounter; i++)
        {
            stars[i].SetActive(true);

        }
        if (starCounter == 6) {
            allStarsPanel.SetActive(true);
        }

    }
    // Update is called once per frame
    void Update () {
		
	}
}
